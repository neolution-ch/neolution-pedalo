namespace PedaloWebApp.UI.Api.Models.Pedalos
{
    using PedaloWebApp.Core.Domain.Enums;

    /// <summary>
    /// Represents a pedalo model.
    /// </summary>
    public class PedaloModel
    {
        /// <summary>
        /// Gets or sets the unique identifier for the pedalo.
        /// </summary>
        public Guid PedaloId { get; set; }

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
    }
}
