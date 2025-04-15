namespace PedaloWebApp.UI.Api.Endpoints.Customers
{
    /// <inheritdoc/>
    public class Delete : EndpointBaseAsync
    .WithRequest<Guid>
    .WithActionResult<bool>
    {
        /// <summary>
        /// The context factory
        /// </summary>
        private readonly IAppDbContextFactory contextFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="Delete"/> class.
        /// </summary>
        /// <param name="contextFactory">The context factory.</param>
        public Delete(IAppDbContextFactory contextFactory)
        {
            this.contextFactory = contextFactory;
        }

        /// <summary>
        /// API method to delete an existing entry.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns><c>true</c> if successful; otherwise <c>false</c>.</returns>
        [HttpDelete("/[baseroute]/{id}")]
        public override async Task<ActionResult<bool>> HandleAsync([FromRoute] Guid id, CancellationToken cancellationToken = default)
        {
            await using var context = this.contextFactory.CreateContext();

            var entry = await context.Customers
               .FirstOrDefaultAsync(x => x.CustomerId == id, cancellationToken);

            if (entry == null)
            {
                return false;
            }

            context.Customers.Remove(entry);
            await context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
