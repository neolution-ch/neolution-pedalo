using System;

namespace PedaloWebApp.Core.Domain.Entities
{
    public class BookingPassengers
    {
        public Guid BookingPassengersId { get; set; } = Guid.NewGuid();
        public Guid BookingId { get; set; }
        public Guid PassengerId { get; set; }
        public Passenger Passenger { get; set; }
        public Booking Booking { get; set; }
    }
}
