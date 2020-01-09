using System;
using System.Linq;
using System.Threading.Tasks;
using DataAccessLayer;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using CustomConsole = Services.ConsoleService.ConsoleExtension;

namespace Services
{
    /// <inheritdoc/>
    public sealed class CommandService : ICommandService
    {
        private const int CommandIndex = 0;
        private const int ArgumentIndex = 1;
        private readonly IHelpService helpService;
        private readonly Tuple<string, Func<string[], Task<Location[]>>>[] requestToDbCommands;
        private readonly Tuple<string, Func<string, Task<Location[]>>>[] getRequestAttributes;
        private readonly Tuple<string, Func<string, Task>>[] secondaryCommands;
        private readonly ApplicationDbContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandService"/> class.
        /// </summary>
        /// <param name="context">data context.</param>
        public CommandService(ApplicationDbContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            helpService = new HelpService();

            requestToDbCommands = new Tuple<string, Func<string[], Task<Location[]>>>[]
            {
                new Tuple<string, Func<string[], Task<Location[]>>>("get", GetAsync),
            };

            getRequestAttributes = new Tuple<string, Func<string, Task<Location[]>>>[]
                {
                    new Tuple<string, Func<string, Task<Location[]>>>("-id", GetByIdAsync),
                    new Tuple<string, Func<string, Task<Location[]>>>("-address", GetByAddressAsync),
                    new Tuple<string, Func<string, Task<Location[]>>>("-city", GetByCityAsync),
                };

            secondaryCommands = new Tuple<string, Func<string, Task>>[]
            {
                new Tuple<string, Func<string, Task>>("help", PrintHelpAsync),
                new Tuple<string, Func<string, Task>>("exit", ExitAsync),
            };
        }

        /// <inheritdoc/>
        public bool IsRunning { get; private set; } = true;

        /// <inheritdoc/>
        public async Task<Location[]> ParseComandLine(string commandString)
        {
            if (string.IsNullOrWhiteSpace(commandString))
            {
                await PrintHelpAsync();
                return Array.Empty<Location>();
            }

            var splitedComand = commandString.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var requestToDbCommandIndex = Array.FindIndex(
                requestToDbCommands,
                command => string.Equals(command.Item1, splitedComand[CommandIndex], StringComparison.InvariantCultureIgnoreCase));

            if (requestToDbCommandIndex >= 0)
            {
                var arguments = splitedComand.Length > 1 ? splitedComand.Skip(ArgumentIndex).ToArray<string>() : null;
                return await requestToDbCommands[requestToDbCommandIndex].Item2(arguments);
            }
            else
            {
                var secondaryCommandIndex = Array.FindIndex(
                secondaryCommands,
                command => string.Equals(command.Item1, splitedComand[CommandIndex], StringComparison.InvariantCultureIgnoreCase));

                var argument = splitedComand.Length > 1 ? splitedComand[ArgumentIndex] : null;
                if (secondaryCommandIndex >= 0)
                {
                    await secondaryCommands[secondaryCommandIndex].Item2(argument);
                }
                else
                {
                    CustomConsole.WriteLineRedColor($"There is no explanation for '{splitedComand[CommandIndex]}' command.");
                }
            }

            return Array.Empty<Location>();
        }

        private async Task<Location[]> GetAsync(string[] arguments)
        {
            if (arguments == null || string.IsNullOrWhiteSpace(arguments[0]))
            {
                return await GetAllAsync();
            }
            else
            {
                const int GetParameter = 0;
                const int GetParameterValue = 1;
                var commandIndex = Array.FindIndex(
                    getRequestAttributes,
                    command => string.Equals(command.Item1, arguments[GetParameter], StringComparison.InvariantCultureIgnoreCase));

                if (commandIndex >= 0)
                {
                    var getParameter = arguments.Length > 1 ? arguments[GetParameterValue] : null;
                    var locations = await getRequestAttributes[commandIndex].Item2(getParameter);
                    return locations;
                }
                else
                {
                    CustomConsole.WriteLineRedColor($"There is no available '{arguments[GetParameter]}' command");
                    helpService.PrintHelp("get");
                    return Array.Empty<Location>();
                }
            }
        }

        private async Task<Location[]> GetByCityAsync(string city)
        {
            if (string.IsNullOrWhiteSpace(city))
            {
                return await GetOrderedCitiesAsync();
            }
            else
            {
                city = city.Trim();
                var locationList = await context.Locations.ToListAsync();
                var locations = locationList.Where(i => FindCityExpression(i.Name, city));

                if (locations.Count() == 0)
                {
                    CustomConsole.WriteLineRedColor($"There is no location with '{city}' city");
                    return Array.Empty<Location>();
                }

                return locations.ToArray();
            }
        }

        private bool FindCityExpression(string currentRoomName, string necessaryRoomName)
        {
            const int StartCityIndex = 1;
            string currentCityName = string.Empty;
            var splitedRoomName = currentRoomName.Split(new char[] { ' ', ',' });
            var separatorIndex = Array.FindIndex(splitedRoomName, i => i == string.Empty);

            if (!HasCityName(splitedRoomName))
            {
                return false;
            }

            var joinCount = HasOfficeName(separatorIndex) ? separatorIndex - StartCityIndex : splitedRoomName.Length - StartCityIndex;
            currentCityName = string.Join(' ', splitedRoomName, StartCityIndex, joinCount);

            return string.Equals(currentCityName, necessaryRoomName, StringComparison.InvariantCultureIgnoreCase);
        }

        private bool HasCityName(string[] name)
        {
            return name.Length > 1;
        }

        private bool HasOfficeName(int separatorIndex)
        {
            return separatorIndex > 0;
        }

        private async Task<Location[]> GetByAddressAsync(string locationAddress)
        {
            if (string.IsNullOrWhiteSpace(locationAddress))
            {
                return await GetOrderedAddressesAsync();
            }
            else
            {
                var locations = await context.Locations.Where(i => i.Address == locationAddress).ToArrayAsync();
                if (locations.Length == 0)
                {
                    CustomConsole.WriteLineRedColor($"There is no location with '{locationAddress}' address");
                    return Array.Empty<Location>();
                }

                return locations;
            }
        }

        private async Task<Location[]> GetByIdAsync(string locationId)
        {
            var success = int.TryParse(locationId, out int id);
            if (!success)
            {
                CustomConsole.WriteLineRedColor("id - must be a number");
                return null;
            }
            else
            {
                var locations = await context.Locations.Where(i => i.Id == id).ToArrayAsync();
                if (locations.Length == 0)
                {
                    CustomConsole.WriteLineRedColor($"There is no location with '{locationId}' Id");
                    return Array.Empty<Location>();
                }

                return locations;
            }
        }

        private async Task<Location[]> GetAllAsync(string[] args = null)
        {
            return await context.Locations.OrderBy(i => i.Id).ToArrayAsync();
        }

        private async Task ExitAsync(string args = null)
        {
            await context.DisposeAsync();
            IsRunning = false;
            CustomConsole.WriteLineGreenColor("Good bye");
            await Task.CompletedTask;
        }

        private async Task<Location[]> GetOrderedCitiesAsync()
        {
            return await context.Locations.OrderBy(i => i.Name).ToArrayAsync();
        }

        private async Task<Location[]> GetOrderedAddressesAsync()
        {
            return await context.Locations.OrderBy(i => i.Address).ToArrayAsync();
        }

        private async Task PrintHelpAsync(string command = null)
        {
            if (command == null)
            {
                helpService.PrintHelp();
            }
            else
            {
                helpService.PrintHelp(command);
            }

            await Task.CompletedTask;
        }
    }
}
