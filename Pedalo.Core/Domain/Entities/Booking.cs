namespace Pedalo.Core.Domain.Entities
{
    using System;

    /// <summary>
    /// Represents a booking for a pedalo.
    /// </summary>
    public class Booking
    {
        /// <summary>
        /// Gets or sets the unique identifier for the booking.
        /// </summary>
        public Guid BookingId { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Gets or sets the unique identifier of the customer who made the booking.
        /// </summary>
        public Guid CustomerId { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the pedalo being booked.
        /// </summary>
        public Guid PedaloId { get; set; }

        /// <summary>
        /// Gets or sets the start date of the booking.
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Gets or sets the end date of the booking. Is <c>null</c> if the booking is ongoing.
        /// </summary>
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// Gets or sets the customer who made the booking.
        /// </summary>
        public Customer Customer { get; set; } = null!;

        /// <summary>
        /// Gets or sets the pedalo associated with the booking.
        /// </summary>
        public Pedalo Pedalo { get; set; } = null!;
    }
}
