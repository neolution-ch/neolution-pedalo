namespace Pedalo.UI.Api.Endpoints.Customers
{
    using Pedalo.UI.Api.Models.Customers;

    /// <inheritdoc/>
    public class Read : EndpointBaseAsync
    .WithRequest<Guid>
    .WithActionResult<CustomerModel>
    {
        /// <summary>
        /// The context factory
        /// </summary>
        private readonly IAppDbContextFactory contextFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="Read"/> class.
        /// </summary>
        /// <param name="contextFactory">The context factory.</param>
        public Read(IAppDbContextFactory contextFactory)
        {
            this.contextFactory = contextFactory;
        }

        /// <summary>
        /// API method to return the specified entry.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// The specified entry.
        /// </returns>
        [HttpGet("/[baseroute]/{id}")]
        public override async Task<ActionResult<CustomerModel>> HandleAsync([FromRoute] Guid id, CancellationToken cancellationToken = default)
        {
            await using var context = this.contextFactory.CreateReadOnlyContext();
            var entry = await context.Customers
                .Select(x => new CustomerModel
                {
                    CustomerId = x.CustomerId,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    BirthdayDate = x.BirthdayDate,
                })
                .FirstOrDefaultAsync(x => x.CustomerId == id, cancellationToken);

            if (entry is null)
            {
                return this.NotFound();
            }

            return entry;
        }
    }
}
