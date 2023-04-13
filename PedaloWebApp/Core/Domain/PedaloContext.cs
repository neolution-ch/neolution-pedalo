namespace PedaloWebApp.Core.Domain
{
    using System;
    using System.Linq;
    using System.Reflection;
    using Microsoft.EntityFrameworkCore;
    using PedaloWebApp.Core.Domain.Entities;

    public class PedaloContext : DbContext
    {
        /// <summary>
        /// The assembly entity configurations.
        /// </summary>
        private readonly Assembly entityConfigurationsAssembly;

        public PedaloContext(DbContextOptions<PedaloContext> options, Assembly entityConfigurationsAssembly)
            : base(options)
        {
            this.entityConfigurationsAssembly = entityConfigurationsAssembly;
        }

        public virtual DbSet<Booking> Bookings { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Pedalo> Pedaloes { get; set; }
        public virtual DbSet<Passenger> Passengers { get; set; }
        public virtual DbSet<BookingPassengers> BookingPassengers { get; set; }

        /// <inheritdoc />
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if (modelBuilder == null)
            {
                throw new ArgumentNullException(nameof(modelBuilder));
            }

            // Automatically Add all entity configurations
            modelBuilder.ApplyConfigurationsFromAssembly(this.entityConfigurationsAssembly, type => type.Namespace?.EndsWith(".EntityConfigurations", StringComparison.Ordinal) == true);

            // Remove OnDeleteCascade from all Foreign Keys
            var foreignKeys = modelBuilder.Model.GetEntityTypes()
                .SelectMany(t => t.GetForeignKeys())
                .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);
            foreach (var fk in foreignKeys)
            {
                fk.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }
    }
}
