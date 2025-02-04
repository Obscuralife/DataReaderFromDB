﻿using System;
using DataAccessLayer.Models;

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
        /// <param name="symbol">your custom symbol.</param>
        public static void Symbol(string symbol = ">>> ")
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(symbol);
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

        /// <summary>
        /// Prints each property of <see cref="Location"/>.
        /// </summary>
        /// <param name="location">.</param>
        public static void Print(Location location)
        {
            WriteYellowColor("  ID:     \t");
            WriteLineGreenColor($"{location.Id}");
            WriteYellowColor("  Name:   \t");
            WriteLineGreenColor($"{location.Name}");
            WriteYellowColor("  Address:\t");
            WriteLineGreenColor($"{location.Address}");
            Console.WriteLine();
        }

        /// <summary>
        /// Prints each property of <see cref="Location"/>.
        /// </summary>
        /// <param name="locations">collection of location.</param>
        public static void Print(Location[] locations)
        {
            foreach (var location in locations)
            {
                Print(location);
            }
        }
    }
}
