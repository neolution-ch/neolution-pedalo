namespace PedaloWebApp.Infrastructure.Data
{
    using System.Reflection;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using PedaloWebApp.Core.Domain;
    using PedaloWebApp.Core.Interfaces.Data;

    public class DbContextFactory : IDbContextFactory
    {
        private static readonly Assembly ConfigurationsAssembly = typeof(DbContextFactory).Assembly;
        private readonly DbContextOptionsBuilder<PedaloContext> optionsBuilder;

        public DbContextFactory(IConfiguration configuration)
        {
            this.optionsBuilder = new DbContextOptionsBuilder<PedaloContext>();
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            this.optionsBuilder.UseSqlServer(connectionString);
        }

        /// <inheritdoc/>
        public PedaloContext CreateContext()
        {
            return new PedaloContext(this.optionsBuilder.Options, ConfigurationsAssembly);
        }

        /// <inheritdoc/>
        public PedaloContext CreateReadOnlyContext()
        {
            var context = new PedaloContext(this.optionsBuilder.Options, ConfigurationsAssembly);
            context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            return context;
        }
    }
}
