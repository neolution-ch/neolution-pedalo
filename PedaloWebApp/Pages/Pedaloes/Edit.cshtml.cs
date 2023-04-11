namespace PedaloWebApp.Pages.Pedaloes
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
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
        public PedaloEditModel Pedalo { get; set; }
        public IActionResult OnGet(Guid? id)
        {
            if (id == null)
            {
                return this.BadRequest();
            }

            using var context = this.contextFactory.CreateReadOnlyContext();
            this.Pedalo = context.Pedaloes
                .Where(m => m.PedaloId == id)
                .Select(x => new PedaloEditModel
                {
                    PedaloId = x.PedaloId,
                    Name = x.Name,
                    Color = x.Color,
                    Capacity = x.Capacity,
                    HourlyRate = x.HourlyRate,
                })
                .FirstOrDefault();

            if (this.Pedalo == null)
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
            var pedalo = context.Pedaloes.FirstOrDefault(x => x.PedaloId == this.Pedalo.PedaloId);
            if (pedalo == null)
            {
                return this.NotFound();
            }

            try
            {
                pedalo.Name = this.Pedalo.Name;
                pedalo.Color = this.Pedalo.Color;
                pedalo.Capacity = this.Pedalo.Capacity;
                pedalo.HourlyRate = this.Pedalo.HourlyRate;

                context.SaveChanges();
            }
            catch (Exception)
            {
                return this.RedirectToPage("/Error");
            }

            return this.RedirectToPage("./Index");
        }
    }

    public class PedaloEditModel
    {
        public Guid PedaloId { get; set; }
        public string Name { get; set; }
        public PedaloColor Color { get; set; }
        public int Capacity { get; set; }
        public decimal HourlyRate { get; set; }
    }
}
