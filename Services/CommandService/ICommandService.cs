namespace Services
{
    /// <summary>
    /// Represents a service for works with commands.
    /// </summary>
    public interface ICommandService
    {
        /// <summary>
        /// Gets a value indicating whether application is running.
        /// </summary>
        bool IsRunning { get; }

        /// <summary>
        /// Processes input.
        /// </summary>
        /// <param name="commandLine">.</param>
        void Initialize(string commandLine);
    }
}