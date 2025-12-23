using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OrderSystem.Core;
using OrderSystem.Win.Forms;

namespace OrderSystem.Win
{
    public static class Program
    {
        [STAThread]
        public static void Main()
        {
            SQLitePCL.Batteries_V2.Init();
            ApplicationConfiguration.Initialize();

            using IHost host = Host.CreateDefaultBuilder()
                                   .ConfigureAppConfiguration((_, config) =>
                                    {
                                        config.AddJsonFile("appsettings.json", optional: false);
                                    })
                                   .ConfigureServices((context, services) =>
                                    {
                                        services.AddLogging();
                                        services.AddDbContext<OrderContext>(options =>
                                        {
                                            options.UseSqlite(context.Configuration.GetConnectionString("Default"));
                                        });

                                        services.AddScoped<MainForm>();
                                    }).Build();

            using IServiceScope scope = host.Services.CreateScope();
            OrderContext db = scope.ServiceProvider.GetRequiredService<OrderContext>();
            db.Database.Migrate();

            MainForm mainForm = host.Services.GetRequiredService<MainForm>();
            Application.Run(mainForm);
        }
    }
}