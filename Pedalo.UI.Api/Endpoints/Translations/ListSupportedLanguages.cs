namespace Pedalo.UI.Api.Endpoints.Translations
{
    using Microsoft.Extensions.Options;
    using Pedalo.UI.Api.Options.LanguageConfigOptions;

    /// <inheritdoc/>
    [AllowAnonymous]
    public class ListSupportedLanguages
            : EndpointBaseSync.WithoutRequest.WithActionResult<List<ListSupportedLanguagesResult>>
    {
        /// <summary>
        /// The language configuration
        /// </summary>
        private readonly LanguageConfig languageConfig;

        /// <summary>
        /// Initializes a new instance of the <see cref="ListSupportedLanguages"/> class.
        /// </summary>
        /// <param name="languageConfig">The language configuration.</param>
        public ListSupportedLanguages(IOptions<LanguageConfig> languageConfig)
        {
            if (languageConfig is null)
            {
                throw new ArgumentNullException(nameof(languageConfig));
            }

            this.languageConfig = languageConfig.Value;
        }

        /// <summary>
        /// Gets all the language codes.
        /// </summary>
        /// <returns>List of language codes</returns>
        [HttpGet("/[baseroute]/languages")]
        public override ActionResult<List<ListSupportedLanguagesResult>> Handle()
        {
            var result = this.languageConfig.SupportedLanguages.Select(x => new ListSupportedLanguagesResult
            {
                Code = x.Code,
                DisplayName = x.DisplayName,
            }).ToList();

            return result;
        }
    }
}
