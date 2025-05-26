namespace PedaloWebApp.UI.Api.Models.Bookings
{
    /// <summary>
    /// Represents a booking model.
    /// </summary>
    public class BookingModel
    {
        /// <summary>
        /// Gets or sets the unique identifier for the booking.
        /// </summary>
        public Guid BookingId { get; set; }

        /// <summary>
        /// Gets or sets the name of the pedalo.
        /// </summary>
        public required string PedaloName { get; set; }

        /// <summary>
        /// Gets or sets the name of the customer.
        /// </summary>
        public required string CustomerName { get; set; }

        /// <summary>
        /// Gets or sets the start date of the booking.
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Gets or sets the end date of the booking. Is <c>null</c> if the booking is ongoing.
        /// </summary>
        public DateTime? EndDate { get; set; }
    }
}
