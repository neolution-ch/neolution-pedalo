namespace PedaloWebApp.UI.Api.Features.TableQueries
{
    using System.ComponentModel;

    /// <summary>
    /// Options for the table query.
    /// </summary>
    public class TableQueryOptions
    {
        /// <summary>
        /// Gets or sets the limit. The table query result will contain maximum this amount of records.
        /// </summary>
        [DefaultValue(25)]
        public int Limit { get; set; } = 25;

        /// <summary>
        /// Gets or sets the page of the paged table query result.
        /// </summary>
        [DefaultValue(1)]
        public int Page { get; set; } = 1;

        /// <summary>
        /// Gets or sets the OrderBy expression. You can sort by multiple columns by comma separating them.
        /// </summary>
        public string? OrderBy { get; set; }

        /// <summary>
        /// Gets or sets the sort direction.
        /// </summary>
        [DefaultValue(ListSortDirection.Ascending)]
        public ListSortDirection SortDirection { get; set; }
    }
}
