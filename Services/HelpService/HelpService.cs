using System;
using Services.ConsoleColorService;

namespace Services.HelpService
{
    /// <summary>
    /// Represents an application help service.
    /// </summary>
    public class HelpService
    {
        private const int CommandHelpIndex = 0;
        private const int DescriptionHelpIndex = 1;
        private const int ExplanationHelpIndex = 2;

        /// <summary>
        /// Initializes a new instance of the <see cref="HelpService"/> class.
        /// </summary>
        public HelpService()
        {
            _consoleColor = new CustomConsoleColor();
            this.HelpMessages = new string[][]
            {
                new string[] { "help", "print the help screen", "The 'help' command prints the help screen." },
                new string[] { "list", "shows records", "The 'list' command shows the records." },
                new string[] { "exit", "exits the application", "The 'exit' command exits the application." },
            };
        }

        /// <summary>
        /// Gets help messages list.
        /// </summary>
        public string[][] HelpMessages { get; }

        private CustomConsoleColor _consoleColor;

        /// <summary>
        /// Prints help message for command.
        /// </summary>
        /// <param name="command">command that needs explanation.</param>
        public void PrintHelp(string command)
        {
            var index = Array.FindIndex(
                this.HelpMessages,
                i => string.Equals(i[CommandHelpIndex], command, StringComparison.InvariantCultureIgnoreCase));

            if (index >= 0)
            {
                Console.WriteLine(this.HelpMessages[index][ExplanationHelpIndex]);
            }
            else
            {
                _consoleColor.WriteLineRedColor($"There is no explanation for '{command}' command.");
            }
        }

        /// <summary>
        /// Prints help messages.
        /// </summary>
        public void PrintHelp()
        {
            _consoleColor.WriteLineGreenColor("Available commands:");
            foreach (var helpMessage in this.HelpMessages)
            {
                _consoleColor.WriteYellowColor($"\t {helpMessage[CommandHelpIndex]}");
                Console.WriteLine(" - \t{0}", helpMessage[DescriptionHelpIndex]);
            }
        }
    }
}
