namespace Pedalo.UI.Api.Extensions
{
    using System.Text.Json;

    /// <summary>
    /// The string extensions.
    /// </summary>
    internal static class StringExtensions
    {
        /// <summary>
        /// Gets the namespace route.
        /// </summary>
        /// <param name="nameSpace">The name space.</param>
        /// <param name="baseNamespace">The base namespace.</param>
        /// <param name="basePath">The base path.</param>
        /// <returns>The namespace route</returns>
        public static string GetNamespaceRoute(this string nameSpace, string baseNamespace, string basePath)
        {
            if (string.IsNullOrWhiteSpace(nameSpace))
            {
                throw new ArgumentNullException(nameof(nameSpace));
            }

            var segments = new List<string> { basePath };
            segments.AddRange(nameSpace.Split('.').SkipWhile(x => x != baseNamespace).Skip(1).Select(x => x.PascalToKebabCase()));
            return string.Join("/", segments.Where(x => !string.IsNullOrWhiteSpace(x)));
        }

        /// <summary>
        /// Pascals to kebab case.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The string in kebab case</returns>
        public static string PascalToKebabCase(this string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return value;
            }

            return JsonNamingPolicy.KebabCaseLower.ConvertName(value);
        }
    }
}
