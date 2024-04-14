using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Planer1.Models;

namespace Planer1.Pages.Trekers
{
    public class DetailsModel : PageModel
    {
        private readonly Planer1.Models.PlaneralContext _context;

        public DetailsModel(Planer1.Models.PlaneralContext context)
        {
            _context = context;
        }

      public Treker Treker { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Trekers == null)
            {
                return NotFound();
            }

            var treker = await _context.Trekers.FirstOrDefaultAsync(m => m.IdTreker == id);
            if (treker == null)
            {
                return NotFound();
            }
            else 
            {
                Treker = treker;
            }
            return Page();
        }
    }
}
