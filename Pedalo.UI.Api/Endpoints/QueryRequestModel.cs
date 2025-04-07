namespace Pedalo.UI.Api.Endpoints
{
    /// <summary>
    /// The Query Request Model
    /// </summary>
    public class QueryRequestModel
    {
        /// <summary>
        /// Gets or sets the options.
        /// </summary>
        /// <value>
        /// The options.
        /// </value>
        [FromQuery]
        public TableQueryOptions Options { get; set; } = new TableQueryOptions();
    }
}
