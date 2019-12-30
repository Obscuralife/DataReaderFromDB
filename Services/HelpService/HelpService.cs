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
        private CustomConsoleColor consoleColor;

        /// <summary>
        /// Initializes a new instance of the <see cref="HelpService"/> class.
        /// </summary>
        public HelpService()
        {
            consoleColor = new CustomConsoleColor();
            HelpMessages = new string[][]
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

        /// <summary>
        /// Prints help message for command.
        /// </summary>
        /// <param name="command">command that needs explanation.</param>
        public void PrintHelp(string command)
        {
            var index = Array.FindIndex(
                HelpMessages,
                i => string.Equals(i[CommandHelpIndex], command, StringComparison.InvariantCultureIgnoreCase));

            if (index >= 0)
            {
                Console.WriteLine(HelpMessages[index][ExplanationHelpIndex]);
            }
            else
            {
                consoleColor.WriteLineRedColor($"There is no explanation for '{command}' command.");
            }
        }

        /// <summary>
        /// Prints help messages.
        /// </summary>
        public void PrintHelp()
        {
            consoleColor.WriteLineGreenColor("Available commands:");
            foreach (var helpMessage in HelpMessages)
            {
                consoleColor.WriteYellowColor($"\t {helpMessage[CommandHelpIndex]}");
                Console.WriteLine(" - \t{0}", helpMessage[DescriptionHelpIndex]);
            }
        }
    }
}
