namespace PedaloWebApp.Infrastructure.Data.EntityConfigurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using PedaloWebApp.Core.Domain.Entities;

    public class PedaloConfiguration : IEntityTypeConfiguration<Pedalo>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<Pedalo> builder)
        {
            builder.HasKey(x => x.PedaloId);
            builder.Property(x => x.Name).IsRequired().HasMaxLength(40);

            Seed(builder);
        }

        private static void Seed(EntityTypeBuilder<Pedalo> builder)
        {
            builder.HasData(new Pedalo
            {
                Name = "Heart of Gold",
                Capacity = 6,
                Color = PedaloColor.Blue,
                HourlyRate = 21.00m,
            });

            builder.HasData(new Pedalo
            {
                Name = "Golgafrincham",
                Capacity = 2,
                Color = PedaloColor.Pink,
                HourlyRate = 10.00m,
            });

            builder.HasData(new Pedalo
            {
                Name = "Millennium Falcon",
                Capacity = 4,
                Color = PedaloColor.Green,
                HourlyRate = 17.00m,
            });
        }
    }
}
