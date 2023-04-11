namespace PedaloWebApp.Pages.Pedaloes
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
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
        public PedaloDeleteModel Pedalo { get; set; }

        public IActionResult OnGet(Guid? id)
        {
            if (id == null)
            {
                return this.BadRequest();
            }

            using var context = this.contextFactory.CreateReadOnlyContext();
            this.Pedalo = context.Pedaloes
                .Where(m => m.PedaloId == id)
                .Select(x => new PedaloDeleteModel
                {
                    PedaloId = x.PedaloId,
                    Name = x.Name,
                    Color = x.Color,
                    Capacity = x.Capacity,
                    HourlyRate = x.HourlyRate,
                    NumberOfBookings = x.Bookings.Count,
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
                context.Pedaloes.Remove(pedalo);

                context.SaveChanges();
            }
            catch (Exception)
            {
                return this.RedirectToPage("/Error");
            }

            return this.RedirectToPage("./Index");
        }
    }

    public class PedaloDeleteModel
    {
        public Guid PedaloId { get; set; }
        public string Name { get; set; }
        public PedaloColor Color { get; set; }
        public int Capacity { get; set; }
        public decimal HourlyRate { get; set; }

        public int? NumberOfBookings { get; set; }
    }
}