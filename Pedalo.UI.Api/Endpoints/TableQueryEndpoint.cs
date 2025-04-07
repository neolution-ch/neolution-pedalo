namespace Pedalo.UI.Api.Endpoints
{
    /// <summary>
    /// A helper class for creating Table Query Endpoints
    /// </summary>
    /// <typeparam name="TModel">The type of the model.</typeparam>
    public abstract class TableQueryEndpoint<TModel>
        : EndpointBaseSync.WithRequest<QueryRequestModel>.WithActionResult<TableQueryResult<TModel>>
    {
    }
}
