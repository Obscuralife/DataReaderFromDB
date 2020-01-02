using System;

namespace Services.ConsoleService
{
    /// <summary>
    /// Represents custom color IO.
    /// </summary>
    public static class ConsoleExtension
    {
        /// <summary>
        /// Prints special symbol.
        /// </summary>
        public static void Symbol()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(">>> ");
            Console.ResetColor();
        }

        /// <summary>
        /// Write line green color message.
        /// </summary>
        /// <param name="message">.</param>
        public static void WriteLineGreenColor(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        /// <summary>
        /// Write line red color message.
        /// </summary>
        /// <param name="message">.</param>
        public static void WriteLineRedColor(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        /// <summary>
        /// Write yellow color message.
        /// </summary>
        /// <param name="message">.</param>
        public static void WriteYellowColor(string message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(message);
            Console.ResetColor();
        }

        /// <summary>
        /// Write green color message.
        /// </summary>
        /// <param name="message">.</param>
        public static void WriteGreenColor(string message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(message);
            Console.ResetColor();
        }
    }
}
