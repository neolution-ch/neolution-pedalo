namespace PedaloWebApp.UI.Api.Endpoints.Pedalos
{
    using PedaloWebApp.UI.Api.Models.Pedalos;

    /// <inheritdoc/>
    public class Query : EndpointBaseAsync
        .WithRequest<QueryRequestModel<PedaloFilter>>
        .WithActionResult<TableQueryResult<PedaloModel>>
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
        public async override Task<ActionResult<TableQueryResult<PedaloModel>>> HandleAsync([FromMultiSource] QueryRequestModel<PedaloFilter> request, CancellationToken cancellationToken = default)
        {
            if (request is null)
            {
                return this.BadRequest("Request cannot be null");
            }

            await using var context = this.contextFactory.CreateReadOnlyContext();

            var query = context.Pedalos
                .Select(x => new PedaloModel
                {
                    PedaloId = x.PedaloId,
                    Name = x.Name,
                    Color = x.Color,
                    Capacity = x.Capacity,
                    HourlyRate = x.HourlyRate,
                })
                .AsQueryable();

            var filter = request.Filter;
            var options = request.Options;

            if (!string.IsNullOrWhiteSpace(filter.Name))
            {
#pragma warning disable CA1310 // Specify StringComparison for correctness - StringComparison.OrdinalIgnoreCase cannot be translated to SQL
                query = query.Where(x => x.Name.StartsWith(filter.Name));
#pragma warning restore CA1310 // Specify StringComparison for correctness
            }

            var result = await query.ToTableQueryResultAsync(options, cancellationToken);
            return result;
        }
    }
}
