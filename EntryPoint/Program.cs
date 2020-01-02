using System;
using System.Threading.Tasks;
using DataAccessLayer;
using Services;
using CustomConsole = Services.ConsoleService.ConsoleExtension;

namespace EntryPoint
{
    /// <summary>
    /// Represents entry program point.
    /// </summary>
    internal class Program
    {
        private static CommandService commandHandler;

        private static void Main(string[] args)
        {
            CustomConsole.WriteLineGreenColor("Connecting to data base..");
            commandHandler = new CommandService(new ApplicationDbContext());
            Console.WriteLine("Hello!");
            Console.WriteLine("Enter the 'help' to get a help");
            do
            {
                Console.WriteLine();
                CustomConsole.Symbol();
                commandHandler.Initialize(commandLine: Console.ReadLine());
            }
            while (commandHandler.IsRunning);
        }
    }
}
