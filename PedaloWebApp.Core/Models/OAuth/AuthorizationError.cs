namespace PedaloWebApp.Core.Models.OAuth
{
    /// <summary>
    /// Authorization error
    /// </summary>
    public class AuthorizationError
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AuthorizationError"/> class.
        /// </summary>
        /// <param name="errorCode">The error code.</param>
        public AuthorizationError(OAuthErrorCode errorCode) => this.Init(errorCode, null, null);

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthorizationError"/> class.
        /// </summary>
        /// <param name="errorCode">The error code.</param>
        /// <param name="description">The description.</param>
        public AuthorizationError(OAuthErrorCode errorCode, string description) => this.Init(errorCode, description, null);

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthorizationError"/> class.
        /// </summary>
        /// <param name="errorCode">The error code.</param>
        /// <param name="description">The description.</param>
        /// <param name="uri">The URI.</param>
        public AuthorizationError(OAuthErrorCode errorCode, string description, string uri) => this.Init(errorCode, description, uri);

        /// <summary>
        /// Gets or sets the error code.
        /// </summary>
        public OAuthErrorCode Code { get; set; }

        /// <summary>
        /// Gets or sets the error description.
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Gets or sets the URI to a resource with more details regarding the error.
        /// </summary>
        public string? Uri { get; set; }

        /// <summary>
        /// Initializes the specified error code.
        /// </summary>
        /// <param name="errorCode">The error code.</param>
        /// <param name="description">The description.</param>
        /// <param name="uri">The URI.</param>
        private void Init(OAuthErrorCode errorCode, string? description, string? uri)
        {
            this.Code = errorCode;
            this.Description = description;
            this.Uri = uri;
        }
    }
}
