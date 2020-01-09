using System.Threading.Tasks;
using DataAccessLayer.Models;

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
        /// <param name="commandLine">execution command line.</param>
        /// <returns>Collection of 'location' entities.</returns>
        Task<Location[]> ParseComandLine(string commandLine);
    }
}