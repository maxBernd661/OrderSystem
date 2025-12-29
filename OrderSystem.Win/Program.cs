using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OrderSystem.Core;
using OrderSystem.Win.Forms;
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
                                        services.AddTransient<ProductDetailView>();
                                        services.AddTransient<CustomerDetailView>();

                                        //services.AddSingleton<IViewDescriptor>(new ViewDescriptor<CustomerListView, Customer>(ViewKind.ListView, "All Customers"));
                                        //services.AddSingleton<IViewDescriptor>(new ViewDescriptor<CustomerDetailView, Customer>(ViewKind.DetailView, "New Customer"));
                                        //services.AddSingleton<IViewDescriptor>(new ViewDescriptor<ProductListView, Product>(ViewKind.ListView, "All Products"));
                                        //services.AddSingleton<IViewDescriptor>(new ViewDescriptor<ProductDetailView, Product>(ViewKind.DetailView, "New Product"));
                                    }).Build();

            using IServiceScope scope = host.Services.CreateScope();
            OrderContext db = scope.ServiceProvider.GetRequiredService<OrderContext>();
            db.Database.Migrate();

            MainForm mainForm = host.Services.GetRequiredService<MainForm>();
            try
            {
                Application.Run(mainForm);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().Name, MessageBoxButtons.OK);
            }
        }
    }
}