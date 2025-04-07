namespace Pedalo.Infrastructure.Extensions
{
    /// <summary>
    /// EntityTypeBuilderExtensions
    /// </summary>
    public static class EntityTypeBuilderExtensions
    {
        /// <summary>
        /// Makes the a table temporal.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="builder">The builder.</param>
        /// <returns>The entity type builder.</returns>
        public static EntityTypeBuilder<TEntity> MakeTemporal<TEntity>(this EntityTypeBuilder<TEntity> builder)
            where TEntity : class
        {
            if (builder == null)
            {
                throw new System.ArgumentNullException(nameof(builder));
            }

            var tableName = builder.Metadata.GetTableName();

            if (tableName is null)
            {
                return builder;
            }

            return builder.ToTable(tableName, x => x.IsTemporal());
        }
    }
}
