namespace Pedalo.UI.Api.Attributes
{
    using Microsoft.AspNetCore.Mvc.ModelBinding;

    /// <summary>
    /// An attribute that lets you use parameters from different sources
    /// </summary>
    /// <seealso cref="System.Attribute" />
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ModelBinding.IBindingSourceMetadata" />
    [AttributeUsage(AttributeTargets.Parameter)]
    public sealed class FromMultiSourceAttribute : Attribute, IBindingSourceMetadata
    {
        /// <inheritdoc/>
        public BindingSource BindingSource { get; } = CompositeBindingSource.Create(
            new[] { BindingSource.Path, BindingSource.Query },
            nameof(FromMultiSourceAttribute));
    }
}
