namespace PedaloWebApp.Core.Domain.Entities
{
    using System;
    using System.Collections.Generic;

    public class Pedalo
    {
        public Guid PedaloId { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public PedaloColor Color { get; set; }
        public int Capacity { get; set; }
        public decimal HourlyRate { get; set; }

        public ICollection<Booking> Bookings { get; set; }
    }

    public enum PedaloColor
    {
        Red,
        Blue,
        Pink,
        Green,
        Yellow,
    }
}
