namespace PedaloWebApp.Tools.DataInitializer.Commands.Start
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Logging;
    using Neolution.DotNet.Console.Abstractions;
    using PedaloWebApp.Tools.DataInitializer.Initializers;

    /// <summary>
    /// The default command of the application.
    /// </summary>
    /// <seealso cref="IDotNetConsoleCommand{TOptions}" />
    public class StartCommand : IDotNetConsoleCommand<StartOptions>
    {
        /// <summary>
        /// The configuration
        /// </summary>
        private readonly IConfiguration configuration;

        /// <summary>
        /// The application database context initializer
        /// </summary>
        private readonly DbContextInitializer<AppDbContext> appDbContextInitializer;

        /// <summary>
        /// The logger
        /// </summary>
        private readonly ILogger<StartCommand> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="StartCommand"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="configuration">the configuration</param>
        /// <param name="appDbContextInitializer">the db context initializer</param>
        public StartCommand(ILogger<StartCommand> logger, IConfiguration configuration, AppDbContextInitializer appDbContextInitializer)
        {
            this.logger = logger;
            this.configuration = configuration;
            this.appDbContextInitializer = appDbContextInitializer;
        }

        /// <summary>
        /// Gets or sets the cancellation token
        /// </summary>
        private static CancellationToken CancellationToken { get; set; } = CancellationToken.None;

        /// <summary>
        /// Runs the command with the specified options.
        /// </summary>
        /// <param name="options">The options.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The <see cref="Task"/>.</returns>
        public Task RunAsync(StartOptions options, CancellationToken cancellationToken)
        {
            if (options is null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            return RunInternalAsync();

            async Task RunInternalAsync()
            {
                this.logger.LogInformation("Starting data initialization");

                var silent = this.configuration.GetValue<bool>("Silent");

                // Detect if the connection string is connecting to a local SQL Server instance
                var localDataSources = new List<string> { ".", "(local)", "localhost", "127.0.0.1", "(localdb)", "host.docker.internal" };
                var dataSource = this.appDbContextInitializer.Context.Database.GetDbConnection().DataSource;
                var isLocalConnectionString = localDataSources.Any(local => dataSource.StartsWith(local, StringComparison.OrdinalIgnoreCase));

                // If not running silently and the connection string is not local, prompt the user for confirmation
                if (!silent && !isLocalConnectionString)
                {
                    this.logger.LogWarning("Danger: The Initializer will connect to {dataSource} and may delete all data. If you are sure, type YES to proceed. Anything else will abort the job.", dataSource);

                    if (!(Console.ReadLine() ?? string.Empty).Equals("yes", StringComparison.OrdinalIgnoreCase))
                    {
                        this.logger.LogInformation("Initializer aborted, confirmation was not YES.");
                        return;
                    }
                }

                var sw = new Stopwatch();
                sw.Start();

                try
                {
                    // Run initializer for AppDbContext
                    await this.appDbContextInitializer.CreateDatabaseAsync(CancellationToken).ConfigureAwait(false);
                }
                catch (Exception ex)
                {
                    this.logger.LogCritical(ex, "***** An ERROR occured: Press enter to continue *****");
                    if (ex.InnerException != null)
                    {
                        this.logger.LogCritical(ex, "InnerException");
                    }

                    throw;
                }

                sw.Stop();

                this.logger.LogInformation("Total Duration: {elapsed}", sw.Elapsed);
                this.logger.LogInformation("All initialization work done. Have fun!");

                if (silent)
                {
                    return;
                }

                this.logger.LogInformation("Press enter to exit");
                Console.ReadLine();
            }
        }
    }
}
