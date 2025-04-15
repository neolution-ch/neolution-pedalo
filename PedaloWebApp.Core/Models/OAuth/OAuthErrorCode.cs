namespace PedaloWebApp.Core.Models.OAuth
{
    /// <summary>
    /// Possible error codes to return with an OAuth error response.
    /// </summary>
    /// <remarks>https://www.oauth.com/oauth2-servers/access-tokens/access-token-response/#error</remarks>
    public enum OAuthErrorCode
    {
        /// <summary>
        /// The request is missing a parameter so the server can’t proceed with the request. This may also be returned if the request
        /// includes an unsupported parameter or repeats a parameter.
        /// </summary>
        InvalidRequest = 0,

        /// <summary>
        /// Client authentication failed, such as if the request contains an invalid client ID or secret.
        /// </summary>
        InvalidClient = 1,

        /// <summary>
        /// The authorization code (or user’s password for the password grant type) is invalid or expired. This is also the error you
        /// would return if the redirect URL given in the authorization grant does not match the URL provided in this access token
        /// request.
        /// </summary>
        InvalidGrant = 2,

        /// <summary>
        /// For access token requests that include a scope (password or client_credentials grants), this error indicates an invalid
        /// scope value in the request.
        /// </summary>
        InvalidScope = 3,

        /// <summary>
        /// This client is not authorized to use the requested grant type. For example, if you restrict which applications can use the
        /// Implicit grant, you would return this error for the other apps.
        /// </summary>
        UnauthorizedClient = 4,

        /// <summary>
        /// If a grant type is requested that the authorization server does not recognize, use this code. Note that unknown grant types
        /// also use this specific error code rather than using the invalid_request above.
        /// </summary>
        UnsupportedGrantType = 5,
    }
}
