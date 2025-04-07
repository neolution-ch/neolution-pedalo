namespace Pedalo.Core.Interfaces.Database
{
    /// <summary>
    /// The database context factory
    /// </summary>
    public interface IAppDbContextFactory
    {
        /// <summary>
        /// Creates a new database context.
        /// </summary>
        /// <returns>The <see cref="AppDbContext"/>.</returns>
        AppDbContext CreateContext();

        /// <summary>
        /// Creates a new database context.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        /// <returns>The <see cref="AppDbContext"/>.</returns>
        AppDbContext CreateContext(string connectionString);

        /// <summary>
        /// Creates a new database context without the change tracker attached for better reading performance.
        /// </summary>
        /// <returns>The <see cref="AppDbContext"/>.</returns>
        AppDbContext CreateReadOnlyContext();
    }
}
