namespace DataAccessLayer.Models
{
    /// <summary>
    /// Represents location entity.
    /// </summary>
    public interface ILocation
    {
        /// <summary>
        /// Gets or sets location address.
        /// </summary>
        string Address { get; set; }

        /// <summary>
        /// Gets or sets lication ID. Can be null.
        /// </summary>
        int? Id { get; set; }

        /// <summary>
        /// Gets or sets location name.
        /// </summary>
        string Name { get; set; }
    }
}