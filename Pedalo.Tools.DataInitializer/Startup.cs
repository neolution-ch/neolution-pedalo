namespace Pedalo.Tools.DataInitializer
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Neolution.Extensions.DataSeeding;
    using Pedalo.Infrastructure.Database;
    using Pedalo.Tools.DataInitializer.Initializers;

    /// <summary>
    /// The startup class.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// </summary>
        /// <param name="environment">The environment.</param>
        /// <param name="configuration">The configuration.</param>
        public Startup(IHostEnvironment environment, IConfiguration configuration)
        {
            this.Environment = environment;
            this.Configuration = configuration;
        }

        /// <summary>
        /// Gets the environment.
        /// </summary>
        public IHostEnvironment Environment { get; }

        /// <summary>
        /// Gets the configuration.
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Configures the services.
        /// </summary>
        /// <param name="services">The services.</param>
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<AppDbContextInitializer>();

            services.AddDbContextAndFactory<AppDbContext, AppDbContextFactory>(factoryLifetime: ServiceLifetime.Scoped);

            services.AddDataSeeding(typeof(Startup).Assembly);
        }
    }
}
