namespace PedaloWebApp.UI.Api.Endpoints.Customers
{
    using PedaloWebApp.UI.Api.Models.Customers;

    /// <inheritdoc/>
    public class Query : EndpointBaseAsync
        .WithRequest<QueryRequestModel<CustomerFilter>>
        .WithActionResult<TableQueryResult<CustomerModel>>
    {
        /// <summary>
        /// The context factory
        /// </summary>
        private readonly IAppDbContextFactory contextFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="Query"/> class.
        /// </summary>
        /// <param name="contextFactory">The context factory.</param>
        public Query(IAppDbContextFactory contextFactory)
        {
            this.contextFactory = contextFactory;
        }

        /// <summary>
        /// Provides filter functionality for tables.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// The table query result.
        /// </returns>
        [HttpGet("/[baseroute]")]
        public async override Task<ActionResult<TableQueryResult<CustomerModel>>> HandleAsync([FromMultiSource] QueryRequestModel<CustomerFilter> request, CancellationToken cancellationToken = default)
        {
            if (request is null)
            {
                return this.BadRequest("Request cannot be null");
            }

            await using var context = this.contextFactory.CreateReadOnlyContext();

            var query = context.Customers
                .Select(x => new CustomerModel
                {
                    CustomerId = x.CustomerId,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    BirthdayDate = x.BirthdayDate,
                })
                .AsQueryable();

            var filter = request.Filter;
            var options = request.Options;

#pragma warning disable CA1310 // Specify StringComparison for correctness - StringComparison.OrdinalIgnoreCase cannot be translated to SQL
            if (!string.IsNullOrWhiteSpace(filter.FirstName))
            {
                query = query.Where(x => x.FirstName.StartsWith(filter.FirstName));
            }

            if (!string.IsNullOrWhiteSpace(filter.LastName))
            {
                query = query.Where(x => x.LastName.StartsWith(filter.LastName));
            }
#pragma warning restore CA1310 // Specify StringComparison for correctness

            var result = await query.ToTableQueryResultAsync(options, cancellationToken);
            return result;
        }
    }
}
