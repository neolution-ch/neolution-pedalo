namespace PedaloWebApp.Core.Models.OAuth
{
    using System.Net;

    /// <summary>
    /// The response for an OAuth grant request.
    /// </summary>
    public class OAuthResponse
    {
        /// <summary>
        /// Gets or sets the status code.
        /// </summary>
        public HttpStatusCode StatusCode { get; set; }

        /// <summary>
        /// Gets or sets the body.
        /// </summary>
        public object? Body { get; set; }
    }
}
