namespace PedaloWebApp.UI.Api.Services
{
    /// <summary>
    /// Service to create a hash key from an object:
    /// https://www.codeproject.com/Articles/21312/Generating-MD5-Hash-out-of-C-Objects
    /// </summary>
    public interface IGenerateKeyFromObject
    {
        /// <summary>
        /// Generates the key.
        /// </summary>
        /// <param name="sourceObject">The source object.</param>
        /// <returns>The generated key</returns>
        /// <exception cref="ArgumentNullException">Null as parameter is not allowed</exception>
        /// <exception cref="ApplicationException">Could not definitely decide if object is serializable.Message:" + ame.Message</exception>
        string GenerateKey(object sourceObject);
    }
}
