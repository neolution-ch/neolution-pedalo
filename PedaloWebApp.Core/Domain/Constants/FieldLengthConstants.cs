namespace PedaloWebApp.Core.Domain.Constants
{
    /// <summary>
    /// Some field length.
    /// </summary>
    public static class FieldLengthConstants
    {
        /// <summary>
        /// Gets the max email address length
        /// </summary>
        public static int MaxEmailAddressLength => 320;

        /// <summary>
        /// Gets the maximum email recipients
        /// </summary>
        public static int MaxEmailRecipients => 10;

        /// <summary>
        /// Gets the maximum length for an IP address
        /// </summary>
        /// <remarks>IPv6 can use up to 46 chars, see <see href="https://datatracker.ietf.org/doc/html/rfc2553#section-6.6"/> for details (INET6_ADDRSTRLEN).</remarks>
        public static int MaxIpAddressLength => 46;
    }
}
