namespace PedaloWebApp.Tools.DataInitializer.Seeds
{
    using Microsoft.EntityFrameworkCore;
    using Neolution.Extensions.DataSeeding.Abstractions;

    /// <inheritdoc />
    public class Customers : ISeed
    {
        /// <summary>
        /// The context factory
        /// </summary>
        private readonly IDbContextFactory<AppDbContext> contextFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="Customers" /> class.
        /// </summary>
        /// <param name="contextFactory">The context factory.</param>
        public Customers(IDbContextFactory<AppDbContext> contextFactory)
        {
            this.contextFactory = contextFactory;
        }

        /// <summary>
        /// Gets the list of added customers.
        /// </summary>
        internal static List<Customer> AddedCustomers { get; } = [];

        /// <inheritdoc />
        public async Task SeedAsync()
        {
            var customer1 = new Customer
            {
                FirstName = "Wilhelm",
                LastName = "Tell",
                BirthdayDate = new DateTime(1980, 1, 15, 0, 0, 0, DateTimeKind.Utc),
            };
            AddedCustomers.Add(customer1);

            var customer2 = new Customer
            {
                FirstName = "Arthur",
                LastName = "Dent",
                BirthdayDate = new DateTime(1985, 2, 20, 0, 0, 0, DateTimeKind.Utc),
            };
            AddedCustomers.Add(customer2);

            var customer3 = new Customer
            {
                FirstName = "Ford",
                LastName = "Prefect",
                BirthdayDate = new DateTime(1990, 3, 25, 0, 0, 0, DateTimeKind.Utc),
            };
            AddedCustomers.Add(customer3);

            var customer4 = new Customer
            {
                FirstName = "Zaphod",
                LastName = "Beeblebrox",
                BirthdayDate = new DateTime(1975, 4, 10, 0, 0, 0, DateTimeKind.Utc),
            };
            AddedCustomers.Add(customer4);

            var customer5 = new Customer
            {
                FirstName = "Tricia",
                LastName = "McMillan",
                BirthdayDate = new DateTime(1988, 5, 5, 0, 0, 0, DateTimeKind.Utc),
            };
            AddedCustomers.Add(customer5);

            var customer6 = new Customer
            {
                FirstName = "Luke",
                LastName = "Skywalker",
                BirthdayDate = new DateTime(1977, 6, 30, 0, 0, 0, DateTimeKind.Utc),
            };
            AddedCustomers.Add(customer6);

            var customer7 = new Customer
            {
                FirstName = "Han",
                LastName = "Solo",
                BirthdayDate = new DateTime(1982, 7, 15, 0, 0, 0, DateTimeKind.Utc),
            };
            AddedCustomers.Add(customer7);

            var customer8 = new Customer
            {
                FirstName = "Obiwan",
                LastName = "Kenobi",
                BirthdayDate = new DateTime(1970, 8, 25, 0, 0, 0, DateTimeKind.Utc),
            };
            AddedCustomers.Add(customer8);

            await using var context = await this.contextFactory.CreateDbContextAsync();
            context.Customers.AddRange(AddedCustomers);
            await context.SaveChangesAsync();
        }
    }
}
