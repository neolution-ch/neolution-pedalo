namespace PedaloWebApp.UI.Api.Extensions
{
    using System.Globalization;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Localization;
    using PedaloWebApp.UI.Api.Options.LanguageConfigOptions;

    /// <summary>
    /// The localization extension
    /// </summary>
    public static class LocalizationExtensions
    {
        /// <summary>
        /// Uses the custom request localization.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="supportedLanguages">The supported languages.</param>
        /// <param name="defaultLanguage">The default language.</param>
        public static void UseCustomRequestLocalization(this IApplicationBuilder app, IEnumerable<SupportedLanguage> supportedLanguages, string defaultLanguage)
        {
            app.UseRequestLocalization(options =>
            {
                options.RequestCultureProviders = new List<IRequestCultureProvider>
                {
                    new CustomRequestCultureProvider(context =>
                    {
                        var culture = context.Request.Headers["Language"].ToString();
                        var result = new ProviderCultureResult(culture);
#pragma warning disable CS8619 // Nullability of reference types in value doesn't match target type.
                        return Task.FromResult(result);
#pragma warning restore CS8619 // Nullability of reference types in value doesn't match target type.
                    }),
                    new AcceptLanguageHeaderRequestCultureProvider(),
                };

                options.SupportedCultures = supportedLanguages.Select(x => new CultureInfo(x.Code)).ToList();
                options.DefaultRequestCulture = new RequestCulture(defaultLanguage);
                options.SupportedUICultures = options.SupportedCultures;
            });
        }
    }
}
