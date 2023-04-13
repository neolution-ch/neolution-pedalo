namespace PedaloWebApp.Pages.Passengers
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using PedaloWebApp.Core.Domain.Entities;
    using PedaloWebApp.Core.Interfaces.Data;

    public class DeleteModel : PageModel
    {
        private readonly IDbContextFactory contextFactory;

        public DeleteModel(IDbContextFactory contextFactory)
        {
            this.contextFactory = contextFactory;
        }

        [BindProperty]
        public PassengerDeleteModel Passenger { get; set; }

        public IActionResult OnGet(Guid? id)
        {
            if (id == null)
            {
                return this.BadRequest();
            }

            using var context = this.contextFactory.CreateReadOnlyContext();
            this.Passenger = context.Passengers
                .Where(m => m.PassengerId == id)
                .Select(x => new PassengerDeleteModel
                {
                    PassengerId = x.PassengerId,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    NumberOfBookings = x.BookingPassengers.Count,
                })
                .FirstOrDefault();
            if (this.Passenger == null)
            {
                return this.NotFound();
            }

            return this.Page();
        }

        public IActionResult OnPost()
        {
            if (!this.ModelState.IsValid)
            {
                return this.Page();
            }

            using var context = this.contextFactory.CreateContext();
            var passenger = context.Passengers.FirstOrDefault(x => x.PassengerId == this.Passenger.PassengerId);
            if (passenger == null)
            {
                return this.NotFound();
            }

            try
            {
                context.Passengers.Remove(passenger);

                context.SaveChanges();
            }
            catch (Exception)
            {
                return this.RedirectToPage("/Error");
            }

            return this.RedirectToPage("./Index");
        }
    }

    public class PassengerDeleteModel
    {
        public Guid PassengerId { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        public int? NumberOfBookings { get; set; }
    }
}
