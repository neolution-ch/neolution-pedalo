namespace Pedalo.UI.Api.Endpoints.Customers
{
    using Pedalo.UI.Api.Models.Customers;
    using QuestPDF.Fluent;
    using QuestPDF.Helpers;

    /// <inheritdoc/>
    public class Pdf : EndpointBaseAsync
    .WithRequest<Guid>
    .WithActionResult<byte[]>
    {
        /// <summary>
        /// The context factory
        /// </summary>
        private readonly IAppDbContextFactory contextFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="Pdf"/> class.
        /// </summary>
        /// <param name="contextFactory">The context factory.</param>
        public Pdf(IAppDbContextFactory contextFactory)
        {
            this.contextFactory = contextFactory;
        }

        /// <summary>
        /// API method to return the specified entry.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// The specified entry.
        /// </returns>
        [HttpGet("/[baseroute]/pdf/{id}")]
        public override async Task<ActionResult<byte[]>> HandleAsync([FromRoute] Guid id, CancellationToken cancellationToken = default)
        {
            await using var context = this.contextFactory.CreateReadOnlyContext();
            var entry = await context.Customers
                .Select(x => new CustomerModel
                {
                    CustomerId = x.CustomerId,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    BirthdayDate = x.BirthdayDate,
                })
                .FirstOrDefaultAsync(x => x.CustomerId == id, cancellationToken);

            if (entry is null)
            {
                return this.NotFound();
            }

            var document = QuestPDF.Fluent.Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.DefaultTextStyle(x => x.FontFamily("Arial"));
                    page.MarginTop(30);
                    page.MarginBottom(50);
                    page.MarginHorizontal(40);
                    page.Header()
                        .Text("Customer")
                        .SemiBold().FontSize(36).FontColor(Colors.Blue.Medium);
                    page.Content()
                        .PaddingTop(10)
                        .Text(text =>
                        {
                            text.Line($"ID: {entry.CustomerId.ToString().ToUpperInvariant()}").FontColor(Colors.Grey.Lighten2);
                            text.Line($"Last name: {entry.LastName.ToUpperInvariant()}").SemiBold();
                            text.Line($"First name: {entry.FirstName}").SemiBold();
                            text.Line($"Birthdate: {entry.BirthdayDate.ToShortDateString()}");
                        });
                });
            });

            return this.File(document.GeneratePdf(), "application/pdf");
        }
    }
}
