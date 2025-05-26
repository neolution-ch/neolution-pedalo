namespace PedaloWebApp.UI.Api
{
    using NLog.Web;
    using PedaloWebApp.UI.Api.Middleware.UnhandledExceptionLogger;
    using PedaloWebApp.UI.Api.Options.LanguageConfigOptions;
    using QuestPDF.Infrastructure;

    /// <summary>
    /// The main program class that contains the entry point of the application.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// Defines the entry point of the application.
        /// </summary>
        /// <param name="args">The arguments.</param>
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Configure logging to use NLog exclusively
            builder.Logging.ClearProviders();
            builder.Host.UseNLog();

            // Set the application's URLs based on the "PORT" environment variable
            var port = Environment.GetEnvironmentVariable("PORT");
            if (!string.IsNullOrEmpty(port))
            {
                builder.WebHost.UseUrls($"http://+:{port}");
            }

            builder.AddGoogleSecrets();

            var startup = new Startup(builder.Configuration);
            startup.ConfigureServices(builder.Services);

            var app = builder.Build();

            // Build but do not run the application if it is being called by NSwag
            if (app.Environment.EnvironmentName.Equals("NSwag", StringComparison.Ordinal))
            {
                return;
            }

            // Create service provide to resolve services
            using var scope = app.Services.CreateScope();
            var serviceProvider = scope.ServiceProvider;

            // Run database migrator
            serviceProvider.GetRequiredService<IDatabaseMigrator>().Run();

            // Configure the HTTP request pipeline.
            var siteConfig = app.Configuration.GetOptions<SiteConfig>();
            var languageConfig = app.Configuration.GetOptions<LanguageConfig>();

            app.UseCors(
                x =>
                    x.WithOrigins(siteConfig.ClientBaseUrl, "http://whitelabel.ui.local")
                        .SetPreflightMaxAge(new TimeSpan(hours: 0, minutes: 30, seconds: 0))
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials());

            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseOpenApi();
                app.UseSwaggerUi();
            }
            else
            {
                app.UseUnhandledExceptionsLogger();
                app.UseHttpsRedirection();
                app.UseHsts();
            }

            app.UseRouting();

            app.UseCustomRequestLocalization(languageConfig.SupportedLanguages, languageConfig.DefaultLanguage);

            app.MapControllers();

            app.UseMiddleware<NLogRequestPostedBodyMiddleware>();

            // Quest PDF License
            QuestPDF.Settings.License = LicenseType.Community;

            app.Run();
        }
    }
}
