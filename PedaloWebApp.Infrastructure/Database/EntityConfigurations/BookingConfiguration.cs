namespace PedaloWebApp.Infrastructure.Database.EntityConfigurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    /// <summary>
    /// Configures the Booking entity.
    /// </summary>
    public class BookingConfiguration : IEntityTypeConfiguration<Booking>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<Booking> builder)
        {
            ArgumentNullException.ThrowIfNull(builder);

            builder.HasKey(x => x.BookingId);
        }
    }
}
