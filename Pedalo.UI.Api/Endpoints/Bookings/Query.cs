namespace Pedalo.UI.Api.Endpoints.Bookings
{
    using Pedalo.UI.Api.Models.Bookings;

    /// <inheritdoc/>
    public class Query : EndpointBaseAsync
        .WithRequest<QueryRequestModel<BookingFilter>>
        .WithActionResult<TableQueryResult<BookingModel>>
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
        public async override Task<ActionResult<TableQueryResult<BookingModel>>> HandleAsync([FromMultiSource] QueryRequestModel<BookingFilter> request, CancellationToken cancellationToken = default)
        {
            if (request is null)
            {
                return this.BadRequest("Request cannot be null");
            }

            await using var context = this.contextFactory.CreateReadOnlyContext();

            var query = context.Bookings
                .Select(x => new BookingModel
                {
                    BookingId = x.BookingId,
                    PedaloName = x.Pedalo.Name,
                    CustomerName = $"{x.Customer.FirstName} {x.Customer.LastName}",
                    StartDate = x.StartDate,
                    EndDate = x.EndDate,
                })
                .AsQueryable();

            var filter = request.Filter;
            var options = request.Options;

#pragma warning disable CA1310 // Specify StringComparison for correctness - StringComparison.OrdinalIgnoreCase cannot be translated to SQL
            if (!string.IsNullOrWhiteSpace(filter.PedaloName))
            {
                query = query.Where(x => x.PedaloName.StartsWith(filter.PedaloName));
            }

            if (!string.IsNullOrWhiteSpace(filter.CustomerName))
            {
                query = query.Where(x => x.CustomerName.StartsWith(filter.CustomerName));
            }
#pragma warning restore CA1310 // Specify StringComparison for correctness

            var result = await query.ToTableQueryResultAsync(options, cancellationToken);
            return result;
        }
    }
}
