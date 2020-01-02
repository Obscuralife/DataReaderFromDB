using System;
using CustomConsole = Services.ConsoleService.ConsoleExtension;

namespace Services
{
    /// <inheritdoc/>
    public class HelpService : IHelpService
    {
        private const int CommandHelpIndex = 0;
        private const int DescriptionHelpIndex = 1;
        private const int ExplanationHelpIndex = 2;

        /// <summary>
        /// Initializes a new instance of the <see cref="HelpService"/> class.
        /// </summary>
        public HelpService()
        {
            HelpMessages = new string[][]
            {
                new string[] { "help", "prints the help screen", "The 'help' command prints the help screen." },
                new string[] { "exit", "exits the application", "The 'exit' command exits the application." },
                new string[]
                {
                    "get ", "gets locations", "The 'get' returns all locations.\n" +
                                                         "The 'get -id #' returns location by id\n" +
                                                         "The 'get -address #' returns location by address\n" +
                                                         "The 'get -city # returns locations by city",
                },
            };
        }

        /// <inheritdoc/>
        public string[][] HelpMessages { get; }

        /// <inheritdoc/>
        public void PrintHelp(string command)
        {
            var commandIndex = Array.FindIndex(
                HelpMessages,
                i => string.Equals(i[CommandHelpIndex].Trim(' '), command, StringComparison.InvariantCultureIgnoreCase));

            if (commandIndex >= 0)
            {
                Console.WriteLine(HelpMessages[commandIndex][ExplanationHelpIndex]);
            }
            else
            {
                CustomConsole.WriteLineRedColor($"There is no explanation for '{command}' command.");
            }
        }

        /// <inheritdoc/>
        public void PrintHelp()
        {
            CustomConsole.WriteLineGreenColor("Available commands:");
            foreach (var helpMessage in HelpMessages)
            {
                CustomConsole.WriteYellowColor($"\t {helpMessage[CommandHelpIndex]}");
                Console.WriteLine(" - \t{0}", helpMessage[DescriptionHelpIndex]);
            }
        }
    }
}
