namespace Pedalo.UI.Api.Models.Customers
{
    /// <summary>
    /// Represents a filter for customer queries.
    /// </summary>
    public class CustomerFilter
    {
        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        public string? FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        public string? LastName { get; set; }
    }
}
