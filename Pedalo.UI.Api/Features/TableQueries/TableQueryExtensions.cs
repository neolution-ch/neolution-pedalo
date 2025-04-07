namespace Pedalo.UI.Api.Features.TableQueries
{
    using System.ComponentModel;
    using System.Linq;
    using System.Linq.Dynamic.Core;
    using System.Linq.Expressions;
    using System.Text;

    /// <summary>
    /// Extensions for the table query feature
    /// </summary>
    public static class TableQueryExtensions
    {
        /// <summary>
        /// Converts to query to a table result.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <param name="query">The query.</param>
        /// <param name="expression">The expression.</param>
        /// <param name="options">The options.</param>
        /// <param name="sortFunction">the sort function</param>
        /// <returns>The table query result.</returns>
        public static TableQueryResult<TModel> ToTableQueryResult<TEntity, TModel>(this IQueryable<TEntity> query, Expression<Func<TEntity, TModel>> expression, TableQueryOptions options, Func<IQueryable<TEntity>, TableQueryOptions, IQueryable<TEntity>>? sortFunction)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            query = sortFunction?.Invoke(query, options) ?? OrderByOptions(query, options);

            var result = new TableQueryResult<TModel>
            {
                TotalRecords = query.Count(),
                Records = query.Skip((options.Page - 1) * options.Limit).Take(options.Limit).Select(expression).ToList(),
            };

            return result;
        }

        /// <summary>
        /// Converts to query to a table result.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="query">The query.</param>
        /// <param name="options">The options.</param>
        /// <returns>The table query result.</returns>
        public static TableQueryResult<TEntity> ToTableQueryResult<TEntity>(this IQueryable<TEntity> query, TableQueryOptions options)
        {
            return ToTableQueryResult(query, x => x, options, null);
        }

        /// <summary>
        /// Converts to query to a table result.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <param name="query">The query.</param>
        /// <param name="expression">The expression.</param>
        /// <param name="options">The options.</param>
        /// <returns>The table query result.</returns>
        public static TableQueryResult<TModel> ToTableQueryResult<TEntity, TModel>(this IQueryable<TEntity> query, Expression<Func<TEntity, TModel>> expression, TableQueryOptions options)
        {
            return ToTableQueryResult(query, expression, options, null);
        }

        /// <summary>
        /// Converts the query to a table query result asynchronously.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="query">The query.</param>
        /// <param name="options">The options.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The table query result.</returns>
        public static Task<TableQueryResult<TEntity>> ToTableQueryResultAsync<TEntity>(this IQueryable<TEntity> query, TableQueryOptions options, CancellationToken cancellationToken)
        {
            return ToTableQueryResultAsync(query, x => x, options, null, cancellationToken);
        }

        /// <summary>
        /// Converts the query to a table query result asynchronously.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <param name="query">The query.</param>
        /// <param name="expression">The expression.</param>
        /// <param name="options">The options.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The table query result.</returns>
        public static Task<TableQueryResult<TModel>> ToTableQueryResultAsync<TEntity, TModel>(this IQueryable<TEntity> query, Expression<Func<TEntity, TModel>> expression, TableQueryOptions options, CancellationToken cancellationToken)
        {
            return ToTableQueryResultAsync(query, expression, options, null, cancellationToken);
        }

        /// <summary>
        /// Converts the query to a table query result asynchronously.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <param name="query">The query.</param>
        /// <param name="expression">The expression.</param>
        /// <param name="options">The options.</param>
        /// <param name="sortFunction">The sort function.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The table query result.</returns>
        public static Task<TableQueryResult<TModel>> ToTableQueryResultAsync<TEntity, TModel>(this IQueryable<TEntity> query, Expression<Func<TEntity, TModel>> expression, TableQueryOptions options, Func<IQueryable<TEntity>, TableQueryOptions, IQueryable<TEntity>>? sortFunction, CancellationToken cancellationToken)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            return ToTableQueryResultInternalAsync();

            async Task<TableQueryResult<TModel>> ToTableQueryResultInternalAsync()
            {
                query = sortFunction?.Invoke(query, options) ?? OrderByOptions(query, options);

                var queryResult = new TableQueryResult<TModel>
                {
                    TotalRecords = await query.CountAsync(cancellationToken),
                    Records = await query.Skip((options.Page - 1) * options.Limit).Take(options.Limit).Select(expression).ToListAsync(cancellationToken),
                };

                return queryResult;
            }
        }

        /// <summary>
        /// Applies ordering to the query.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="query">The query.</param>
        /// <param name="options">The options.</param>
        /// <returns>The query with the ordering applied</returns>
        private static IQueryable<TEntity> OrderByOptions<TEntity>(IQueryable<TEntity> query, TableQueryOptions options)
        {
            if (string.IsNullOrWhiteSpace(options.OrderBy))
            {
                // Nothing to sort if OrderBy is not specified
                return query;
            }

            var expression = new StringBuilder(options.OrderBy);

            if (options.SortDirection == ListSortDirection.Descending)
            {
                expression.Append(" desc");
            }

            query = query.OrderBy(expression.ToString());
            return query;
        }
    }
}
