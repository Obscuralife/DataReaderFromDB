using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace HostedService
{
    /// <inheritdoc/>
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> logger;
        private readonly string dataPath;

        /// <summary>
        /// Initializes a new instance of the <see cref="Worker"/> class.
        /// </summary>
        /// <param name="logger">.</param>
        /// <param name="settings">..</param>
        public Worker(ILogger<Worker> logger, IOptions<AppSettings> settings)
        {
            this.logger = logger;
            dataPath = settings.Value.PathToLocationsFile;
        }

        /// <inheritdoc/>
        public override void Dispose()
        {
            base.Dispose();
        }

        /// <inheritdoc/>
        public override Task StartAsync(CancellationToken cancellationToken)
        {
            logger.LogInformation("Service Starting");
            if (!File.Exists(dataPath))
            {
                logger.LogWarning($"Please make sure the input file [{dataPath}] exists, then restart the service.");
                return Task.CompletedTask;
            }

            return base.StartAsync(cancellationToken);
        }

        /// <inheritdoc/>
        public override Task StopAsync(CancellationToken cancellationToken)
        {
            return base.StopAsync(cancellationToken);
        }

        /// <inheritdoc/>
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
