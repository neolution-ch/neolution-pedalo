namespace PedaloWebApp.Tools.DataInitializer.Initializers
{
    using System.Text;
    using Microsoft.Data.SqlClient;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Class to inherit from to build a database context initializer
    /// </summary>
    /// <typeparam name="TContext">The database context.</typeparam>
    /// <seealso cref="IDisposable" />
    public abstract class DbContextInitializer<TContext> : IDisposable
        where TContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DbContextInitializer{T}" /> class.
        /// </summary>
        /// <param name="contextFactory">The context factory.</param>
        /// <param name="logger">The logger.</param>
        /// <param name="configuration">The configuration.</param>
        protected DbContextInitializer(IDbContextFactory<TContext> contextFactory, ILogger logger, IConfiguration configuration)
        {
            if (contextFactory is null)
            {
                throw new ArgumentNullException(nameof(contextFactory));
            }

            this.Context = contextFactory.CreateDbContext();

            this.Logger = logger;
            this.Configuration = configuration;

            this.Logger.LogTrace("DbContext Initializer for '{name}'.", typeof(TContext).Name);
            this.Logger.LogTrace("ConnectionString: '{connectionString}'.", this.DisplayConnectionString());
        }

        /// <summary>
        /// Gets the context for this initializer to run.
        /// </summary>
        public TContext Context { get; }

        /// <summary>
        /// Gets the configuration.
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Gets the logger
        /// </summary>
        protected ILogger Logger { get; }

        /// <summary>
        /// - Ensures that the database for the context exists. If it exists, all tables will be dropped
        /// - Database will be created using migrations.
        /// - Database will be seeded.
        /// </summary>
        /// <param name="cancellationToken">the cancellation token</param>
        /// <returns>void</returns>
        public async virtual Task CreateDatabaseAsync(CancellationToken cancellationToken)
        {
            var databaseExists = await this.Context.Database.CanConnectAsync(cancellationToken).ConfigureAwait(false);
            if (databaseExists)
            {
                // Drop all tables when database existed
                this.Logger.LogInformation("Start dropping all tables/indexes");
                await this.ClearDatabaseAsync(cancellationToken).ConfigureAwait(false);
                this.Logger.LogInformation("All tables/indexes dropped!");
            }

            this.CreateSqlUser();

            this.Logger.LogInformation("Start Migrating the DB");
            await this.Context.Database.MigrateAsync(cancellationToken).ConfigureAwait(false);
            this.Logger.LogInformation("DB Migrated");

            // After creation, seed the database
            this.Logger.LogInformation("Start seeding...");
            await this.SeedAsync().ConfigureAwait(false);
            this.Logger.LogInformation("Seeding complete!");

            if (this.Configuration.GetValue<bool>("CreateSnapshot"))
            {
                await this.CreateSnapshotAsync(cancellationToken).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Ensures that the database for the context does not exist. If it does not exist,
        /// no action is taken. If it does exist then the database is deleted.
        /// Warning: The entire database is deleted, and no effort is made to remove just
        /// the database objects that are used by the model for this context.
        /// </summary>
        /// <param name="cancellationToken">the cancellation token</param>
        /// <returns>void</returns>
        public async virtual Task DeleteDatabaseAsync(CancellationToken cancellationToken)
        {
            this.Logger.LogInformation("Start deleting the database...");
            await this.Context.Database.EnsureDeletedAsync(cancellationToken).ConfigureAwait(false);
            this.Logger.LogInformation("Database deleted!");
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.Context?.Dispose();
            }
        }

        /// <summary>
        /// Implement this method if you have seeding data for your database.
        /// </summary>
        /// <returns>void</returns>
        protected abstract Task SeedAsync();

        /// <summary>
        /// Displays the connection string.
        /// </summary>
        /// <returns>The connection string with the password masked.</returns>
        private string DisplayConnectionString()
        {
            var connectionString = this.Context.Database.GetDbConnection().ConnectionString;
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                return string.Empty;
            }

            // Display the connection string if it does not contain a password
            var passwordPosition = connectionString.IndexOf("password", StringComparison.OrdinalIgnoreCase);
            if (passwordPosition == -1)
            {
                return connectionString;
            }

            // Mask the password of the connection string
            var equalPosition = connectionString.IndexOf("=", passwordPosition, StringComparison.OrdinalIgnoreCase);
            var separatorPosition = connectionString.IndexOf(";", equalPosition, StringComparison.OrdinalIgnoreCase);

            // If there is no semicolon in the connection string after "password=" do not do further string manipulation
            var second = string.Empty;
            if (separatorPosition > -1)
            {
                second = connectionString[separatorPosition..];
            }

            var first = connectionString.Substring(0, equalPosition + 1);
            return $"{first}*****{second}";
        }

        /// <summary>
        /// Clears the database.
        /// Drops all tables, FK's, PK's and Procedures
        /// </summary>
        /// <param name="cancellationToken">the cancellation token</param>
        /// <returns>void</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Minor Code Smell", "S1192:String literals should not be duplicated", Justification = "SQL Scripts")]
        private async Task ClearDatabaseAsync(CancellationToken cancellationToken)
        {
            /* Source: https://stackoverflow.com/a/36619064/879553 */
            /* Azure friendly */
            /* Declare the variables only once */
            var sb = new StringBuilder();
            sb.AppendLine("DECLARE @name VARCHAR(128)");
            sb.AppendLine("DECLARE @constraint VARCHAR(254)");
            sb.AppendLine("DECLARE @SQL VARCHAR(254)");
            sb.AppendLine(string.Empty);

            /* Disalbe all temporal tables */
            sb.AppendLine("SELECT @name = (SELECT TOP 1 NAME FROM sys.tables WHERE temporal_type = 2)");
            sb.AppendLine(string.Empty);
            sb.AppendLine("WHILE @name is not null");
            sb.AppendLine("BEGIN");
            sb.AppendLine("    SELECT @SQL = 'ALTER TABLE [dbo].[' + RTRIM(@name) +'] SET ( SYSTEM_VERSIONING = OFF)'");
            sb.AppendLine("    EXEC (@SQL)");
            sb.AppendLine("    SELECT @name = (SELECT TOP 1 NAME FROM sys.tables WHERE temporal_type = 2)");
            sb.AppendLine("END");
            sb.AppendLine(string.Empty);

            /* Drop all Foreign Key constraints */
            sb.AppendLine("SELECT @name = (SELECT TOP 1 TABLE_NAME FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS WHERE constraint_catalog=DB_NAME() AND CONSTRAINT_TYPE = 'FOREIGN KEY' ORDER BY TABLE_NAME)");
            sb.AppendLine(string.Empty);
            sb.AppendLine("WHILE @name is not null");
            sb.AppendLine("BEGIN");
            sb.AppendLine("    SELECT @constraint = (SELECT TOP 1 CONSTRAINT_NAME FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS WHERE constraint_catalog=DB_NAME() AND CONSTRAINT_TYPE = 'FOREIGN KEY' AND TABLE_NAME = @name ORDER BY CONSTRAINT_NAME)");
            sb.AppendLine("    WHILE @constraint IS NOT NULL");
            sb.AppendLine("    BEGIN");
            sb.AppendLine("        SELECT @SQL = 'ALTER TABLE [dbo].[' + RTRIM(@name) +'] DROP CONSTRAINT [' + RTRIM(@constraint) +']'");
            sb.AppendLine("        EXEC (@SQL)");
            sb.AppendLine("        PRINT 'Dropped FK Constraint: ' + @constraint + ' on ' + @name");
            sb.AppendLine("        SELECT @constraint = (SELECT TOP 1 CONSTRAINT_NAME FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS WHERE constraint_catalog=DB_NAME() AND CONSTRAINT_TYPE = 'FOREIGN KEY' AND CONSTRAINT_NAME <> @constraint AND TABLE_NAME = @name ORDER BY CONSTRAINT_NAME)");
            sb.AppendLine("    END");
            sb.AppendLine("SELECT @name = (SELECT TOP 1 TABLE_NAME FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS WHERE constraint_catalog=DB_NAME() AND CONSTRAINT_TYPE = 'FOREIGN KEY' ORDER BY TABLE_NAME)");
            sb.AppendLine("END");
            sb.AppendLine(string.Empty);

            /* Drop all Primary Key constraints */
            sb.AppendLine("SELECT @name = (SELECT TOP 1 TABLE_NAME FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS WHERE constraint_catalog=DB_NAME() AND CONSTRAINT_TYPE = 'PRIMARY KEY' ORDER BY TABLE_NAME)");
            sb.AppendLine(string.Empty);
            sb.AppendLine("WHILE @name IS NOT NULL");
            sb.AppendLine("BEGIN");
            sb.AppendLine("    SELECT @constraint = (SELECT TOP 1 CONSTRAINT_NAME FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS WHERE constraint_catalog=DB_NAME() AND CONSTRAINT_TYPE = 'PRIMARY KEY' AND TABLE_NAME = @name ORDER BY CONSTRAINT_NAME)");
            sb.AppendLine("    WHILE @constraint is not null");
            sb.AppendLine("    BEGIN");
            sb.AppendLine("        SELECT @SQL = 'ALTER TABLE [dbo].[' + RTRIM(@name) +'] DROP CONSTRAINT [' + RTRIM(@constraint)+']'");
            sb.AppendLine("        EXEC (@SQL)");
            sb.AppendLine("        PRINT 'Dropped PK Constraint: ' + @constraint + ' on ' + @name");
            sb.AppendLine("        SELECT @constraint = (SELECT TOP 1 CONSTRAINT_NAME FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS WHERE constraint_catalog=DB_NAME() AND CONSTRAINT_TYPE = 'PRIMARY KEY' AND CONSTRAINT_NAME <> @constraint AND TABLE_NAME = @name ORDER BY CONSTRAINT_NAME)");
            sb.AppendLine("    END");
            sb.AppendLine("SELECT @name = (SELECT TOP 1 TABLE_NAME FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS WHERE constraint_catalog=DB_NAME() AND CONSTRAINT_TYPE = 'PRIMARY KEY' ORDER BY TABLE_NAME)");
            sb.AppendLine("END");
            sb.AppendLine(string.Empty);

            /* Drop all tables */
            sb.AppendLine("SELECT @name = (SELECT TOP 1 [name] FROM sysobjects WHERE [type] = 'U' AND category = 0 ORDER BY [name])");
            sb.AppendLine(string.Empty);
            sb.AppendLine("WHILE @name IS NOT NULL");
            sb.AppendLine("BEGIN");
            sb.AppendLine("    SELECT @SQL = 'DROP TABLE [dbo].[' + RTRIM(@name) +']'");
            sb.AppendLine("    EXEC (@SQL)");
            sb.AppendLine("    PRINT 'Dropped Table: ' + @name");
            sb.AppendLine("    SELECT @name = (SELECT TOP 1 [name] FROM sysobjects WHERE [type] = 'U' AND category = 0 AND [name] > @name ORDER BY [name])");
            sb.AppendLine("END");

            /* Drop all Procedures */
            sb.AppendLine("SELECT @name = (SELECT TOP 1 [name] FROM sysobjects WHERE [type] = 'P' ORDER BY [name])");
            sb.AppendLine(string.Empty);
            sb.AppendLine("WHILE @name IS NOT NULL");
            sb.AppendLine("BEGIN");
            sb.AppendLine("    SELECT @SQL = 'DROP PROCEDURE [dbo].[' + RTRIM(@name) +']'");
            sb.AppendLine("    EXEC (@SQL)");
            sb.AppendLine("    PRINT 'Dropped Procedure: ' + @name");
            sb.AppendLine("    SELECT @name = (SELECT TOP 1 [name] FROM sysobjects WHERE [type] = 'P' AND [name] > @name ORDER BY [name])");
            sb.AppendLine("END");

            if (this.Context is not null)
            {
                await this.Context.Database.ExecuteSqlRawAsync(sb.ToString(), cancellationToken).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Creates a snapshot of the database
        /// </summary>
        /// <param name="cancellationToken">the cancellation token</param>
        /// <returns>void</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Security", "EF1002:Review SQL queries for security vulnerabilities", Justification = "Query does not take input from user")]
        private async Task CreateSnapshotAsync(CancellationToken cancellationToken)
        {
            this.Logger.LogInformation("Creating snapshot of database");

            var dbName = this.Context.Database.GetDbConnection().Database;

            // make sure to delete the existing snapshot db
            await this.Context.Database.ExecuteSqlRawAsync($"DROP DATABASE IF EXISTS [{dbName}_tests]", cancellationToken).ConfigureAwait(false);

            using var command = this.Context.Database.GetDbConnection().CreateCommand();
            command.CommandText = $"USE {dbName}; SELECT SERVERPROPERTY('INSTANCEDEFAULTDATAPATH')";
            await this.Context.Database.OpenConnectionAsync(cancellationToken).ConfigureAwait(false);
            var dbPath = await command.ExecuteScalarAsync(cancellationToken).ConfigureAwait(false);
            await this.Context.Database
                .ExecuteSqlRawAsync($"CREATE DATABASE [{dbName}_tests] ON(NAME = {dbName}, FILENAME = '{dbPath}{dbName}_tests.ss') AS SNAPSHOT OF [{dbName}]", cancellationToken)
                .ConfigureAwait(false);
        }

        /// <summary>
        /// Creates the sql user.
        /// </summary>
        private void CreateSqlUser()
        {
            if (!this.Configuration.GetValue<bool>("CreateSqlUserOnMaster"))
            {
                this.Logger.LogInformation("Skipping creating sql users on master database");
                return;
            }

            string sql = @"
IF EXISTS(SELECT * FROM sys.sql_logins WHERE name = 'pedalo01')
DROP LOGIN pedalo01;

CREATE LOGIN pedalo01 WITH PASSWORD = '" + this.Configuration["SqlUserPassword"] + @"';
";
            using var masterConnection = this.CreateMasterDbConnection(this.Context.Database.GetDbConnection().Database);
            using var masterCommand = masterConnection.CreateCommand();
            masterCommand.CommandText = sql;
            masterCommand.ExecuteNonQuery();

            this.Logger.LogInformation("Sql user created on master database");
        }

        /// <summary>
        /// Creates the database context.
        /// </summary>
        /// <param name="currentDbName">The current db name.</param>
        /// <returns>The database context.</returns>
        private SqlConnection CreateMasterDbConnection(string currentDbName)
        {
            SqlConnection conn = new SqlConnection();
            var connectionString = this.Configuration.GetConnectionString("AppDbContext");

            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new InvalidOperationException("Connection string is empty");
            }

            connectionString = connectionString.Replace($"Database={currentDbName};", $"Database=master;", StringComparison.InvariantCultureIgnoreCase).Replace($"Initial Catalog={currentDbName};", $"Initial Catalog=master;", StringComparison.InvariantCultureIgnoreCase);
            conn.ConnectionString = connectionString;
            conn.Open();

            return conn;
        }
    }
}
