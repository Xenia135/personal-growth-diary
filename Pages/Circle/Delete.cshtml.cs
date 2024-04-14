using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Planer1.Models;

namespace Planer1.Pages.Circle
{
    public class DeleteModel : PageModel
    {
        private readonly Planer1.Models.PlaneralContext _context;

        public DeleteModel(Planer1.Models.PlaneralContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Circleoflife Circleoflife { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Circleoflives == null)
            {
                return NotFound();
            }

            var circleoflife = await _context.Circleoflives.FirstOrDefaultAsync(m => m.IdSector == id);

            if (circleoflife == null)
            {
                return NotFound();
            }
            else 
            {
                Circleoflife = circleoflife;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Circleoflives == null)
            {
                return NotFound();
            }
            var circleoflife = await _context.Circleoflives.FindAsync(id);

            if (circleoflife != null)
            {
                Circleoflife = circleoflife;
                _context.Circleoflives.Remove(Circleoflife);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
