namespace PedaloWebApp.Core.Interfaces.Database
{
    /// <summary>
    /// The database migrator.
    /// </summary>
    public interface IDatabaseMigrator
    {
        /// <summary>
        /// Runs the desired database migrations depending on the application environment and configuration
        /// </summary>
        void Run();
    }
}
