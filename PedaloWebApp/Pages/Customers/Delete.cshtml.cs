namespace PedaloWebApp.Pages.Customers
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
        public CustomerDeleteModel Customer { get; set; }

        public IActionResult OnGet(Guid? id)
        {
            if (id == null)
            {
                return this.BadRequest();
            }

            using var context = this.contextFactory.CreateReadOnlyContext();
            this.Customer = context.Customers
                .Where(m => m.CustomerId == id)
                .Select(x => new CustomerDeleteModel
                {
                    CustomerId = x.CustomerId,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    BirthdayDate = x.BirthdayDate,
                    NumberOfBookings = x.Bookings.Count,
                })
                .FirstOrDefault();
            if (this.Customer == null)
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
            var customer = context.Customers.FirstOrDefault(x => x.CustomerId == this.Customer.CustomerId);
            if (customer == null)
            {
                return this.NotFound();
            }

            try
            {
                context.Customers.Remove(customer);

                context.SaveChanges();
            }
            catch (Exception)
            {
                return this.RedirectToPage("/Error");
            }

            return this.RedirectToPage("./Index");
        }
    }

    public class CustomerDeleteModel
    {
        public Guid CustomerId { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [DataType(DataType.Date)]

        [Display(Name = "Birthday Date")]
        public DateTime BirthdayDate { get; set; }
        public List<Customer> Customers { get; internal set; }

        public int? NumberOfBookings { get; set; }
    }
}
