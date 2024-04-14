using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Planer1.Models;

namespace Planer1.Pages.Stages
{
    public class DeleteModel : PageModel
    {
        private readonly Planer1.Models.PlaneralContext _context;

        public DeleteModel(Planer1.Models.PlaneralContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Stage Stage { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Stages == null)
            {
                return NotFound();
            }

            var stage = await _context.Stages.FirstOrDefaultAsync(m => m.IdStage == id);

            if (stage == null)
            {
                return NotFound();
            }
            else 
            {
                Stage = stage;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Stages == null)
            {
                return NotFound();
            }
            var stage = await _context.Stages.FindAsync(id);

            if (stage != null)
            {
                Stage = stage;
                _context.Stages.Remove(Stage);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
