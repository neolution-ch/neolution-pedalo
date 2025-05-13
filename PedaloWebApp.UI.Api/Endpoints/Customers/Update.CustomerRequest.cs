namespace PedaloWebApp.UI.Api.Endpoints.Customers
{
    using PedaloWebApp.UI.Api.Models.Customers;

    /// <summary>
    /// The Update Customer Request
    /// </summary>
    public class UpdateCustomerRequest
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        [FromRoute]
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the customer.
        /// </summary>
        [FromBody]
        public CustomerModel? Customer { get; set; }
    }
}
