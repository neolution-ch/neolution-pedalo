namespace PedaloWebApp.UI.Api.Options
{
    /// <summary>
    /// Site configuration options
    /// </summary>
    public class SiteConfig
    {
        /// <summary>
        /// Gets or sets the client base URL. Used to configure CORS.
        /// </summary>
        public string ClientBaseUrl { get; set; } = string.Empty;
    }
}
