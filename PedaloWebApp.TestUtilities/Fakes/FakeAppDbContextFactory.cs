namespace PedaloWebApp.TestUtilities.Fakes
{
    using Microsoft.Data.Sqlite;
    using Microsoft.EntityFrameworkCore;
    using PedaloWebApp.Infrastructure.Database;

    /// <summary>
    /// The Fake App Db Context Factory (uses SQLite with either in memory or file system)
    /// </summary>
    /// <seealso cref="PedaloWebApp.Core.Interfaces.Database.IAppDbContextFactory" />
    public class FakeAppDbContextFactory : IAppDbContextFactory, IDisposable
    {
        /// <summary>
        /// The options builder
        /// </summary>
        private readonly DbContextOptionsBuilder<AppDbContext> optionsBuilder;

        /// <summary>
        /// The database context options
        /// </summary>
        private readonly AppDbContextConfig config;

        /// <summary>
        /// The database connection
        /// </summary>
        private readonly SqliteConnection connection;

        /// <summary>
        /// To detect redundant calls
        /// </summary>
        private bool disposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="FakeAppDbContextFactory" /> class.
        /// </summary>
        public FakeAppDbContextFactory()
            : this(useInMemory: true)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FakeAppDbContextFactory"/> class.
        /// </summary>
        /// <param name="useInMemory">if set to <c>true</c> use in-memory database.</param>
        public FakeAppDbContextFactory(bool useInMemory)
        {
            var appDbContextFactoryAssembly = typeof(AppDbContextFactory).Assembly;
            this.connection = CreateDbConnection(useInMemory);
            this.optionsBuilder = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlite(this.connection, x => x.MigrationsAssembly(appDbContextFactoryAssembly.FullName));

            this.config = new AppDbContextConfig
            {
                EntityConfigurationsAssembly = appDbContextFactoryAssembly,
            };

            // Open the connection if using in-memory database to ensure the database exists
            if (useInMemory)
            {
                this.connection.Open();
            }
        }

        /// <inheritdoc/>
        public AppDbContext CreateContext()
        {
            var context = new AppDbContext(this.optionsBuilder.Options, this.config);
            context.Database.EnsureCreated();
            return context;
        }

        /// <inheritdoc/>
        public AppDbContext CreateContext(string connectionString)
        {
            return this.CreateContext();
        }

        /// <inheritdoc/>
        public AppDbContext CreateReadOnlyContext()
        {
            var context = new AppDbContext(this.optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking).Options, this.config);
            context.Database.EnsureCreated();
            return context;
        }

        /// <summary>
        /// Disposes the database connection.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Disposes the database connection.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (this.disposed)
            {
                return;
            }

            if (disposing)
            {
                this.connection?.Dispose();
            }

            this.disposed = true;
        }

        /// <summary>
        /// Creates the database.
        /// </summary>
        /// <param name="useInMemoryDb">Use the in-memory database</param>
        /// <returns>DbConnection</returns>
        private static SqliteConnection CreateDbConnection(bool useInMemoryDb)
        {
            return useInMemoryDb
                ? new SqliteConnection("DataSource=file::memory:?cache=shared")
                : new SqliteConnection(@"Filename=C:\temp\test.db");
        }
    }
}
