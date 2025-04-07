namespace Pedalo.UI.Api.Features.TableQueries
{
    /// <summary>
    /// The table query result
    /// </summary>
    /// <typeparam name="TModel">The type of the model.</typeparam>
    public class TableQueryResult<TModel>
    {
        /// <summary>
        /// Gets or sets the total records.
        /// </summary>
        public int TotalRecords { get; set; }

        /// <summary>
        /// Gets or sets the records.
        /// </summary>
        public IList<TModel> Records { get; set; } = new List<TModel>();
    }
}
