using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OrderSystem.Core;
using OrderSystem.Core.Entities;
using OrderSystem.Win.Forms;
using OrderSystem.Win.Services;
using OrderSystem.Win.View;

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

                                        services.AddTransient<MainForm>();
                                        services.AddTransient(typeof(ListView<>));
                                        services.AddTransient(typeof(DetailView<>));

                                        services.AddSingleton<ViewFactory>();
                                        services.AddSingleton<FilterRegistry>();
                                    }).Build();

            using IServiceScope scope = host.Services.CreateScope();
            OrderContext db = scope.ServiceProvider.GetRequiredService<OrderContext>();
            db.Database.Migrate();

            MainForm mainForm = host.Services.GetRequiredService<MainForm>();

            BuildFilterKeys(host.Services);

            try
            {
                Application.Run(mainForm);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().Name, MessageBoxButtons.OK);
            }
        }

        private static void BuildFilterKeys(IServiceProvider sp)
        {
            FilterRegistry registry = sp.GetRequiredService<FilterRegistry>();
            registry.Register<Order>("OpenOrders", order => order.Status is not OrderStatus.Shipped);
        }
    }
}