namespace PedaloWebApp.UI.Api.Endpoints.Translations
{
    using Microsoft.Extensions.Options;
    using PedaloWebApp.UI.Api.Options.LanguageConfigOptions;

    /// <inheritdoc />
    [AllowAnonymous]
    public class GetDefaultLanguage
        : EndpointBaseSync.WithoutRequest.WithActionResult<string>
    {
        /// <summary>
        /// The language configuration
        /// </summary>
        private readonly LanguageConfig languageConfig;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetDefaultLanguage"/> class.
        /// </summary>
        /// <param name="languageConfig">The language configuration.</param>
        public GetDefaultLanguage(IOptions<LanguageConfig> languageConfig)
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
        [HttpGet("/[baseroute]/[controller]")]
        public override ActionResult<string> Handle()
        {
            var result = this.languageConfig.DefaultLanguage;

            return result;
        }
    }
}
