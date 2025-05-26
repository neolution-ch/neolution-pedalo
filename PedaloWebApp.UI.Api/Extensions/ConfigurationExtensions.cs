namespace PedaloWebApp.UI.Api.Extensions
{
    using Microsoft.Extensions.Configuration;

    /// <summary>
    /// Extensions for getting strongly typed options from configuration.
    /// </summary>
    internal static class ConfigurationExtensions
    {
        /// <summary>
        /// Gets the strongly typed options.
        /// </summary>
        /// <typeparam name="TOptions">The options type.</typeparam>
        /// <param name="config">The configuration.</param>
        /// <returns>The options.</returns>
        internal static TOptions GetOptions<TOptions>(this IConfiguration config)
        {
            if (config == null)
            {
                throw new ArgumentNullException(nameof(config));
            }

            var options = config.GetSection(typeof(TOptions).Name).Get<TOptions>();

            if (options is null)
            {
                throw new InvalidOperationException($"Could not find configuration section '{typeof(TOptions).Name}'");
            }

            return options;
        }

        /// <summary>
        /// Gets the section by the specified options type.
        /// </summary>
        /// <typeparam name="T">The options type.</typeparam>
        /// <param name="config">The configuration.</param>
        /// <returns>The configuration section.</returns>
        /// <exception cref="ArgumentNullException">config</exception>
        internal static IConfigurationSection GetSection<T>(this IConfiguration config)
        {
            if (config == null)
            {
                throw new ArgumentNullException(nameof(config));
            }

            return config.GetSection(typeof(T).Name);
        }
    }
}
