using System;
using System.Linq;
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
        private static ICommandService commandHandler;

        private static void Main(string[] args)
        {
            CustomConsole.WriteLineGreenColor("Connecting to data base..");
            commandHandler = new CommandService(new ApplicationDbContext());
            Console.Clear();
            Console.WriteLine("Hello!");
            Console.WriteLine("Enter the 'help' to get a help");
            if (args.Length > 0)
            {
                InvokeFromCommandWindow(args);
            }
            else
            {
                InvokeFromApplication();
            }
        }

        private static void InvokeFromApplication()
        {
            do
            {
                Console.WriteLine();
                CustomConsole.Symbol();
                commandHandler.Initialize(commandLine: Console.ReadLine());
            }
            while (commandHandler.IsRunning);
        }

        private static void InvokeFromCommandWindow(string[] args)
        {
            Console.WriteLine();
            CustomConsole.Symbol();
            var commandLine = string.Join(' ', args);
            commandHandler.Initialize(commandLine);

            if (commandHandler.IsRunning)
            {
                InvokeFromApplication();
            }
        }
    }
}
