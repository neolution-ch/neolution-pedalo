namespace Pedalo.Core.Domain
{
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Text;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata;
    using Pedalo.Core.Domain.Entities;

    /// <inheritdoc />
    /// <summary>
    /// Database context for the application
    /// </summary>
    public class AppDbContext : DbContext
    {
        /// <summary>
        /// The database context config
        /// </summary>
        private readonly AppDbContextConfig config;

        /// <summary>
        /// Initializes a new instance of the <see cref="AppDbContext" /> class.
        /// </summary>
        /// <param name="options">The EF database context options</param>
        /// <param name="config">The database context config.</param>
        public AppDbContext(DbContextOptions options, AppDbContextConfig config)
            : base(options)
        {
            this.config = config;
        }

        // !DbSets (please order alphabetically)

        /// <summary>
        /// Gets the bookings.
        /// </summary>
        public DbSet<Booking> Bookings => this.Set<Booking>();

        /// <summary>
        /// Gets the customers.
        /// </summary>
        public DbSet<Customer> Customers => this.Set<Customer>();

        /// <summary>
        /// Gets the pedalos.
        /// </summary>
        public DbSet<Pedalo> Pedalos => this.Set<Pedalo>();

        /// <inheritdoc />
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ArgumentNullException.ThrowIfNull(modelBuilder);

            // Remove OnDeleteCascade from all foreign keys by default.
            // Must be called before applying the entity configurations to allow default overriding.
            var foreignKeys = modelBuilder.Model.GetEntityTypes()
                .SelectMany(t => t.GetForeignKeys())
                .Where(fk => fk is { IsOwnership: false, DeleteBehavior: DeleteBehavior.Cascade });
            foreach (var fk in foreignKeys)
            {
                fk.DeleteBehavior = DeleteBehavior.ClientNoAction;
            }

            if (this.config.EntityConfigurationsAssembly is not null)
            {
                // Automatically Add all entity configurations
                modelBuilder.ApplyConfigurationsFromAssembly(this.config.EntityConfigurationsAssembly, type => type.Namespace?.EndsWith(".EntityConfigurations", StringComparison.Ordinal) == true);
            }

            this.PerformDesignTimeChecks(modelBuilder);
        }

        /// <inheritdoc/>
        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            ArgumentNullException.ThrowIfNull(configurationBuilder);

            base.ConfigureConventions(configurationBuilder);

            configurationBuilder.Properties<DateOnly>()
                .HaveConversion<DateOnlyConverter>()
                .HaveColumnType("date");

            configurationBuilder.Properties<DateOnly?>()
                .HaveConversion<DateOnlyConverter>()
                .HaveColumnType("date");
        }

        /// <inheritdoc />
        [SuppressMessage("Major Code Smell", "S125:Sections of code should not be commented out", Justification = "Useful for debugging")]
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.LogTo(Console.WriteLine);
            //optionsBuilder.EnableSensitiveDataLogging();
            //optionsBuilder.EnableDetailedErrors();
        }

        /// <summary>
        /// Performs the design time checks.
        /// </summary>
        /// <param name="builder">The builder.</param>
        private void PerformDesignTimeChecks(ModelBuilder builder)
        {
            if (this.config.PerformChecks)
            {
                // Check entities without table name
                var entitiesWithoutTableName = builder.Model.GetEntityTypes().Where(x => x.FindAnnotation(RelationalAnnotationNames.TableName) == null).ToList();
                if (entitiesWithoutTableName.Count != 0)
                {
                    var error = new StringBuilder();
                    error.AppendLine("The following entities doesn't have a defined table name (must be explicitly defined in the database context):");
                    entitiesWithoutTableName.ForEach(x => error.AppendLine($"{x.Name}"));
                    throw new NotSupportedException(error.ToString());
                }

                // Check shadow properties
                var validShadowProperties = new List<string>() { "PeriodStart", "PeriodEnd" }; // exclude the properties for temporal tables
                var shadowProperties = builder.Model.GetEntityTypes().SelectMany(x => x.GetDeclaredProperties().Where(x => x.IsShadowProperty() && !validShadowProperties.Contains(x.Name))).ToList();
                if (shadowProperties.Count != 0)
                {
                    var error = new StringBuilder();
                    error.AppendLine("The following shadow properties must be explicitly defined in the entity:");
                    shadowProperties.ForEach(x => error.AppendLine($"{(x.DeclaringType as IMutableEntityType)?.Name}.{x.Name}"));

                    // throw new NotSupportedException(error.ToString())
                }
            }
        }
    }
}
