using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;
using OrderSystem.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Diagnostics;

namespace OrderSystem.API
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            SQLitePCL.Batteries_V2.Init();
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddLogging();

            builder.Services.AddDbContext<OrderContext>(options =>
            {
                options.UseSqlite(builder.Configuration.GetConnectionString("Default"));
            });

            builder.Services.AddProblemDetails();

            ILoggerFactory loggerFactory = Logger.BuildLoggerFactory();
            ILogger logger = loggerFactory.CreateLogger("Program");
            logger.LogInformation("Started App");

            WebApplication app = builder.Build();
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.Use(async (ctx, next) =>
            {
                const string header = "X-Correlation-ID";
                if (!ctx.Request.Headers.TryGetValue(header, out StringValues cid) ||
                    string.IsNullOrWhiteSpace(cid))
                {
                    cid = Guid.NewGuid().ToString("N");
                }

                ctx.Response.Headers[header] = cid!;
                ctx.Items[header] = cid!.ToString();
                await next();
            });

            app.UseExceptionHandler(handler =>
            {
                handler.Run(async ctx =>
                {
                    IExceptionHandlerFeature? feature = ctx.Features.Get<IExceptionHandlerFeature>();
                    Exception? ex = feature?.Error;

                    ILogger logger = ctx.RequestServices
                                        .GetRequiredService<ILoggerFactory>()
                                        .CreateLogger("GlobalException");

                    string? cid = ctx.Items["X-Correlation-ID"]?.ToString();

                    logger.LogError(ex, "Unhandled exception. CorrelationId={correlationId} Path={path}", cid, ctx.Request.Path);

                    (int status, string title) = ex switch
                    {
                        NullReferenceException => (StatusCodes.Status404NotFound, "Not Found"),
                        ArgumentException => (StatusCodes.Status400BadRequest, "Bad Request"),
                        UnauthorizedAccessException => (StatusCodes.Status403Forbidden, "Forbidden"),
                        var _ => (StatusCodes.Status500InternalServerError, "Internal Server Error")
                    };

                    ctx.Response.StatusCode = status;

                    ProblemDetails problem = new()
                    {
                        Status = status,
                        Title = title,
                        Detail = status == 500 ? "An internal error has occurred" : ex?.Message,
                        Instance = ctx.Request.Path,
                        Extensions =
                        {
                            ["correlationId"] = cid
                        }
                    };

                    await ctx.Response.WriteAsJsonAsync(problem);
                });
            });

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            using IServiceScope scope = app.Services.CreateScope();
            OrderContext db = scope.ServiceProvider.GetRequiredService<OrderContext>();
            db.Database.Migrate();

            app.Run();
        }
    }
}