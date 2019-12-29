using System;
using System.Collections.Generic;
using System.Text;

namespace Services.ConsoleColorService
{
    public class CustomConsoleColor
    {
        public void Symbol()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(">>> ");
            Console.ResetColor();
        }

        public void WriteLineGreenColor(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(message);
            Console.ResetColor();
        }
        public void WriteLineRedColor(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ResetColor();
        }
        public void WriteYellowColor(string message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(message);
            Console.ResetColor();
        }
    }
}
