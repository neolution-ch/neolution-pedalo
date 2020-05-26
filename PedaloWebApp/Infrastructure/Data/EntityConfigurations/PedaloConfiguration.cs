namespace PedaloWebApp.Infrastructure.Data.EntityConfigurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using PedaloWebApp.Core.Domain.Entities;

    public class PedaloConfiguration : IEntityTypeConfiguration<Pedalo>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<Pedalo> builder)
        {
            builder.HasKey(x => x.PedaloId);
            builder.Property(x => x.Name).IsRequired().HasMaxLength(40);
        }
    }
}
