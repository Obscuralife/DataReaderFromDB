using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using DataAccessLayer;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Services.HostService
{
    /// <summary>
    /// Represents a data service.
    /// </summary>
    public class DataService : IDataService
    {
        private readonly ILogger<DataService> logger;
        private readonly ApplicationDbContext context;
        private int currentPosition;
        private List<Location> locations;

        /// <summary>
        /// Initializes a new instance of the <see cref="DataService"/> class.
        /// </summary>
        /// <param name="logger"><see cref="ILogger"/> instance of logging.</param>
        public DataService(ILogger<DataService> logger)
        {
            logger.LogInformation("Initialise data service.");
            this.logger = logger;
            currentPosition = 0;
            logger.LogInformation("Connecting to DataBase.");
            context = new ApplicationDbContext();
        }

        /// <inheritdoc/>
        public bool MoveNext()
        {
            return !(currentPosition >= locations.Count);
        }

        /// <inheritdoc/>
        public async Task PushEntityToSqlBaseAsync()
        {
            if (MoveNext())
            {
                var location = locations[currentPosition];
                if (!await IsExist(location))
                {
                    currentPosition++;
                    context.Locations.Add(location);
                    logger.LogInformation($"Add '{location.Name}' location");
                    await context.SaveChangesAsync();
                }
                else
                {
                    logger.LogWarning($"'{location.Name}' is exist");
                }
            }
            else
            {
                logger.LogInformation("All entities is recorded");
            }

            await Task.CompletedTask;
        }

        /// <inheritdoc/>
        public void Build(string dataPath)
        {
            var jsonString = File.ReadAllText(dataPath);
            logger.LogInformation("Desirialize location entities.");
            locations = JsonSerializer.Deserialize<List<Location>>(jsonString);
        }

        private async Task<bool> IsExist(Location location)
        {
            var locations = await context.Locations.Where(i => i.Address == location.Address).ToArrayAsync();
            return locations.Length > 0;
        }
    }
}
