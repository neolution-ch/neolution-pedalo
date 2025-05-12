namespace PedaloWebApp.UI.Api.UnitTests.Endpoints.Customers
{
    using AutoFixture.AutoNSubstitute;
    using Microsoft.AspNetCore.Mvc;
    using NSubstitute;
    using PedaloWebApp.TestUtilities.Fakes;
    using PedaloWebApp.UI.Api.Endpoints.Customers;
    using PedaloWebApp.UI.Api.Models.Customers;

    /// <summary>
    /// Unit tests for the <see cref="Read"/> endpoint.
    /// </summary>
    public class ReadTests
    {
        /// <summary>
        /// Fixture used to create mock objects and test data.
        /// </summary>
        private readonly IFixture fixture;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReadTests"/> class.
        /// </summary>
        public ReadTests()
        {
            this.fixture = new Fixture().Customize(new AutoNSubstituteCustomization());
        }

        /// <summary>
        /// Tests that <see cref="Read.HandleAsync"/> returns a <see cref="CustomerModel"/> when the customer exists.
        /// </summary>
        /// <returns>A task representing the asynchronous operation.</returns>
        [Fact]
        public async Task HandleAsync_ExistingCustomer_ReturnsCustomerModel()
        {
            // Arrange
            var fakeDbContextFactory = new FakeAppDbContextFactory(useInMemory: true);
            var fakeContext = fakeDbContextFactory.CreateContext();
            var testCustomer = new Customer
            {
                CustomerId = Guid.NewGuid(),
                FirstName = "John",
                LastName = "Doe",
                BirthdayDate = DateTime.Today.AddYears(-30),
            };

            fakeContext.Customers.Add(testCustomer);
            await fakeContext.SaveChangesAsync();

            var mockFactory = this.fixture.Create<IAppDbContextFactory>();
            mockFactory.CreateReadOnlyContext().Returns(fakeContext);

            var sut = new Read(mockFactory);

            // Act
            var result = await sut.HandleAsync(testCustomer.CustomerId, CancellationToken.None);

            // Assert
            var customerModel = result.Value;
            customerModel.ShouldNotBeNull();
            customerModel.CustomerId.ShouldBe(testCustomer.CustomerId);
        }

        /// <summary>
        /// Tests that <see cref="Read.HandleAsync"/> returns a <see cref="NotFoundResult"/> when the customer does not exist.
        /// </summary>
        /// <returns>A task representing the asynchronous operation.</returns>
        [Fact]
        public async Task HandleAsync_NonExistingCustomer_ReturnsNotFound()
        {
            // Arrange
            var fakeDbContextFactory = new FakeAppDbContextFactory(useInMemory: true);
            var fakeContext = fakeDbContextFactory.CreateContext();
            var mockFactory = this.fixture.Create<IAppDbContextFactory>();
            mockFactory.CreateReadOnlyContext().Returns(fakeContext);

            var sut = new Read(mockFactory);

            // Act
            var result = await sut.HandleAsync(Guid.NewGuid(), CancellationToken.None);

            // Assert
            result.Result.ShouldBeOfType<NotFoundResult>();
        }
    }
}
