namespace PedaloWebApp.UI.Api.Models.Translations
{
    /// <summary>
    /// The translation result model
    /// </summary>
    public class TranslationResultModel
    {
        /// <summary>
        /// Gets or sets the translations.
        /// </summary>
        public IEnumerable<TranslationItem>? Translations { get; set; }

        /// <summary>
        /// Gets or sets the hash.
        /// </summary>
        public string? Hash { get; set; }
    }
}
