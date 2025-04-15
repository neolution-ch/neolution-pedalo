namespace PedaloWebApp.UI.Api.Endpoints
{
    /// <summary>
    /// A helper class for creating Table Query Endpoints
    /// </summary>
    /// <typeparam name="TModel">The type of the model.</typeparam>
    /// <typeparam name="TFilter">The type of the filter.</typeparam>
    public abstract class TableQueryEndpoint<TModel, TFilter>
        : EndpointBaseSync.WithRequest<QueryRequestModel<TFilter>>.WithActionResult<TableQueryResult<TModel>>
        where TFilter : new()
    {
    }
}
