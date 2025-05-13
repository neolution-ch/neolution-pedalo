namespace PedaloWebApp.UI.Api.Extensions
{
    using System.Globalization;
    using Microsoft.Extensions.Localization;

    /// <summary>
    /// Provides extension methods for the <see cref="IStringLocalizer"/> interface.
    /// </summary>
    public static class IStringLocalizerExtensions
    {
        /// <summary>
        /// Retrieves a translated string and formats it with optional arguments.
        /// </summary>
        /// <param name="stringLocalizer">The <see cref="IStringLocalizer"/> instance.</param>
        /// <param name="key">The translation key represented as a <see cref="TranslationCodeId"/>.</param>
        /// <param name="arguments">Optional arguments to format the translated string.</param>
        /// <returns>The formatted translated string.</returns>
        public static string Text(this IStringLocalizer stringLocalizer, TranslationCodeId key, params string[] arguments)
        {
            if (stringLocalizer is null)
            {
                throw new ArgumentNullException(nameof(stringLocalizer));
            }

            var translation = stringLocalizer[key.ToString()].Value;
            var result = string.Format(CultureInfo.InvariantCulture, translation, arguments);
            return result;
        }

        /// <summary>
        /// Retrieves a translated string and formats it with a dictionary of arguments.
        /// </summary>
        /// <param name="stringLocalizer">The <see cref="IStringLocalizer"/> instance.</param>
        /// <param name="key">The translation key represented as a <see cref="TranslationCodeId"/>.</param>
        /// <param name="arguments">A dictionary of key-value pairs representing the arguments to format the translated string.</param>
        /// <returns>The formatted translated string.</returns>
        public static string Text(this IStringLocalizer stringLocalizer, TranslationCodeId key, IDictionary<string, string> arguments)
        {
            if (stringLocalizer is null)
            {
                throw new ArgumentNullException(nameof(stringLocalizer));
            }

            var translation = stringLocalizer[key.ToString()].Value;
            var result = FormatStringWithDictionary(translation, arguments);
            return result;
        }

        /// <summary>
        /// Formats a string by replacing placeholders with corresponding values from a dictionary.
        /// </summary>
        /// <param name="inputString">The input string to format.</param>
        /// <param name="arguments">A dictionary of key-value pairs representing the arguments to format the string.</param>
        /// <returns>The formatted string.</returns>
        private static string FormatStringWithDictionary(string inputString, IDictionary<string, string> arguments)
        {
            foreach (var argument in arguments)
            {
                string placeholder = $"{{{argument.Key}}}";
                string replacement = argument.Value;

                inputString = inputString.Replace(placeholder, replacement);
            }

            return inputString;
        }
    }
}
