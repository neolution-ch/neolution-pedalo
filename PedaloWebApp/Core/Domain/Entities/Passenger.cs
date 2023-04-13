namespace PedaloWebApp.Core.Domain.Entities
{
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;

    public class Passenger
    {
        public Guid PassengerId { get; set; } = Guid.NewGuid();
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public ICollection<BookingPassengers> BookingPassengers { get; set; }
    }
}
