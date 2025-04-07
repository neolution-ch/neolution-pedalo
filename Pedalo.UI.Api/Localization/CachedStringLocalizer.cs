namespace Pedalo.UI.Api.Localization
{
    using System.Globalization;
    using Microsoft.Extensions.Caching.Distributed;
    using Microsoft.Extensions.Localization;

    /// <inheritdoc/>
    public class CachedStringLocalizer : IStringLocalizer
    {
        /// <summary>
        /// The cache
        /// </summary>
        private readonly IDistributedCache cache;

        /// <summary>
        /// Initializes a new instance of the <see cref="CachedStringLocalizer"/> class.
        /// </summary>
        /// <param name="cache">The cache.</param>
        public CachedStringLocalizer(IDistributedCache cache)
        {
            this.cache = cache;
        }

        /// <summary>
        /// Gets the <see cref="LocalizedString"/> with the specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>the translated string</returns>
        public LocalizedString this[string name]
        {
            get
            {
                var key = $"{CultureInfo.CurrentCulture.Name}_{name}";
                var value = this.cache.GetString(key);
                return new LocalizedString(name, value ?? $"NOT_FOUND_{name}");
            }
        }

        /// <summary>
        /// Gets the <see cref="LocalizedString"/> with the specified name.
        /// </summary>
        /// <value>
        /// The <see cref="LocalizedString"/>.
        /// </value>
        /// <param name="name">The name.</param>
        /// <param name="arguments">The arguments.</param>
        /// <returns>the translated string</returns>
        public LocalizedString this[string name, params object[] arguments]
        {
            get
            {
                var value = this[name];
                return new LocalizedString(name, string.Format(CultureInfo.InvariantCulture, value.Value, arguments));
            }
        }

        /// <summary>
        /// Gets all string resources.
        /// </summary>
        /// <param name="includeParentCultures">indicating whether to include strings from parent cultures.</param>
        /// <returns>
        /// The strings.
        /// </returns>
        public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
        {
            var result = new List<LocalizedString>();

            foreach (string name in Enum.GetNames(typeof(TranslationCodeId)))
            {
                result.Add(this[name]);
            }

            return result;
        }
    }
}
