namespace Pedalo.UI.Api.Extensions
{
    using Microsoft.AspNetCore.Mvc.ApplicationModels;
    using Pedalo.UI.Api.Models.CustomRouteToken;

    /// <summary>
    /// The MVC options extensions.
    /// </summary>
    public static class MvcOptionsExtensions
    {
        /// <summary>
        /// Uses the full namespace route token.
        /// </summary>
        /// <param name="options">The options</param>
        /// <param name="baseNamespace">The base namespace that wont be used for the url</param>
        /// <param name="basePath">The additional base path</param>
        /// <returns>The updated options</returns>
        public static MvcOptions UseBaseRouteToken(this MvcOptions options, string baseNamespace, string basePath)
        {
            if (options is null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            options.Conventions.Add(new CustomRouteToken("baseroute", (ControllerModel c) => c.ControllerType.Namespace?.GetNamespaceRoute(baseNamespace, basePath)));

            return options;
        }

        /// <summary>
        /// Uses the kebab case controller token.
        /// </summary>
        /// <param name="options">The options</param>
        /// <returns>The updated options</returns>
        public static MvcOptions UseKebabCaseController(this MvcOptions options)
        {
            if (options is null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            options.Conventions.Add(new CustomRouteToken("controller", (ControllerModel c) => c.ControllerName.PascalToKebabCase()));

            return options;
        }
    }
}
