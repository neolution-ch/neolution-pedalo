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

    public class EditModel : PageModel
    {
        private readonly IDbContextFactory contextFactory;

        public EditModel(IDbContextFactory contextFactory)
        {
            this.contextFactory = contextFactory;
        }

        [BindProperty]
        public BookingEditModel Booking { get; set; }

        [BindProperty]
        public List<Pedalo> Pedalo { get; set; }

        [BindProperty]
        public List<Customer> Customer { get; set; }

        public IActionResult OnGet(Guid? id)
        {
            if (id == null)
            {
                return this.BadRequest();
            }

            using var context = this.contextFactory.CreateReadOnlyContext();
            this.Booking = context.Bookings
                .Where(m => m.BookingId == id)
                .Select(x => new BookingEditModel
                {
                    BookingId = x.BookingId,
                    CustomerId = x.CustomerId,
                    PedaloId = x.PedaloId,
                    StartDate = x.StartDate,
                    EndDate = x.EndDate,
                    Pedalo = x.Pedalo,
                    Customer = x.Customer,
                })
                .FirstOrDefault();
            this.Pedalo = context.Pedaloes.ToList();
            this.Customer = context.Customers.ToList();


            return this.Page();
        }

        public IActionResult OnPost()
        {
            if (!this.ModelState.IsValid)
            {
                return this.Page();
            }

            using var context = this.contextFactory.CreateContext();
            var booking = context.Bookings.FirstOrDefault(x => x.BookingId == this.Booking.BookingId);
            if (booking == null)
            {
                return this.NotFound();
            }

            try
            {
                booking.BookingId = this.Booking.BookingId;
                booking.CustomerId = this.Booking.CustomerId;
                booking.PedaloId = this.Booking.PedaloId;
                booking.StartDate = this.Booking.StartDate;
                booking.EndDate = this.Booking.EndDate;

                context.SaveChanges();
            }
            catch (Exception)
            {
                return this.RedirectToPage("/Error");
            }

            return this.RedirectToPage("./Index");
        }

    }

    public class BookingEditModel
    {
        public Guid BookingId { get; set; }
        public Guid CustomerId { get; set; }
        public Guid PedaloId { get; set; }
        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }
        public Pedalo Pedalo { get; set; }
        public Customer Customer { get; set; }

    }
}
