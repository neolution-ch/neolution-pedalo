namespace PedaloWebApp.UI.Api.Models.Translations
{
    /// <summary>
    /// Represents a single translation
    /// </summary>
    public class TranslationItem
    {
        /// <summary>
        /// Gets or sets the translation code identifier
        /// This has to be string in order that it does not depend on the number behind
        /// the enum. That would cause wrong translation if a new TranslationCodeId is added
        /// </summary>
        public string Id { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the translated text.
        /// </summary>
        public string Text { get; set; } = string.Empty;
    }
}
