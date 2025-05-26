namespace PedaloWebApp.UI.Api.Options
{
    /// <summary>
    /// Config values for authentication and authorization
    /// </summary>
    public class AuthConfig
    {
        /// <summary>
        /// Gets or sets the display name of the issuer.
        /// </summary>
        public string IssuerDisplayName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets a value indicating whether users are required to enable MFA.
        /// </summary>
        /// <value>
        ///   <c>true</c> if MFA is required; otherwise, <c>false</c>.
        /// </value>
        public bool RequireMfa { get; set; }
    }
}
