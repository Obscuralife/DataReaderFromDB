using System;
using Services;

namespace EntryPoint
{
    internal class Program
    {
        private static readonly HelpService HelpService = new HelpService();

        private static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            HelpService.PrintHelp();
        }
    }
}
