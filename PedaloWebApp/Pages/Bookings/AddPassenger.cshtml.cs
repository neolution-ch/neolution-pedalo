using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PedaloWebApp.Core.Domain.Entities;
using PedaloWebApp.Core.Interfaces.Data;
using PedaloWebApp.Pages.Bookings;

namespace PedaloWebApp.Pages.Bookings
{
    public class AddPassengerModel : PageModel
    {
        private readonly IDbContextFactory contextFactory;

        public AddPassengerModel(IDbContextFactory contextFactory)
        {
            this.contextFactory = contextFactory;
        }

        [BindProperty(SupportsGet = true)]
        public Guid BookingId { get; set; }

        [BindProperty(SupportsGet = true)]
        public int Capacity { get; set; }

        [BindProperty]
        public List<Passenger> Passenger { get; set; }

        [BindProperty]
        public BookingAddPassengerModel AddPassenger { get; set; }

        [BindProperty]
        public Guid[] PassengerId { get; set; }

        public IActionResult OnGet()
        {
            if (this.Capacity <= 1)
            {
                return this.RedirectToPage("/Bookings/Index");
            }

            using var context = this.contextFactory.CreateContext();
            this.Passenger = context.Passengers.OrderBy(x => x.FirstName).ToList();
            return this.Page();
        }

        public IActionResult OnPost()
        {
            if (!this.ModelState.IsValid)
            {
                return this.Page();
            }

            using var context = this.contextFactory.CreateContext();
            //For-Each Schleife, damit alle PassengerIds gespeichert werden
            foreach (var item in PassengerId)
            {
                var passenger = new BookingPassengers
                {
                    BookingPassengersId = Guid.NewGuid(),
                    PassengerId = item,
                    BookingId = this.BookingId
                };

                context.BookingPassengers.Add(passenger);
            }

            try
            {
                context.SaveChanges();
            }
            catch (Exception)
            {
                return this.RedirectToPage("/Error");
            }

            return this.RedirectToPage("./Index");
        }
    }

    public class BookingAddPassengerModel
    {
        public Guid BookingPassengersId { get; set; }
        public Guid PassengerId { get; set; }
        public Passenger Passenger { get; set; }
    }
}
