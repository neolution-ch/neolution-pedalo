namespace PedaloWebApp.Core.Domain.Entities
{
    using Microsoft.EntityFrameworkCore;
    using System;

    public class Booking
    {
        public Guid BookingId { get; set; } = Guid.NewGuid();
        public Guid CustomerId { get; set; }
        public Guid PedaloId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public Pedalo Pedalo { get; set; }
        public Customer Customer { get; set; }
    }
}
