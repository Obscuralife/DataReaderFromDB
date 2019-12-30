using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccessLayer.Models
{
    /// <summary>
    /// Represents custom configuration for location table.
    /// </summary>
    public class LocationConfiguration : IEntityTypeConfiguration<Location>
    {
        /// <summary>
        /// Configure method for location tables.
        /// </summary>
        /// <param name="builder">Provides a simple API for configuring an Microsoft.EntityFrameworkCore.</param>
        public void Configure(EntityTypeBuilder<Location> builder)
        {
            builder.HasKey(i => i.Id);
            builder.HasAlternateKey(i => i.Address);
            builder.Property(i => i.Name).HasColumnName("Name").HasMaxLength(60).IsRequired();
            builder.Property(i => i.Address).HasColumnName("Email_address").HasMaxLength(60).IsRequired();
        }
    }
}
