namespace PedaloWebApp.Core.Domain.Enums
{
    /// <summary>
    /// The user claim value
    /// </summary>
    public enum UserClaimValue
    {
        /// <summary>
        /// Generic read permission
        /// </summary>
        Read = 0,

        /// <summary>
        /// Generic write permission
        /// </summary>
        Write = 1,

        /// <summary>
        /// Read all permission
        /// </summary>
        ReadAll = 2,

        /// <summary>
        /// Write all permission
        /// </summary>
        WriteAll = 3,

        /// <summary>
        /// Read permission on the own tenant
        /// </summary>
        ReadOwnTenant = 4,

        /// <summary>
        /// Write permission on the own tenant
        /// </summary>
        WriteOwnTenant = 5,
    }
}
