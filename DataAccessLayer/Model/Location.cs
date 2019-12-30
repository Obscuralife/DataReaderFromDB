namespace DataAccessLayer.Models
{
    /// <inheritdoc/>
    public class Location : ILocation
    {
        /// <inheritdoc/>
        public int? Id { get; set; }

        /// <inheritdoc/>
        public string Name { get; set; }

        /// <inheritdoc/>
        public string Address { get; set; }
    }
}
