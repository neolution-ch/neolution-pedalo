namespace PedaloWebApp.Tools.DataInitializer.Seeds
{
    using Microsoft.EntityFrameworkCore;
    using Neolution.Extensions.DataSeeding.Abstractions;

    /// <inheritdoc />
    public class Bookings : ISeed
    {
        /// <summary>
        /// The context factory
        /// </summary>
        private readonly IDbContextFactory<AppDbContext> contextFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="Bookings" /> class.
        /// </summary>
        /// <param name="contextFactory">The context factory.</param>
        public Bookings(IDbContextFactory<AppDbContext> contextFactory)
        {
            this.contextFactory = contextFactory;
        }

        /// <inheritdoc />
        public Type DependsOn => typeof(Pedalos);

        /// <inheritdoc />
        public async Task SeedAsync()
        {
            var now = DateTime.UtcNow;
            var bookings = new List<Booking>
            {
                new() { CustomerId = Customers.AddedCustomers[0].CustomerId, PedaloId = Pedalos.AddedPedalos[1].PedaloId, StartDate = now.AddHours(-8), EndDate = now.AddHours(-7) },
                new() { CustomerId = Customers.AddedCustomers[1].CustomerId, PedaloId = Pedalos.AddedPedalos[0].PedaloId, StartDate = now.AddHours(-7.5), EndDate = now.AddHours(-6.83) },
                new() { CustomerId = Customers.AddedCustomers[2].CustomerId, PedaloId = Pedalos.AddedPedalos[2].PedaloId, StartDate = now.AddHours(-6), EndDate = now.AddHours(-5) },
                new() { CustomerId = Customers.AddedCustomers[3].CustomerId, PedaloId = Pedalos.AddedPedalos[1].PedaloId, StartDate = now.AddHours(-4.75), EndDate = now.AddHours(-4) },
                new() { CustomerId = Customers.AddedCustomers[4].CustomerId, PedaloId = Pedalos.AddedPedalos[2].PedaloId, StartDate = now.AddHours(-4.5), EndDate = now.AddHours(-3.92) },
                new() { CustomerId = Customers.AddedCustomers[5].CustomerId, PedaloId = Pedalos.AddedPedalos[2].PedaloId, StartDate = now.AddHours(-3.5), EndDate = now.AddHours(-2) },
                new() { CustomerId = Customers.AddedCustomers[5].CustomerId, PedaloId = Pedalos.AddedPedalos[0].PedaloId, StartDate = now.AddHours(-1.92), EndDate = now.AddHours(-1) },
                new() { CustomerId = Customers.AddedCustomers[6].CustomerId, PedaloId = Pedalos.AddedPedalos[0].PedaloId, StartDate = now.AddHours(-0.75), EndDate = now.AddHours(-0.08) },
                new() { CustomerId = Customers.AddedCustomers[1].CustomerId, PedaloId = Pedalos.AddedPedalos[2].PedaloId, StartDate = now.AddHours(-0.5), EndDate = now.AddHours(0.5) },
                new() { CustomerId = Customers.AddedCustomers[7].CustomerId, PedaloId = Pedalos.AddedPedalos[1].PedaloId, StartDate = now.AddHours(-0.25), EndDate = null },
            };

            await using var context = await this.contextFactory.CreateDbContextAsync();
            context.Bookings.AddRange(bookings);
            await context.SaveChangesAsync();
        }
    }
}
