namespace Pedalo.Core.Domain.Entities
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Represents a pedalo that can be booked by customers.
    /// </summary>
    public class Pedalo
    {
        /// <summary>
        /// Gets or sets the unique identifier for the pedalo.
        /// </summary>
        public Guid PedaloId { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Gets or sets the name of the pedalo.
        /// </summary>
        public required string Name { get; set; }

        /// <summary>
        /// Gets or sets the color of the pedalo.
        /// </summary>
        public PedaloColor Color { get; set; }

        /// <summary>
        /// Gets or sets the capacity of the pedalo.
        /// </summary>
        public int Capacity { get; set; }

        /// <summary>
        /// Gets or sets the hourly rate for booking the pedalo.
        /// </summary>
        public decimal HourlyRate { get; set; }

        /// <summary>
        /// Gets or sets the bookings of the pedalo.
        /// </summary>
        public ICollection<Booking> Bookings { get; set; } = [];
    }
}
