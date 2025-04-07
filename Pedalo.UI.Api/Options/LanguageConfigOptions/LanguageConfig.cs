namespace Pedalo.UI.Api.Options.LanguageConfigOptions
{
    /// <summary>
    /// The lanuage config
    /// </summary>
    public class LanguageConfig
    {
        /// <summary>
        /// Gets or sets the default language.
        /// </summary>
        public string DefaultLanguage { get; set; } = default!;

        /// <summary>
        /// Gets or sets the supported languages.
        /// </summary>
        public IList<SupportedLanguage> SupportedLanguages { get; set; } = default!;
    }
}
