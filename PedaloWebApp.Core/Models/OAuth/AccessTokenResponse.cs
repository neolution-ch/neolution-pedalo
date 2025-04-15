namespace PedaloWebApp.Core.Models.OAuth
{
    using System.Text.Json.Serialization;

    /// <summary>
    /// If the request for an access token is valid, the authorization server needs to generate
    /// an access token (and optional refresh token) and return these to the client, typically
    /// along with some additional properties about the authorization.
    /// https://www.oauth.com/oauth2-servers/access-tokens/access-token-response/
    /// </summary>
    public class AccessTokenResponse
    {
        /// <summary>
        /// Gets or sets the access token string as issued by the authorization server.
        /// </summary>
        [JsonPropertyName("access_token")]
        public string AccessToken { get; set; } = null!;

        /// <summary>
        /// Gets or sets the type of token this is, typically just the string "bearer"
        /// </summary>
        [JsonPropertyName("token_type")]
        public string TokenType { get; set; } = "bearer";

        /// <summary>
        /// Gets or sets the duration of time the access token is granted for.
        /// </summary>
        [JsonPropertyName("expires_in")]
        public int ExpiresIn { get; set; }

        /// <summary>
        /// Gets or sets the refresh token which applications can use to obtain another access token
        /// when the access token is expired. However, tokens issued with the implicit grant cannot
        /// be issued a refresh token.
        /// </summary>
        [JsonPropertyName("refresh_token")]
        public string RefreshToken { get; set; } = null!;

        /// <summary>
        /// Gets or sets the scope. If the scope the user granted is identical to the scope the app
        /// requested, this parameter is optional. If the granted scope is different from the requested
        /// scope, such as if the user modified the scope, then this parameter is required.
        /// </summary>
        [JsonPropertyName("scope")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Scope { get; set; }
    }
}
