namespace PedaloWebApp.Infrastructure.Database
{
    using Microsoft.Extensions.Configuration;

    /// <inheritdoc cref="IAppDbContextFactory" />
    public class AppDbContextFactory : IAppDbContextFactory, IDbContextFactory<AppDbContext>
    {
        /// <summary>
        /// The configuration
        /// </summary>
        private readonly IConfiguration configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="AppDbContextFactory"/> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        public AppDbContextFactory(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        /// <inheritdoc />
        public AppDbContext CreateContext()
        {
            var connectionString = this.configuration.GetConnectionString(nameof(AppDbContext));
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new InvalidOperationException("Connection string not found in configuration");
            }

            return this.CreateDbContextInternal(connectionString);
        }

        /// <inheritdoc />
        public AppDbContext CreateContext(string connectionString)
        {
            return this.CreateDbContextInternal(connectionString);
        }

        /// <inheritdoc />
        public AppDbContext CreateReadOnlyContext()
        {
            var context = this.CreateContext();
            context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            return context;
        }

        /// <inheritdoc />
        public AppDbContext CreateDbContext()
        {
            return this.CreateContext();
        }

        /// <summary>
        /// Creates the context internal.
        /// </summary>
        /// <param name="connectionString">The connection string</param>
        /// <returns>The context</returns>
        private AppDbContext CreateDbContextInternal(string connectionString)
        {
            var dbContextFactoryAssembly = typeof(AppDbContextFactory).Assembly;

            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseSqlServer(connectionString, x => x.MigrationsAssembly(dbContextFactoryAssembly.FullName));

            var config = new AppDbContextConfig
            {
                EntityConfigurationsAssembly = dbContextFactoryAssembly,
            };

            var context = new AppDbContext(optionsBuilder.Options, config);
            context.ChangeTracker.LazyLoadingEnabled = false;

            return context;
        }
    }
}
