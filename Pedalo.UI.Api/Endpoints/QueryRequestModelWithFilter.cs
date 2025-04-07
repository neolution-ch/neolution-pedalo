namespace Pedalo.UI.Api.Endpoints
{
    /// <summary>
    /// The Query Request Model
    /// </summary>
    /// <typeparam name="TFilter">The type of the filter.</typeparam>
    public class QueryRequestModel<TFilter> : QueryRequestModel
        where TFilter : new()
    {
        /// <summary>
        /// Gets or sets the filter.
        /// </summary>
        /// <value>
        /// The filter.
        /// </value>
        [FromQuery]
        public TFilter Filter { get; set; } = new TFilter();
    }
}
