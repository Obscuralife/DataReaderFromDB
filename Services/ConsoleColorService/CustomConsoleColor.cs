using System;

namespace Services.ConsoleColorService
{
    /// <summary>
    /// Represents custom color IO.
    /// </summary>
    public class CustomConsoleColor
    {
        /// <summary>
        /// Prints special symbol.
        /// </summary>
        public void Symbol()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(">>> ");
            Console.ResetColor();
        }

        /// <summary>
        /// Write line green color message.
        /// </summary>
        /// <param name="message">.</param>
        public void WriteLineGreenColor(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        /// <summary>
        /// Write line red color message.
        /// </summary>
        /// <param name="message">.</param>
        public void WriteLineRedColor(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        /// <summary>
        /// Write yellow color message.
        /// </summary>
        /// <param name="message">.</param>
        public void WriteYellowColor(string message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(message);
            Console.ResetColor();
        }
    }
}
