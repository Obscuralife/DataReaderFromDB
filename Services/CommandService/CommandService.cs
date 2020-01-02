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
    public class CommandService
    {
        private const int CommandIndex = 0;
        private readonly IHelpService helpService;
        private readonly Tuple<string, Action<string[]>>[] commands;
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
            if (parameteres == null)
            {
                GetAll();
            }
            else
            {
                const int GetParameter = 0;
                const int GetParameterValue = 1;
                var getCommands = new Tuple<string, Action<string>>[]
                {
                    new Tuple<string, Action<string>>("-id", GetById),
                    new Tuple<string, Action<string>>("-address", GetByAddress),
                    new Tuple<string, Action<string>>("-city", GetByCity),
                };

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
                    helpService.PrintHelp("get");
                }
            }
        }

        private void GetByCity(string city)
        {
            var locations = context.Locations.Where(i => i.Name.Contains(city)).ToArray();
            if (locations.Length == 0)
            {
                CustomConsole.WriteLineRedColor($"There is no location with '{city}' city");
            }
            else
            {
                foreach (var location in locations)
                {
                    Print(location);
                }
            }
        }

        private void GetByAddress(string locationAddress)
        {
            var locations = context.Locations.Where(i => i.Address == locationAddress).ToArray();
            if (locations.Length == 0)
            {
                CustomConsole.WriteLineRedColor($"There is no location with '{locationAddress}' address");
            }
            else
            {
                Print(locations[0]);
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
                Print(locations[0]);
            }
        }

        private void GetAll(string[] obj = null)
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
                    Print(location);
                }
            }
        }

        private void Exit(string[] obj = null)
        {
            context.Dispose();
            IsRunning = false;
            CustomConsole.WriteLineGreenColor("Good bye");
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

        private void Print(Location location)
        {
            CustomConsole.WriteYellowColor("ID:     \t");
            CustomConsole.WriteLineGreenColor($"{location.Id}");
            CustomConsole.WriteYellowColor("Name:   \t");
            CustomConsole.WriteLineGreenColor($"{location.Name}");
            CustomConsole.WriteYellowColor("Address:\t");
            CustomConsole.WriteLineGreenColor($"{location.Address}");
        }
    }
}
