namespace PedaloWebApp.UI.Api.Localization
{
    using System.Text.Json;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Caching.Distributed;
    using Microsoft.Extensions.Localization;

    /// <inheritdoc/>
    public class JsonStringLocalizerFactory : IStringLocalizerFactory
    {
        /// <summary>
        /// JsonSerializer options
        /// </summary>
        private static readonly JsonSerializerOptions JsonSerializerOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        /// <summary>
        /// The cache
        /// </summary>
        private readonly IDistributedCache cache;

        /// <summary>
        /// The hosting environment
        /// </summary>
        private readonly IWebHostEnvironment hostingEnvironment;

        /// <summary>
        /// Initializes a new instance of the <see cref="JsonStringLocalizerFactory"/> class.
        /// </summary>
        /// <param name="cache">The cache.</param>
        /// <param name="hostingEnvironment">The hosting environment.</param>
        public JsonStringLocalizerFactory(
            IDistributedCache cache,
            IWebHostEnvironment hostingEnvironment)
        {
            this.cache = cache;
            this.hostingEnvironment = hostingEnvironment;
            this.LoadTranslationsToCache();
        }

        /// <summary>
        /// Loads the translations to cache.
        /// </summary>
        public void LoadTranslationsToCache()
        {
            var currentDirectory = new DirectoryInfo(this.hostingEnvironment.ContentRootPath);
            var resourcesDirectory = currentDirectory.GetDirectories("Resources").FirstOrDefault();
            if (resourcesDirectory != null)
            {
                var resourceFiles = new Dictionary<string, FileInfo>();
                var jsonFiles = resourcesDirectory.GetFiles("*.json");
                foreach (var jsonFile in jsonFiles)
                {
                    var languageCode = jsonFile.Name[..jsonFile.Name.IndexOf(".json", StringComparison.InvariantCultureIgnoreCase)];
                    resourceFiles.Add(languageCode, jsonFile);
                }

                var result = ReadAllFileResources(resourceFiles);

                foreach (var languageCode in result.Keys)
                {
                    foreach (var translation in result[languageCode])
                    {
                        this.cache.SetString(
                            $"{languageCode}_{translation.Key}",
                            translation.Value);
                    }
                }
            }
        }

        /// <summary>
        /// Creates an IStringLocalizer
        /// </summary>
        /// <param name="resourceSource">The <see cref="System.Type" /> to load resources for.</param>
        /// <returns>The IStringLocalizer</returns>
        public IStringLocalizer Create(Type resourceSource) => new CachedStringLocalizer(this.cache);

        /// <summary>
        /// Creates an IStringLocalizer
        /// </summary>
        /// <param name="baseName">The base name of the resource to load strings from.</param>
        /// <param name="location">The location to load resources from.</param>
        /// <returns>
        /// The IStringLocalizer
        /// </returns>
        public IStringLocalizer Create(string baseName, string location) =>
            new CachedStringLocalizer(this.cache);

        /// <summary>
        /// reads json resource file
        /// </summary>
        /// <param name="resourceFiles">The resource files.</param>
        /// <returns>
        /// dictionary with translations
        /// </returns>
        private static IDictionary<string, IDictionary<TranslationCodeId, string>> ReadAllFileResources(IDictionary<string, FileInfo> resourceFiles)
        {
            var result = new Dictionary<string, IDictionary<TranslationCodeId, string>>();

            foreach (var languageCode in resourceFiles.Select(x => x.Key))
            {
                var languageTranslations = ReadFileResource(
                    resourceFiles[languageCode].FullName);

                // convert string based json file to enum based dictionary
                // don't use try/catch here when the source is json based, so we are always sure it works as expected
                var translationEnumDictionary = new Dictionary<TranslationCodeId, string>();
                foreach (var translation in languageTranslations)
                {
                    translationEnumDictionary.Add(
                        (TranslationCodeId)Enum.Parse(typeof(TranslationCodeId), translation.Key),
                        translation.Value);
                }

                result.Add(languageCode, translationEnumDictionary);
            }

            return result;
        }

        /// <summary>
        /// reads json resource file
        /// </summary>
        /// <param name="jsonFile">file to read</param>
        /// <returns>
        /// dictionary with translations
        /// </returns>
        private static IDictionary<string, string> ReadFileResource(string jsonFile)
        {
            var json = File.ReadAllText(jsonFile) ?? throw new ArgumentNullException($"Could not read JSON file resource: {jsonFile}");
            return JsonSerializer.Deserialize<Dictionary<string, string>>(json, JsonSerializerOptions) ?? throw new InvalidOperationException("Could not deserialize JSON from resource file");
        }
    }
}
