namespace PedaloWebApp.UI.Api.Services
{
    using System.Globalization;
    using System.Security.Cryptography;
    using System.Text;
    using System.Text.Json;
    using Microsoft.Extensions.Logging;

    /// <inheritdoc />
    public class GenerateKeyFromObject : IGenerateKeyFromObject
    {
        /// <summary>
        /// The logger
        /// </summary>
        private readonly ILogger<GenerateKeyFromObject> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GenerateKeyFromObject"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        public GenerateKeyFromObject(ILogger<GenerateKeyFromObject> logger)
        {
            this.logger = logger;
        }

        /// <inheritdoc />
        public string GenerateKey(object sourceObject)
        {
            if (sourceObject == null)
            {
                return string.Empty;
            }

            var hashString = ComputeHash(this.ObjectToByteArray(sourceObject));
            return hashString;
        }

        /// <summary>
        /// Computes the hash.
        /// </summary>
        /// <param name="objectAsBytes">The object as bytes.</param>
        /// <returns>The computed hash</returns>
        private static string ComputeHash(byte[] objectAsBytes)
        {
            using var hashProvider = SHA512.Create();
            var hash = hashProvider.ComputeHash(objectAsBytes);

            // Build the final string by converting each byte
            // into hex and appending it to a StringBuilder
            var sb = new StringBuilder();
            foreach (var b in hash)
            {
                sb.Append(b.ToString("X2", CultureInfo.InvariantCulture));
            }

            // And return it
            return sb.ToString();
        }

        /// <summary>
        /// Objects to byte array.
        /// </summary>
        /// <param name="objectToSerialize">The object to serialize.</param>
        /// <returns>The object as byte array</returns>
        private byte[] ObjectToByteArray(object objectToSerialize)
        {
            try
            {
                return JsonSerializer.SerializeToUtf8Bytes(objectToSerialize);
            }
            catch (Exception ex)
            {
                this.logger.LogError("Error occurred during serialization. Message: {message}", ex.Message);
                return Array.Empty<byte>();
            }
        }
    }
}
