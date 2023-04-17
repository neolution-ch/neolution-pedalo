namespace PedaloWebApp.Pages.Bookings
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.EntityFrameworkCore;
    using PedaloWebApp.Core.Domain.Entities;
    using PedaloWebApp.Core.Interfaces.Data;
    using PedaloWebApp.Pages.Customers;

    public class CreateModel : PageModel
    {
        private readonly IDbContextFactory contextFactory;

        public CreateModel(IDbContextFactory contextFactory)
        {
            this.contextFactory = contextFactory;
        }

        [BindProperty]
        public BookingCreateModel Booking { get; set; }

        [BindProperty]
        public List<Pedalo> Pedalo { get; set; }

        [BindProperty]
        public List<Customer> Customer { get; set; }

        public IActionResult OnGet()
        {
            using var context = this.contextFactory.CreateContext();
            this.Pedalo = context.Pedaloes.OrderBy(x => x.Name).ToList();
            this.Customer = context.Customers.OrderBy(x => x.FirstName).ToList();
            return this.Page();
        }

        public IActionResult OnPost()
        {
            if (!this.ModelState.IsValid)
            {
                return this.Page();
            }

            using var context = this.contextFactory.CreateContext();
            var booking = new Booking
            {
                BookingId = Guid.NewGuid(),
                CustomerId = this.Booking.CustomerId,
                PedaloId = this.Booking.PedaloId,
                StartDate = this.Booking.StartDate,
                EndDate = this.Booking.EndDate,
            };

            try
            {
                context.Bookings.Add(booking);
                context.SaveChanges();
            }
            catch (Exception)
            {
                return this.RedirectToPage("/Error");
            }

            // Get the Pedalo object based on the PedaloId property of the Booking object
            var pedalo = context.Pedaloes.FirstOrDefault(p => p.PedaloId == this.Booking.PedaloId);

            // Redirect to the Add page for Passengers
            return this.RedirectToPage("/Bookings/AddPassenger", new { bookingId = booking.BookingId});
        }

    }

    public class BookingCreateModel
    {
        public Guid BookingId { get; set; }
        public Guid CustomerId { get; set; }
        public Guid PedaloId { get; set; }

        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        [Display(Name = "End Date")]
        public DateTime? EndDate { get; set; }
        public Pedalo Pedalo { get; set; }
        public Customer Customer { get; set; }
    }
}