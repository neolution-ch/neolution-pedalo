namespace Pedalo.UI.Api.Features.TableQueries
{
    /// <summary>
    /// Interface that specify how should behave a controller that return the list of objects for a table with paging, sorting and filtering
    /// </summary>
    /// <typeparam name="TModel">The returned model type.</typeparam>
    /// <typeparam name="TFilter">The filter type.</typeparam>
    public interface ITableQueryController<TModel, in TFilter>
        where TFilter : new()
    {
        /// <summary>
        /// Provides filter functionality for tables.
        /// </summary>
        /// <param name="filter">The filter</param>
        /// <param name="options">The options.</param>
        /// <returns>
        /// The table query result.
        /// </returns>
        [HttpPost("query")]
        ActionResult<TableQueryResult<TModel>> Query([FromBody] TFilter filter, [FromQuery] TableQueryOptions options);
    }
}
