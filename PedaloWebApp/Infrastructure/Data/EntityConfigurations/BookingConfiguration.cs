namespace PedaloWebApp.Infrastructure.Data.EntityConfigurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using PedaloWebApp.Core.Domain.Entities;

    public class BookingConfiguration : IEntityTypeConfiguration<Booking>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<Booking> builder)
        {
            builder.HasKey(x => x.BookingId);
        }
    }
}
