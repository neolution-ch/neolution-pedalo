namespace Pedalo.Infrastructure.Database.EntityConfigurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Pedalo.Core.Domain.Entities;

    /// <summary>
    /// Configures the Pedalo entity.
    /// </summary>
    public class PedaloConfiguration : IEntityTypeConfiguration<Pedalo>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<Pedalo> builder)
        {
            ArgumentNullException.ThrowIfNull(builder);

            builder.HasKey(x => x.PedaloId);
            builder.Property(x => x.Name).HasMaxLength(40);
            builder.Property(x => x.HourlyRate).HasPrecision(18, 2);
        }
    }
}
