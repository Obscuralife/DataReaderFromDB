using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccessLayer.Models
{
    public class LocationConfiguration : IEntityTypeConfiguration<Location>
    {
        public void Configure(EntityTypeBuilder<Location> builder)
        {
            builder.HasKey(i => i.Id);
            builder.HasAlternateKey(i => i.Address);
            builder.Property(i => i.Name).HasColumnName("Name").HasMaxLength(60).IsRequired();
            builder.Property(i => i.Address).HasColumnName("Email_address").HasMaxLength(60).IsRequired();
        }
    }
}
