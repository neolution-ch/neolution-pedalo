namespace Pedalo.Infrastructure.Database
{
    using System.IO;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Design;
    using Microsoft.Extensions.Configuration;
    using Pedalo.Core.Domain;

    /// <inheritdoc />
    public class DesignTimeAppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        /// <inheritdoc />
        public AppDbContext CreateDbContext(string[] args)
        {
            // Build config path
            var basePath = Path.Combine(Directory.GetCurrentDirectory(), "..", "Pedalo.UI.Api");

            var configurationRoot = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.Development.json", optional: true)
                .Build();

            // Get connection string (Will be overridden if you use the dotnet ef CLI with the `--connection` parameter)
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            var connectionString = configurationRoot.GetConnectionString($"{nameof(AppDbContext)}Migration");
            optionsBuilder.UseSqlServer(connectionString, b => b.MigrationsAssembly(typeof(DesignTimeAppDbContextFactory).Assembly.FullName));

            return new AppDbContext(optionsBuilder.Options, new AppDbContextConfig
            {
                EntityConfigurationsAssembly = typeof(DesignTimeAppDbContextFactory).Assembly,
                PerformChecks = true,
            });
        }
    }
}
