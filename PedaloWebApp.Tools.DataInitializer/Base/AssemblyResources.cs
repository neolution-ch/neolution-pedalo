namespace PedaloWebApp.Tools.DataInitializer.Base
{
    using System.Reflection;

    /// <summary>
    /// Helper to work with embedded assembly ressources.
    /// </summary>
    public static class AssemblyResources
    {
        /// <summary>
        /// This assembly.
        /// </summary>
        private static readonly Assembly Assembly = typeof(AssemblyResources).Assembly;

        /// <summary>
        /// Reads text from the specified resource name in the assembly.
        /// </summary>
        /// <param name="resourceName">Name of the resource.</param>
        /// <returns>Text content from resource.</returns>
        public static string ReadText(string resourceName)
        {
            string result;
            var stream = Assembly.GetManifestResourceStream(resourceName);

            if (stream == null)
            {
                throw new ArgumentException("Could not find resource.", nameof(resourceName));
            }

            using (var reader = new StreamReader(stream))
            {
                result = reader.ReadToEnd();
            }

            return result;
        }

        /// <summary>
        /// Reads bytes from the specified resource name in the assembly.
        /// </summary>
        /// <param name="resourceName">Name of the resource.</param>
        /// <returns>Binary content from resource.</returns>
        public static byte[] ReadFile(string resourceName)
        {
            byte[] result;
            var stream = Assembly.GetManifestResourceStream(resourceName);

            if (stream == null)
            {
                throw new ArgumentException("Could not find resource.", nameof(resourceName));
            }

            using (var reader = new MemoryStream())
            {
                stream.CopyTo(reader);
                result = reader.ToArray();
            }

            return result;
        }
    }
}
