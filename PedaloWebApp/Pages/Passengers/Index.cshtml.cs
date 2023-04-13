namespace PedaloWebApp.Pages.Passengers
{
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using PedaloWebApp.Core.Domain.Entities;
    using PedaloWebApp.Core.Interfaces.Data;

    public class IndexModel : PageModel
    {
        private readonly IDbContextFactory contextFactory;

        public IndexModel(IDbContextFactory contextFactory)
        {
            this.contextFactory = contextFactory;
        }

        public IReadOnlyList<Passenger> Passengers { get; set; }

        public IActionResult OnGet()
        {
            using var context = this.contextFactory.CreateReadOnlyContext();
            this.Passengers = context.Passengers.OrderBy(x => x.FirstName).ToList();
            return this.Page();
        }
    }
}
