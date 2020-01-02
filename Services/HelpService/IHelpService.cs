namespace Services
{
    /// <summary>
    /// Represents an application help service.
    /// </summary>
    public interface IHelpService
    {
        /// <summary>
        /// Gets help messages list.
        /// </summary>
        string[][] HelpMessages { get; }

        /// <summary>
        /// Prints help messages.
        /// </summary>
        void PrintHelp();

        /// <summary>
        /// Prints help message for command.
        /// </summary>
        /// <param name="command">command that needs explanation.</param>
        void PrintHelp(string command);
    }
}