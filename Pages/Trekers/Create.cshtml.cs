using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Planer1.Models;

namespace Planer1.Pages.Trekers
{
    public class CreateModel : PageModel
    {
        private readonly Planer1.Models.PlaneralContext _context;

        public CreateModel(Planer1.Models.PlaneralContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["IdStage"] = new SelectList(_context.Stages, "IdStage", "IdStage");
            return Page();
        }

        [BindProperty]
        public Treker Treker { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Trekers == null || Treker == null)
            {
                return Page();
            }

            _context.Trekers.Add(Treker);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
