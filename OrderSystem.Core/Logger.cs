using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Extensions.Logging;

namespace OrderSystem.Core
{
    public static class Logger
    {
        public static ILoggerFactory BuildLoggerFactory()
        {
            Log.Logger = new LoggerConfiguration()
                        .MinimumLevel.Debug()
                        .WriteTo.File("logs/app-.log", rollingInterval: RollingInterval.Day,
                             rollOnFileSizeLimit: true)
                        .CreateLogger();

            return new SerilogLoggerFactory(Log.Logger, true);
        }
    }
}