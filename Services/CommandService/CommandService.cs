using System;
using System.Linq;
using DataAccessLayer;
using DataAccessLayer.Models;
using CustomConsole = Services.ConsoleService.ConsoleExtension;

namespace Services
{
    /// <inheritdoc/>
    public sealed class CommandService : ICommandService
    {
        private const int CommandIndex = 0;
        private readonly IHelpService helpService;
        private readonly Tuple<string, Action<string[]>>[] commands;
        private readonly Tuple<string, Action<string>>[] getCommands;
        private readonly ApplicationDbContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandService"/> class.
        /// </summary>
        /// <param name="context">data context.</param>
        public CommandService(ApplicationDbContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            helpService = new HelpService();

            commands = new Tuple<string, Action<string[]>>[]
            {
                new Tuple<string, Action<string[]>>("help", PrintHelp),
                new Tuple<string, Action<string[]>>("exit", Exit),
                new Tuple<string, Action<string[]>>("get", Get),
            };

            getCommands = new Tuple<string, Action<string>>[]
                {
                    new Tuple<string, Action<string>>("-id", GetById),
                    new Tuple<string, Action<string>>("-address", GetByAddress),
                    new Tuple<string, Action<string>>("-city", GetByCity),
                };
        }

        /// <inheritdoc/>
        public bool IsRunning { get; private set; } = true;

        /// <inheritdoc/>
        public void Initialize(string commandLine)
        {
            if (string.IsNullOrEmpty(commandLine))
            {
                PrintHelp();
            }

            var input = commandLine.Split(' ', 3);
            var index = Array.FindIndex(
                commands,
                i => string.Equals(i.Item1, input[CommandIndex], StringComparison.InvariantCultureIgnoreCase));

            if (index >= 0)
            {
                var parameteres = input.Length > 1 ? input.Skip(1).ToArray<string>() : null;
                commands[index].Item2(parameteres);
            }
            else
            {
                CustomConsole.WriteLineRedColor($"There is no explanation for '{input[CommandIndex]}' command.");
            }
        }

        private void Get(string[] parameteres)
        {
            if (parameteres == null || string.IsNullOrWhiteSpace(parameteres[0]))
            {
                GetAll();
            }
            else
            {
                const int GetParameter = 0;
                const int GetParameterValue = 1;
                var index = Array.FindIndex(
                    getCommands,
                    i => string.Equals(i.Item1, parameteres[GetParameter], StringComparison.InvariantCultureIgnoreCase));

                if (index >= 0)
                {
                    var getParameter = parameteres.Length > 1 ? parameteres[GetParameterValue] : null;
                    getCommands[index].Item2(getParameter);
                }
                else
                {
                    CustomConsole.WriteLineRedColor($"There is no available '{parameteres[GetParameter]}' command");
                    helpService.PrintHelp("get");
                }
            }
        }

        private void GetByCity(string city)
        {
            if (string.IsNullOrWhiteSpace(city))
            {
                GetOrderedCities();
            }
            else
            {
                var locations = context.Locations
                                .AsEnumerable()
                                .Where(i => FindCityExpression(i.Name, city))
                                .ToArray();

                if (locations.Length == 0)
                {
                    CustomConsole.WriteLineRedColor($"There is no location with '{city}' city");
                }
                else
                {
                    foreach (var location in locations)
                    {
                        CustomConsole.Print(location);
                    }
                }
            }
        }

        private bool FindCityExpression(string name, string city)
        {
            const int CityIndex = 1;
            var nameParametres = name.Split(new char[] { ' ' });

            return nameParametres.Length > 1 ?
                string.Equals(nameParametres[CityIndex], city, StringComparison.InvariantCultureIgnoreCase)
                : false;
        }

        private void GetByAddress(string locationAddress)
        {
            if (string.IsNullOrWhiteSpace(locationAddress))
            {
                GetOrderedAddresses();
            }
            else
            {
                var locations = context.Locations.Where(i => i.Address == locationAddress).ToArray();
                if (locations.Length == 0)
                {
                    CustomConsole.WriteLineRedColor($"There is no location with '{locationAddress}' address");
                }
                else
                {
                    CustomConsole.Print(locations[0]);
                }
            }
        }

        private void GetById(string locationId)
        {
            var success = int.TryParse(locationId, out int id);
            if (!success)
            {
                CustomConsole.WriteLineRedColor("id - must be a number");
            }

            var locations = context.Locations.Where(i => i.Id == id).ToArray();
            if (locations.Length == 0)
            {
                CustomConsole.WriteLineRedColor($"There is no location with '{locationId}' Id");
            }
            else
            {
                CustomConsole.Print(locations[0]);
            }
        }

        private void GetAll(string[] args = null)
        {
            var locations = context.Locations.OrderBy(i => i.Id).ToArray();
            if (locations.Length == 0)
            {
                CustomConsole.WriteLineRedColor("There is no locations");
            }
            else
            {
                foreach (var location in locations)
                {
                    CustomConsole.Print(location);
                }
            }
        }

        private void Exit(string[] args = null)
        {
            context.Dispose();
            IsRunning = false;
            CustomConsole.WriteLineGreenColor("Good bye");
        }

        private void GetOrderedCities()
        {
            var locations = context.Locations.OrderBy(i => i.Name).ToArray();
            foreach (var location in locations)
            {
                CustomConsole.Print(location);
            }
        }

        private void GetOrderedAddresses()
        {
            var locations = context.Locations.OrderBy(i => i.Address).ToArray();
            foreach (var location in locations)
            {
                CustomConsole.Print(location);
            }
        }

        private void PrintHelp(string[] command = null)
        {
            if (command == null)
            {
                helpService.PrintHelp();
            }
            else
            {
                helpService.PrintHelp(command[0]);
            }
        }
    }
}
