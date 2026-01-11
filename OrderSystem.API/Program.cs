using Microsoft.EntityFrameworkCore;
using OrderSystem.Core;

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
            builder.Services.AddDbContext<OrderContext>(options =>
            {
                options.UseSqlite(builder.Configuration.GetConnectionString("Default"));
            });

            WebApplication app = builder.Build();
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            //app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            using IServiceScope scope = app.Services.CreateScope();
            OrderContext db = scope.ServiceProvider.GetRequiredService<OrderContext>();
            db.Database.Migrate();

            app.Run();
        }
    }
}