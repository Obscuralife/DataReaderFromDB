using System;
using Services.ConsoleColorService;
using Services.HelpService;

namespace EntryPoint
{
    internal class Program
    {
        private static readonly HelpService HelpService = new HelpService();
        private static CustomConsoleColor ConsoleColor = new CustomConsoleColor();

        private static void Main(string[] args)
        {
            ConsoleColor.Symbol();
            Console.WriteLine("Hello World!");
            HelpService.PrintHelp();
        }
    }
}
