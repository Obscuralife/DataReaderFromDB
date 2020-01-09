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
        private static ICommandService commandHandler;

        private static async Task Main(string[] args)
        {
            CustomConsole.WriteLineGreenColor("Connecting to data base..");
            commandHandler = new CommandService(new ApplicationDbContext());
            Console.Clear();
            Console.WriteLine("Hello!");
            Console.WriteLine("Enter the 'help' to get a help");
            if (args.Length > 0)
            {
                await InvokeFromCommandWindowAsync(args);
            }
            else
            {
                await InvokeFromApplicationAsync();
            }
        }

        private static async Task InvokeFromApplicationAsync()
        {
            do
            {
                Console.WriteLine();
                CustomConsole.Symbol();
                var locations = await commandHandler.ParseComandLine(commandLine: Console.ReadLine());
                CustomConsole.Print(locations);
            }
            while (commandHandler.IsRunning);
        }

        private static async Task InvokeFromCommandWindowAsync(string[] args)
        {
            Console.WriteLine();
            CustomConsole.Symbol();
            var commandLine = string.Join(' ', args);
            var locations = await commandHandler.ParseComandLine(commandLine);
            CustomConsole.Print(locations);

            if (commandHandler.IsRunning)
            {
                await InvokeFromApplicationAsync();
            }
        }
    }
}
