namespace Pedalo.UI.Api.Endpoints.Customers
{
    using Pedalo.UI.Api.Models.Customers;

    /// <inheritdoc/>
    public class Create : EndpointBaseAsync
    .WithRequest<CustomerModel>
    .WithActionResult<Guid>
    {
        /// <summary>
        /// The context factory
        /// </summary>
        private readonly IAppDbContextFactory contextFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="Create"/> class.
        /// </summary>
        /// <param name="contextFactory">The context factory.</param>
        public Create(IAppDbContextFactory contextFactory)
        {
            this.contextFactory = contextFactory;
        }

        /// <summary>
        /// API method to create a new address entry
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Guid of the created entry</returns>
        [HttpPost("/[baseroute]")]
        public override async Task<ActionResult<Guid>> HandleAsync(CustomerModel request, CancellationToken cancellationToken = default)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            if (request == null)
            {
                return this.BadRequest("No request model specified...");
            }

            if (string.IsNullOrWhiteSpace(request.FirstName))
            {
                return this.BadRequest("No first name specified...");
            }

            if (string.IsNullOrWhiteSpace(request.LastName))
            {
                return this.BadRequest("No last name specified...");
            }

            var customer = new Customer
            {
                CustomerId = Guid.NewGuid(),
                FirstName = request.FirstName,
                LastName = request.LastName,
                BirthdayDate = request.BirthdayDate,
            };

            await using var context = this.contextFactory.CreateContext();
            await context.Customers.AddAsync(customer, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);

            return customer.CustomerId;
        }
    }
}
