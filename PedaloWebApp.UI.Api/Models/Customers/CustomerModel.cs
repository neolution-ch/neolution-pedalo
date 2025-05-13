namespace PedaloWebApp.UI.Api.Models.Customers
{
    /// <summary>
    /// Represents a customer model.
    /// </summary>
    public class CustomerModel
    {
        /// <summary>
        /// Gets or sets the unique identifier for the customer.
        /// </summary>
        public Guid CustomerId { get; set; }

        /// <summary>
        /// Gets or sets the first name of the customer.
        /// </summary>
        public required string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name of the customer.
        /// </summary>
        public required string LastName { get; set; }

        /// <summary>
        /// Gets or sets the birthday date of the customer.
        /// </summary>
        public DateTime BirthdayDate { get; set; }
    }
}
