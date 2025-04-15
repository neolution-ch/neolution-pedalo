namespace PedaloWebApp.Tools.DataInitializer.Initializers
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Logging;
    using Neolution.Extensions.DataSeeding.Abstractions;

    /// <inheritdoc />
    public class AppDbContextInitializer : DbContextInitializer<AppDbContext>
    {
        /// <summary>
        /// The seeder
        /// </summary>
        private readonly ISeeder seeder;

        /// <summary>
        /// Initializes a new instance of the <see cref="AppDbContextInitializer" /> class.
        /// </summary>
        /// <param name="contextFactory">The context factory.</param>
        /// <param name="logger">The logger.</param>
        /// <param name="seeder">The seeder.</param>
        /// <param name="configuration">The configuration.</param>
        public AppDbContextInitializer(IDbContextFactory<AppDbContext> contextFactory, ILogger<AppDbContextInitializer> logger, ISeeder seeder, IConfiguration configuration)
            : base(contextFactory, logger, configuration)
        {
            this.seeder = seeder;
        }

        /// <inheritdoc />
        protected async override Task SeedAsync()
        {
            this.Logger.LogDebug("Seeding...");

            await this.seeder.SeedAsync().ConfigureAwait(false);
        }
    }
}
