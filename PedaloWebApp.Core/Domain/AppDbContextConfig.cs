namespace PedaloWebApp.Core.Domain
{
    using System;
    using System.Reflection;

    /// <summary>
    /// Application database context options.
    /// </summary>
    public class AppDbContextConfig
    {
        /// <summary>
        /// Gets or sets the tenant identifier.
        /// </summary>
        public Guid? TenantId { get; set; }

        /// <summary>
        /// Gets or sets the entity configurations assembly.
        /// </summary>
        public Assembly? EntityConfigurationsAssembly { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to perform checks at design time.
        /// </summary>
        /// <value>
        ///   <c>true</c> if perform checks at design time; otherwise, <c>false</c>.
        /// </value>
        public bool PerformChecks { get; set; }
    }
}
