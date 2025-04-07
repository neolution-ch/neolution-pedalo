namespace Pedalo.Tools.DataInitializer.Seeds
{
    using Microsoft.EntityFrameworkCore;
    using Neolution.Extensions.DataSeeding.Abstractions;
    using Pedalo.Core.Domain.Entities;
    using Pedalo.Core.Domain.Enums;

    /// <inheritdoc />
    public class Pedalos : ISeed
    {
        /// <summary>
        /// The context factory
        /// </summary>
        private readonly IDbContextFactory<AppDbContext> contextFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="Pedalos" /> class.
        /// </summary>
        /// <param name="contextFactory">The context factory.</param>
        public Pedalos(IDbContextFactory<AppDbContext> contextFactory)
        {
            this.contextFactory = contextFactory;
        }

        /// <inheritdoc />
        public Type DependsOn => typeof(Customers);

        /// <summary>
        /// Gets the list of added pedalos.
        /// </summary>
        internal static List<Pedalo> AddedPedalos { get; } = [];

        /// <inheritdoc />
        public async Task SeedAsync()
        {
            var pedalo1 = new Pedalo
            {
                Name = "Heart of Gold",
                Capacity = 6,
                Color = PedaloColor.Blue,
                HourlyRate = 21.00m,
            };
            AddedPedalos.Add(pedalo1);

            var pedalo2 = new Pedalo
            {
                Name = "Golgafrincham",
                Capacity = 2,
                Color = PedaloColor.Pink,
                HourlyRate = 10.00m,
            };
            AddedPedalos.Add(pedalo2);

            var pedalo3 = new Pedalo
            {
                Name = "Millennium Falcon",
                Capacity = 4,
                Color = PedaloColor.Green,
                HourlyRate = 17.00m,
            };
            AddedPedalos.Add(pedalo3);

            await using var context = await this.contextFactory.CreateDbContextAsync();
            context.Pedalos.AddRange(AddedPedalos);
            await context.SaveChangesAsync();
        }
    }
}
