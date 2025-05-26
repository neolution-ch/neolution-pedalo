namespace PedaloWebApp.Core.Domain.Entities
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Represents a customer who can book pedalos.
    /// </summary>
    public class Customer
    {
        /// <summary>
        /// Gets or sets the unique identifier for the customer.
        /// </summary>
        public Guid CustomerId { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Gets or sets the first name of the customer.
        /// </summary>
        public required string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name of the customer.
        /// </summary>
        public required string LastName { get; set; }

        /// <summary>
        /// Gets or sets the birthday date of the customer.
        /// </summary>
        public DateTime BirthdayDate { get; set; }

        /// <summary>
        /// Gets or sets the bookings of the customer.
        /// </summary>
        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    }
}
