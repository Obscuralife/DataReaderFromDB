using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Services.HostService;

namespace HostedService
{
    /// <inheritdoc/>
    public sealed class Worker : IHostedService, IDisposable
    {
        private readonly ILogger<Worker> logger;
        private readonly IServiceProvider serviceProvider;
        private readonly string dataPath;
        private readonly IDataService dataService;
        private Timer timer;

        /// <summary>
        /// Initializes a new instance of the <see cref="Worker"/> class.
        /// </summary>
        /// <param name="logger">logger.</param>
        /// <param name="settings">settings.</param>
        /// <param name="serviceProvider">service provider.</param>
        public Worker(ILogger<Worker> logger, IOptions<AppSettings> settings, IServiceProvider serviceProvider)
        {
            this.logger = logger;
            this.serviceProvider = serviceProvider;
            dataPath = settings.Value.PathToLocationsFile;
            dataService = GetScopeDataService();
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            logger.LogInformation("Disposing.");
            timer.Dispose();
        }

        /// <inheritdoc/>
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            logger.LogInformation("Service Starting");
            if (!File.Exists(dataPath))
            {
                logger.LogWarning($"Please make sure the input file [{dataPath}] exists, then restart the service.");
                await Task.CompletedTask;
            }

            DoBackgroundWork();

            await Task.CompletedTask;
        }

        /// <inheritdoc/>
        public async Task StopAsync(CancellationToken cancellationToken)
        {
            logger.LogInformation("Hosted Service is stopping.");
            timer?.Change(Timeout.Infinite, 0);
            await Task.CompletedTask;
        }

        private void DoBackgroundWork()
        {
            dataService.Build(dataPath);
            timer = new Timer(async (i) => await dataService.PushEntityToSqlBaseAsync(), null, TimeSpan.Zero, TimeSpan.FromMinutes(10));
            logger.LogInformation("Background service is working");
        }

        private IDataService GetScopeDataService()
        {
            using (var scope = serviceProvider.CreateScope())
            {
                return scope.ServiceProvider.GetRequiredService<IDataService>();
            }
        }
    }
}
