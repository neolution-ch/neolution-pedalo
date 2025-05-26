namespace PedaloWebApp.Infrastructure.Database
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;

    /// <summary>
    /// <see cref="IServiceCollection"/> extension methods for setting up EF Core services.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds the EF Core database context and the database context factory.
        /// </summary>
        /// <typeparam name="TDbContext">The type of the database context.</typeparam>
        /// <typeparam name="TDbContextFactory">The type of the database context factory.</typeparam>
        /// <param name="services">The services.</param>
        /// <param name="factoryLifetime">The factory lifetime.</param>
        public static void AddDbContextAndFactory<TDbContext, TDbContextFactory>(this IServiceCollection services, ServiceLifetime factoryLifetime)
            where TDbContext : DbContext
            where TDbContextFactory : class, IDbContextFactory<TDbContext>
        {
            // If the DbContext is requested from DI, make sure it is always instantiated through the factory.
            // This has to done first, because AddDbContextFactory<TDbContext, TDbContextFactory>() tries to register the DbContext as well: https://github.com/dotnet/efcore/issues/25164#issuecomment-894115892
            services.TryAddScoped(p => p.GetRequiredService<IDbContextFactory<TDbContext>>().CreateDbContext());

            // Add the EF database context factory
            services.AddDbContextFactory<TDbContext, TDbContextFactory>(lifetime: factoryLifetime);

            // Add our custom database context factory
            services.AddScoped<IAppDbContextFactory, AppDbContextFactory>();
        }
    }
}
