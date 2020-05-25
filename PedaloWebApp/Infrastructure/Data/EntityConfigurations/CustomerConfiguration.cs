namespace PedaloWebApp.Infrastructure.Data.EntityConfigurations
{
    using System;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using PedaloWebApp.Core.Domain.Entities;

    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(x => x.CustomerId);
            builder.Property(x => x.FirstName).IsRequired().HasMaxLength(80);
            builder.Property(x => x.LastName).IsRequired().HasMaxLength(80);

            Seed(builder);
        }

        private static void Seed(EntityTypeBuilder<Customer> builder)
        {
            builder.HasData(new Customer
            {
                FirstName = "Wilhelm",
                LastName = "Tell",
                BirthdayDate = new DateTime(1970, 1, 1),
            });

            builder.HasData(new Customer
            {
                FirstName = "Arthur",
                LastName = "Dent",
                BirthdayDate = new DateTime(1970, 2, 1),
            });

            builder.HasData(new Customer
            {
                FirstName = "Ford",
                LastName = "Prefect",
                BirthdayDate = new DateTime(1970, 3, 1),
            });

            builder.HasData(new Customer
            {
                FirstName = "Zaphod",
                LastName = "Beeblebrox",
                BirthdayDate = new DateTime(1970, 4, 1),
            });

            builder.HasData(new Customer
            {
                FirstName = "Tricia",
                LastName = "McMillan",
                BirthdayDate = new DateTime(1970, 5, 1),
            });

            builder.HasData(new Customer
            {
                FirstName = "Luke",
                LastName = "Skywalker",
                BirthdayDate = new DateTime(1970, 6, 1),
            });

            builder.HasData(new Customer
            {
                FirstName = "Han",
                LastName = "Solo",
                BirthdayDate = new DateTime(1970, 7, 1),
            });

            builder.HasData(new Customer
            {
                FirstName = "Obiwan",
                LastName = "Kenobi",
                BirthdayDate = new DateTime(1970, 8, 1),
            });
        }
    }
}
