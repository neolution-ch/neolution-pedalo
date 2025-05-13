namespace PedaloWebApp.UI.Api.Endpoints.Errors
{
    /// <summary>
    /// The Error Request
    /// </summary>
    public class ThrowErrorRequest
    {
        /// <summary>
        /// Gets or sets the code.
        /// </summary>
        [FromRoute]
        public int Code { get; set; }

        /// <summary>
        /// Gets or sets the error message.
        /// </summary>
        [FromQuery]
        public string ErrorMessage { get; set; } = string.Empty;
    }
}
