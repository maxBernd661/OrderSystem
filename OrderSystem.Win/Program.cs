using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OrderSystem.Domain;
using OrderSystem.Win.Forms;

namespace OrderSystem.Win
{
    public static class Program
    {
        [STAThread]
        public static void Main()
        {
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
                                            options.UseNpgsql(context.Configuration.GetConnectionString("Default"));
                                        });

                                        services.AddScoped<MainForm>();
                                    }).Build();

            MainForm mainForm = host.Services.GetRequiredService<MainForm>();
            Application.Run(mainForm);
        }
    }
}