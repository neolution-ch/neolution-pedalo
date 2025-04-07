namespace Pedalo.UI.Api.Endpoints.Translations
{
    using Microsoft.Extensions.Localization;

    /// <inheritdoc />
    [AllowAnonymous]
    public class GetText : EndpointBaseSync.WithRequest<TranslationCodeId>.WithActionResult<string>
    {
        /// <summary>
        /// The string localizer
        /// </summary>
        private readonly IStringLocalizer stringLocalizer;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetText"/> class.
        /// </summary>
        /// <param name="stringLocalizer">The string localizer.</param>
        public GetText(IStringLocalizer stringLocalizer)
        {
            this.stringLocalizer = stringLocalizer;
        }

        /// <summary>
        /// Gets the translation text of the specified identifier and for the specified language.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>The translation text.</returns>
        [HttpGet("/[baseroute]/[controller]")]
        public override ActionResult<string> Handle(TranslationCodeId request)
        {
            return this.stringLocalizer.Text(request);
        }
    }
}
