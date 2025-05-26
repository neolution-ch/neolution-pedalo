namespace PedaloWebApp.UI.Api.Endpoints.Translations
{
    using System.Globalization;
    using Microsoft.Extensions.Localization;
    using Microsoft.Extensions.Options;
    using PedaloWebApp.UI.Api.Models.Translations;
    using PedaloWebApp.UI.Api.Options.LanguageConfigOptions;
    using PedaloWebApp.UI.Api.Services;

    /// <inheritdoc/>
    [AllowAnonymous]
    public class GetTranslations
        : EndpointBaseSync.WithoutRequest.WithActionResult<Dictionary<string, TranslationResultModel>>
    {
        /// <summary>
        /// The key from object generator.
        /// </summary>
        private readonly IGenerateKeyFromObject generateKeyFromObject;

        /// <summary>
        /// The localizer
        /// </summary>
        private readonly IStringLocalizer localizer;

        /// <summary>
        /// The language config
        /// </summary>
        private readonly LanguageConfig languageConfig;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetTranslations" /> class.
        /// </summary>
        /// <param name="generateKeyFromObject">The generate key from object.</param>
        /// <param name="localizer">The localizer.</param>
        /// <param name="languageConfig">The language config.</param>
        public GetTranslations(
            IGenerateKeyFromObject generateKeyFromObject,
            IStringLocalizer localizer,
            IOptions<LanguageConfig> languageConfig)
        {
            if (languageConfig is null)
            {
                throw new ArgumentNullException(nameof(languageConfig));
            }

            this.generateKeyFromObject = generateKeyFromObject;
            this.localizer = localizer;
            this.languageConfig = languageConfig.Value;
        }

        /// <summary>
        /// Gets the translations for the specified language.
        /// </summary>
        /// <returns>The translations.</returns>
        [HttpGet("/[baseroute]")]
        [AllowAnonymous]
        public override ActionResult<Dictionary<string, TranslationResultModel>> Handle()
        {
            var result = new Dictionary<string, TranslationResultModel>();

            foreach (var langCode in this.languageConfig.SupportedLanguages.Select(x => x.Code))
            {
                CultureInfo.CurrentCulture = new CultureInfo(langCode);
                var translations = this.localizer
                                    .GetAllStrings()
                                    .Select(x => new TranslationItem { Id = x.Name, Text = x.Value, });

                result.Add(langCode, new TranslationResultModel
                {
                    Translations = translations,
                    Hash = this.generateKeyFromObject.GenerateKey(translations),
                });
            }

            return result;
        }
    }
}
