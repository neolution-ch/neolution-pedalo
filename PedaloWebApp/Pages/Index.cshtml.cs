namespace PedaloWebApp.Pages
{
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.Extensions.Logging;
    using PedaloWebApp.Core.Domain.Entities;
    using PedaloWebApp.Core.Interfaces.Data;

    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> logger;
        private readonly IDbContextFactory contextFactory;

        public IndexModel(ILogger<IndexModel> logger, IDbContextFactory contextFactory)
        {
            this.logger = logger;
            this.contextFactory = contextFactory;
        }

        public List<PopularPedaloViewModel> PopularPedaloes { get; set; }

        public void OnGet()
        {
            using var context = this.contextFactory.CreateReadOnlyContext();

            // Get the top 3 Pedaloes with the most bookings
            this.PopularPedaloes = context.Pedaloes
                .OrderByDescending(p => p.Bookings.Count)
                .Take(3)
                .Select(p => new PopularPedaloViewModel
                {
                    Name = p.Name,
                    Color = p.Color.ToString(),
                    BookingsCount = p.Bookings.Count
                })
                .ToList();
        }
    }

    public class PopularPedaloViewModel
    {
        public string Name { get; set; }
        public string Color { get; set; }
        public int BookingsCount { get; set; }
    }
}

