using Microsoft.Extensions.Logging;

namespace OrderSystem.Win.Services
{
    public static class ExceptionHandler
    {
        private static ILogger? log;

        private static int handling;

        public static void Init(ILogger logger)
        {
            log = logger;
        }

        public static void Handle(Exception ex, string? context = null, bool isTerminating = false)
        {
            if (Interlocked.Exchange(ref handling, 1) == 1)
            {
                return;
            }

            try
            {
                string id = Guid.NewGuid().ToString("N");
                log?.LogError(ex,
                    "Unhandled exception Id={id} Context={context}. Terminating={terminating}", id, context,
                    isTerminating);

                string message = $"An enexpected error occurred.\r\n{ex.Message}";
                if (isTerminating)
                {
                    message += "\r\nThe application will now shut down.";
                }

                MessageBox.Show(message, ex.GetType().Name, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Interlocked.Exchange(ref handling, 0);
            }
        }
    }
}