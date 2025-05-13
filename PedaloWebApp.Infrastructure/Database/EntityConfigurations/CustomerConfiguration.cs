namespace PedaloWebApp.Infrastructure.Database.EntityConfigurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    /// <summary>
    /// Configures the Customer entity.
    /// </summary>
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            ArgumentNullException.ThrowIfNull(builder);

            builder.HasKey(x => x.CustomerId);
            builder.Property(x => x.FirstName).HasMaxLength(80);
            builder.Property(x => x.LastName).HasMaxLength(80);
        }
    }
}
