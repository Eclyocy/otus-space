using EventGenerator.API.Helpers;
using Microsoft.Extensions.Logging.Console;

namespace EventGenerator
{
    /// <summary>
    /// Web-service entry point.
    /// </summary>
    public class Program
    {
        #region public methods

        /// <summary>
        /// Configure, build and run the web-server.
        /// </summary>
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        #endregion

        #region private methods

        private static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>())
                .ConfigureLogging(logging =>
                {
                    logging
                        .AddConsole(options => options.FormatterName = nameof(CustomConsoleFormatter))
                        .AddConsoleFormatter<CustomConsoleFormatter, ConsoleFormatterOptions>();

                    logging
                        .AddApplicationInsights();
                });

        #endregion
    }
}