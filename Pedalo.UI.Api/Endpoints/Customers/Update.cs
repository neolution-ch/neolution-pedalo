namespace Pedalo.UI.Api.Endpoints.Customers
{
    /// <inheritdoc/>
    public class Update : EndpointBaseAsync
    .WithRequest<UpdateCustomerRequest>
    .WithActionResult<bool>
    {
        /// <summary>
        /// The context factory
        /// </summary>
        private readonly IAppDbContextFactory contextFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="Update"/> class.
        /// </summary>
        /// <param name="contextFactory">The context factory.</param>
        public Update(IAppDbContextFactory contextFactory)
        {
            this.contextFactory = contextFactory;
        }

        /// <summary>
        /// API method to update an existing entry.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns><c>true</c> if successful; otherwise <c>false</c>.</returns>
        [HttpPut("/[baseroute]/{Id}")]
        public override async Task<ActionResult<bool>> HandleAsync([FromMultiSource] UpdateCustomerRequest request, CancellationToken cancellationToken = default)
        {
            if (request is null)
            {
                return this.BadRequest("Request cannot be null");
            }

            var model = request.Customer;
            var id = request.Id;

            if (model == null)
            {
                return this.BadRequest("No request model specified...");
            }

            if (string.IsNullOrWhiteSpace(model.FirstName))
            {
                return this.BadRequest("No first name specified...");
            }

            if (string.IsNullOrWhiteSpace(model.LastName))
            {
                return this.BadRequest("No last name specified...");
            }

            await using var context = this.contextFactory.CreateContext();

            var entry = await context.Customers.FirstOrDefaultAsync(x => x.CustomerId == id, cancellationToken);
            if (entry == null)
            {
                return false;
            }

            entry.FirstName = model.FirstName;
            entry.LastName = model.LastName;
            entry.BirthdayDate = model.BirthdayDate;

            await context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
