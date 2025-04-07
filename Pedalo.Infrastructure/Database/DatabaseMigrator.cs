namespace Pedalo.Infrastructure.Database
{
    using Microsoft.EntityFrameworkCore.Migrations;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Implementation of <see cref="IDatabaseMigrator"/>
    /// </summary>
    public class DatabaseMigrator : IDatabaseMigrator
    {
        /// <summary>
        /// The configuration.
        /// </summary>
        private readonly IConfiguration configuration;

        /// <summary>
        /// The context factory.
        /// </summary>
        private readonly IAppDbContextFactory contextFactory;

        /// <summary>
        /// The logger
        /// </summary>
        private readonly ILogger<DatabaseMigrator> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="DatabaseMigrator"/> class
        /// </summary>
        /// <param name="configuration">The configuration</param>
        /// <param name="contextFactory">The context factory</param>
        /// <param name="logger">The logger</param>
        public DatabaseMigrator(IConfiguration configuration, IAppDbContextFactory contextFactory, ILogger<DatabaseMigrator> logger)
        {
            this.configuration = configuration;
            this.contextFactory = contextFactory;
            this.logger = logger;
        }

        /// <summary>
        /// Gets the migrations.
        /// </summary>
        /// <returns>The list of migrations</returns>
        public static IList<string> Migrations
        {
            get
            {
                var migrations = new List<string>();
                foreach (var type in typeof(DatabaseMigrator).Assembly.GetTypes())
                {
                    var migrationAttributes = type.GetCustomAttributes(typeof(MigrationAttribute), true);
                    if (migrationAttributes.Length == 1)
                    {
                        migrations.Add(((MigrationAttribute)migrationAttributes[0]).Id);
                    }
                }

                migrations.Sort();
                return migrations;
            }
        }

        /// <inheritdoc/>
        public void Run()
        {
            var migrations = Migrations;
            if (!migrations.Any())
            {
                return;
            }

            this.CheckPendingAndAppliedMigrations(migrations);
        }

        /// <summary>
        /// Checks the pending and applied migrations.
        /// Throws an error in the following cases:
        /// - There are pending migrations but RuntimeDatabaseMigration is set to false
        /// - There are pending migrations and RuntimeDatabaseMigration is set to true but the AppDbContextMigration is missing
        /// - If the migrations and applied migrations are not an equal sequence
        /// </summary>
        /// <param name="migrations">The migrations.</param>
        private void CheckPendingAndAppliedMigrations(IList<string> migrations)
        {
            List<string> pendingMigrations;
            List<string> appliedMigrations;
            using (var context = this.contextFactory.CreateReadOnlyContext())
            {
                pendingMigrations = context.Database.GetPendingMigrations().ToList();
                appliedMigrations = context.Database.GetAppliedMigrations().ToList();
            }

            if (pendingMigrations.Any())
            {
                if (!this.configuration.GetValue<bool>("RuntimeDatabaseMigration"))
                {
                    throw new InvalidOperationException("Database requires migration but RuntimeDatabaseMigration is set to false");
                }

                var connectionString = this.configuration.GetConnectionString("AppDbContextMigration");
                if (string.IsNullOrWhiteSpace(connectionString))
                {
                    throw new InvalidOperationException("Connection string 'AppDbContextMigration' is required to migrate database to last version");
                }

                using var context = this.contextFactory.CreateContext(connectionString);
                context.Database.Migrate();
                this.logger.LogInformation("Migrations applied succesfully");

                return;
            }

            if (appliedMigrations.Count > migrations.Count)
            {
                this.logger.LogWarning("Current database version is higher than the expected version");
                return;
            }

            if (!migrations.SequenceEqual(appliedMigrations))
            {
                throw new InvalidOperationException($"Current database version does not match the expected version");
            }
        }
    }
}
