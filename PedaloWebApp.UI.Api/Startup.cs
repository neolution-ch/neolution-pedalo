namespace PedaloWebApp.UI.Api
{
    using FluentValidation.AspNetCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Localization;
    using PedaloWebApp.Infrastructure.Database;
    using PedaloWebApp.UI.Api.Localization;
    using PedaloWebApp.UI.Api.Models.NamespaceRoutingConvention;
    using PedaloWebApp.UI.Api.Options.LanguageConfigOptions;
    using PedaloWebApp.UI.Api.SchemaProcessors;
    using PedaloWebApp.UI.Api.Services;

    /// <summary>
    /// The ASP.NET Core startup class.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Startup" /> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        /// <summary>
        /// Gets the configuration.
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Configures the services.
        /// </summary>
        /// <param name="services">The services.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDistributedMemoryCache();
            services.AddLocalization();
            services.AddSingleton<IStringLocalizerFactory, JsonStringLocalizerFactory>();
            services.AddSingleton(sp => sp.GetRequiredService<IStringLocalizerFactory>().Create(string.Empty, string.Empty));

            services.AddHttpContextAccessor();

            services.AddOptions<SiteConfig>(this.Configuration);
            services.AddOptions<LanguageConfig>(this.Configuration);

            services
                .AddControllers(options =>
                {
                    options.UseNamespaceRouteToken();
                    options.UseKebabCaseController();
                    options.UseBaseRouteToken("Endpoints", "api");
                    options.Conventions.Add(new NamespaceRoutingConvention());
                })
                .AddFluentValidation(fv =>
                {
                    fv.RegisterValidatorsFromAssemblyContaining<Startup>();
                    fv.DisableDataAnnotationsValidation = true;
                    fv.ImplicitlyValidateChildProperties = false;
                })
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.AddDateOnlyConverters();
                });

            services.AddOpenApiDocument(document =>
            {
                document.DocumentName = "v2";
                document.UseApiEndpoints();

                // https://github.com/RicoSuter/NSwag/issues/3110
                document.SchemaSettings.SchemaProcessors.Add(new MarkAsRequiredIfNonNullableSchemaProcessor());
            });

            services.AddDbContextAndFactory<AppDbContext, AppDbContextFactory>(factoryLifetime: ServiceLifetime.Scoped);

            services.AddSingleton<IGenerateKeyFromObject, GenerateKeyFromObject>();
            services.AddScoped<IDatabaseMigrator, DatabaseMigrator>();

            // Add database context factory and make sure in case the AppDbContext class is requested from DI, it is instantiated through the factory.
            services.AddDbContextFactory<AppDbContext, AppDbContextFactory>(lifetime: ServiceLifetime.Scoped);
            services.AddScoped(p => p.GetRequiredService<IDbContextFactory<AppDbContext>>().CreateDbContext());
        }
    }
}
