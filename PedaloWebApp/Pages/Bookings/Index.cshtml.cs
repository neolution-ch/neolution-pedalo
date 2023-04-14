namespace PedaloWebApp.Pages.Bookings
{
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.EntityFrameworkCore;
    using PedaloWebApp.Core.Domain.Entities;
    using PedaloWebApp.Core.Interfaces.Data;

    public class IndexModel : PageModel
    {
        private readonly IDbContextFactory contextFactory;

        public IndexModel(IDbContextFactory contextFactory)
        {
            this.contextFactory = contextFactory;
        }

        public IReadOnlyList<Booking> Bookings { get; set; }

        public IActionResult OnGet()
        {
            using var context = this.contextFactory.CreateReadOnlyContext();
            this.Bookings = context.Bookings
                .Include(x => x.Customer).OrderBy(x => x.StartDate)
                .Include(x => x.Pedalo)
                .ToList();

            // load the passengers for each booking
            foreach (var booking in this.Bookings)
            {
                booking.BookingPassengers = context.BookingPassengers
                    .Where(x => x.BookingId == booking.BookingId)
                    .ToList();
            }

            return this.Page();
        }
    }
}
