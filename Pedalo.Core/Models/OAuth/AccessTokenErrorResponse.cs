namespace Pedalo.Core.Models.OAuth
{
    using System.Text.Json.Serialization;

    /// <summary>
    /// Response if the access token request is invalid.
    /// </summary>
    /// <remarks>https://www.oauth.com/oauth2-servers/access-tokens/access-token-response/#error</remarks>
    public class AccessTokenErrorResponse
    {
        /// <summary>
        /// Gets or sets the error.
        /// </summary>
        [JsonPropertyName("error")]
        public string? Error { get; set; }

        /// <summary>
        /// Gets or sets the error description.
        /// </summary>
        [JsonPropertyName("error_description")]
        public string? ErrorDescription { get; set; }

        /// <summary>
        /// Gets or sets an URI pointing to a resource with additional help related to the error.
        /// </summary>
        [JsonPropertyName("error_uri")]
        public string? ErrorUri { get; set; }
    }
}
