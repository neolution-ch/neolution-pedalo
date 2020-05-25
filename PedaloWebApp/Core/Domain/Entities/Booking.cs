namespace PedaloWebApp.Core.Domain.Entities
{
    using System;

    public class Booking
    {
        public Guid BookingId { get; set; } = Guid.NewGuid();
        public Guid CustomerId { get; set; }
        public Guid PedaloId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public virtual Pedalo Pedalo { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
