using System.Collections.Generic;
using System.Threading.Tasks;
using DataAccessLayer.Models;

namespace Services.HostService
{
    /// <summary>
    /// Data service interface.
    /// </summary>
    public interface IDataService
    {
        /// <summary>
        /// Indicating whether advances the enumerator to the next element of the collection.
        /// </summary>
        /// <returns>.</returns>
        bool MoveNext();

        /// <summary>
        /// Fill collection of deseriazed entities.
        /// </summary>
        /// <param name="dataPath">path to serialized entities.</param>
        void Build(string dataPath);

        /// <summary>
        /// Push local entities to sql data base.
        /// </summary>
        /// <returns><see cref="Task"/>.</returns>
        Task PushEntityToSqlBaseAsync();
    }
}