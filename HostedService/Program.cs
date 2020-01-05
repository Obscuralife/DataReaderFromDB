using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Web;

namespace HostedService
{
    /// <summary>
    /// Represents a main host service class.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// /// Represents a main entry point to host service.
        /// </summary>
        /// <param name="args">.</param>
        public static void Main(string[] args)
        {
            var logger = NLogBuilder.ConfigureNLog("NLog.config").GetCurrentClassLogger();

            try
            {
                logger.Info(new string('*', Console.WindowWidth / 2));
                logger.Info($"Application Starts. Version: {System.Reflection.Assembly.GetEntryAssembly()?.GetName().Version}");
                logger.Info($"Application Directory: {AppDomain.CurrentDomain.BaseDirectory}");
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                logger.Fatal(ex, "Application terminated unexpectedly");
            }
            finally
            {
                logger.Info(new string('*', Console.WindowWidth / 2) + "\r");
                LogManager.Shutdown();
            }
        }

        /// <summary>
        /// Creates host builder.
        /// </summary>
        /// <param name="args">.</param>
        /// <returns><see cref="IHostBuilder"/>.</returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<Worker>();
                    services.Configure<AppSettings>(hostContext.Configuration.GetSection("AppSettings"));
                })
            .ConfigureLogging(logging =>
            {
                logging.ClearProviders();
                logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
            })
            .UseNLog();
    }
}
