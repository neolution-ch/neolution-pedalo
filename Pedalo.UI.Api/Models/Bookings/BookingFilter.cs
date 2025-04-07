namespace Pedalo.UI.Api.Models.Bookings
{
    /// <summary>
    /// Represents a filter for booking queries.
    /// </summary>
    public class BookingFilter
    {
        /// <summary>
        /// Gets or sets the name of the pedalo.
        /// </summary>
        public string? PedaloName { get; set; }

        /// <summary>
        /// Gets or sets the name of the customer.
        /// </summary>
        public string? CustomerName { get; set; }
    }
}
