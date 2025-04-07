namespace Pedalo.UI.Api.Extensions
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// Extensions for adding strongly typed options to the service collection.
    /// </summary>
    internal static class ServiceCollectionOptionsExtensions
    {
        /// <summary>
        /// Adds the strongly typed options.
        /// </summary>
        /// <typeparam name="TOptions">The type of the options.</typeparam>
        /// <param name="services">The services.</param>
        /// <param name="configuration">The configuration.</param>
        /// <exception cref="ArgumentNullException">configuration</exception>
        internal static void AddOptions<TOptions>(this IServiceCollection services, IConfiguration configuration)
            where TOptions : class
        {
            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            services.Configure<TOptions>(configuration.GetSection<TOptions>());
        }
    }
}
