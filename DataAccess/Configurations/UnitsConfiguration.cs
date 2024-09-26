using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using DataAccess.Models;

namespace DataAccess.Configurations
{
    public class UnitsConfiguration : IEntityTypeConfiguration<Unit>
    {
        public void Configure(EntityTypeBuilder<Unit> builder)
        {
            builder.HasMany(e => e.Users)
            .WithOne(e => e.Unit)
            .HasForeignKey(e => e.UnitId)
            .HasPrincipalKey(e => e.Id);
        }
    }
}
