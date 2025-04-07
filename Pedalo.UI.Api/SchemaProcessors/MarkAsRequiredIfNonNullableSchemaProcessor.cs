namespace Pedalo.UI.Api.SchemaProcessors
{
    using NJsonSchema;
    using NJsonSchema.Generation;

    /// <summary>
    /// This Schema Processor will mark properties that are not nullable as required in the openapi schema.
    /// </summary>
    /// <seealso cref="NJsonSchema.Generation.ISchemaProcessor" />
    public class MarkAsRequiredIfNonNullableSchemaProcessor : ISchemaProcessor
    {
        /// <inheritdoc/>
        public void Process(SchemaProcessorContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            foreach (var (propName, prop) in context.Schema.ActualProperties)
            {
                if (!prop.IsNullable(SchemaType.OpenApi3))
                {
                    prop.IsRequired = true;
                }
            }
        }
    }
}
