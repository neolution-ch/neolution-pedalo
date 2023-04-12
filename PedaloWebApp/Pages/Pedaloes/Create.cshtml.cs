namespace PedaloWebApp.Pages.Pedaloes
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Xml.Linq;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using PedaloWebApp.Core.Domain.Entities;
    using PedaloWebApp.Core.Interfaces.Data;

    public class CreateModel : PageModel
    {
        private readonly IDbContextFactory contextFactory;

        public CreateModel(IDbContextFactory contextFactory)
        {
            this.contextFactory = contextFactory;
        }

        [BindProperty]
        public PedaloCreateModel Pedalo { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (!this.ModelState.IsValid)
            {
                return this.Page();
            }

            using var context = this.contextFactory.CreateContext();

            var pedalo = new Pedalo
            {
                PedaloId = Guid.NewGuid(),
                Name = this.Pedalo.Name,
                Color = this.Pedalo.Color,
                Capacity = this.Pedalo.Capacity,
                HourlyRate = this.Pedalo.HourlyRate,
            };

            try
            {
                context.Pedaloes.Add(pedalo);
                context.SaveChanges();
            }
            catch (Exception)
            {
                return this.RedirectToPage("/Error");
            }

            return this.RedirectToPage("./Index");
        }
    }

    public class PedaloCreateModel
    {

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Color")]
        public PedaloColor Color { get; set; }

        [Display(Name = "Capacity")]
        public int Capacity { get; set; }

        [Display(Name = "Hourly Rate")]
        public decimal HourlyRate { get; set; }
    }
}
