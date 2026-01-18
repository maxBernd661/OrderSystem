using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
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

            ILoggerFactory loggerFactory = Logger.BuildLoggerFactory();
            ILogger logger = loggerFactory.CreateLogger<MainForm>();
            logger.LogInformation("Started App");

            ExceptionHandler.Init(loggerFactory.CreateLogger("Global"));

            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            Application.ThreadException += (_, args) => ExceptionHandler.Handle(args.Exception, "Exception in UI Thread");
            AppDomain.CurrentDomain.UnhandledException += (_, args) =>
                ExceptionHandler.Handle(args.ExceptionObject as Exception ?? new Exception("Encountered Non-Exception"), "AppDomain unhandled", args.IsTerminating);

            TaskScheduler.UnobservedTaskException += (_, args) =>
            {
                ExceptionHandler.Handle(args.Exception, "Unobserved Task");
                args.SetObserved();
            };

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

                                        services.AddDbContextFactory<OrderContext>(options =>
                                        {
                                            options.UseSqlite(context.Configuration.GetConnectionString("Default"));
                                        });

                                        services.AddSingleton<MainForm>();
                                        services.AddTransient<PopupView>();

                                        services.AddTransient(typeof(ListView<>));
                                        services.AddTransient(typeof(DetailView<>));

                                        services.AddTransient<IProductLookupProvider, ProductLookupProvider>();
                                        services.AddTransient<ICustomerLookupProvider, CustomerLookupProvider>();

                                        services.AddSingleton<ViewFactory>();
                                        services.AddSingleton<ViewManager>();
                                        services.AddSingleton<FilterRegistry>();

                                        services.AddTransient<IQueryProfile<Product>, ProductQueryProfile>();
                                        services.AddTransient<IQueryProfile<Customer>, CustomerQueryProfile>();
                                        services.AddTransient<IQueryProfile<Order>, OrderQueryProfile>();
                                        services.AddTransient<IQueryProfile<OrderItem>, OrderItemQueryProfile>();

                                        services.AddTransient<IGraphMerger<Order>, OrderGraphMerger>();

                                        services.AddScoped(typeof(DataManipulationService<>));
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