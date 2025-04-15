namespace PedaloWebApp.UI.Api.Extensions
{
    using System.Security.Claims;
    using System.Security.Principal;

    /// <summary>
    /// Extension methods for <see cref="IIdentity"/>.
    /// </summary>
    public static class IdentityExtensions
    {
        /// <summary>
        /// Retrieves the Authentication Method claim from an IIdentity.
        /// </summary>
        /// <param name="identity">The IIdentity instance from which to retrieve the claim.</param>
        /// <returns>The value of the Authentication Method claim, or null if not found.</returns>
        public static string? GetAuthenticationMethod(this IIdentity identity)
        {
            if (identity is not ClaimsIdentity claimsIdentity)
            {
                return null;
            }

            var authenticationMethodClaim = claimsIdentity.FindFirst(ClaimTypes.AuthenticationMethod);
            return authenticationMethodClaim?.Value;
        }
    }
}
