namespace Pedalo.UI.Api.Endpoints.Translations
{
    /// <summary>
    /// The List Supported Languages Result
    /// </summary>
    public class ListSupportedLanguagesResult
    {
        /// <summary>
        /// Gets or sets the code.
        /// </summary>
        public string Code { get; set; } = default!;

        /// <summary>
        /// Gets or sets the display name.
        /// </summary>
        public string DisplayName { get; set; } = default!;
    }
}
