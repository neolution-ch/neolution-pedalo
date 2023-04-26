using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PedaloWebApp.Core.Interfaces.Data;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System;
using System.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace PedaloWebApp.Pages.Pedaloes
{
    public class PdfModel : PageModel
    {
        private readonly IDbContextFactory contextFactory;

        public PdfModel(IDbContextFactory contextFactory)
        {
            this.contextFactory = contextFactory;
        }

        public IActionResult OnGet(Guid id)
        {
            using var context = this.contextFactory.CreateReadOnlyContext();
            var pedalo = context.Pedaloes.Single(x => x.PedaloId == id);

            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.DefaultTextStyle(x => x.FontFamily("Arial"));
                    page.MarginTop(30);
                    page.MarginBottom(50);
                    page.MarginHorizontal(40);
                    page.Header()
                            .Text($"Details for: {pedalo.Name}")
                            .SemiBold().FontSize(36).FontColor(Colors.Blue.Medium); 
                    page.Content()
                            .Text(text =>
                            {
                                text.Line($"Capacity: {pedalo.Capacity}").SemiBold();
                                text.Line($"Color: {pedalo.Color}");
                            });
                });
            });

            return this.File(document.GeneratePdf(), "application/pdf");
        }
    }
}
