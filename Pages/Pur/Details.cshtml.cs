using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Planer1.Models;

namespace Planer1.Pages.Pur
{
    public class DetailsModel : PageModel
    {
        private readonly Planer1.Models.PlaneralContext _context;

        public DetailsModel(Planer1.Models.PlaneralContext context)
        {
            _context = context;
        }

      public Purpose Purpose { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Purposes == null)
            {
                return NotFound();
            }

            var purpose = await _context.Purposes.FirstOrDefaultAsync(m => m.IdPurpose == id);
            if (purpose == null)
            {
                return NotFound();
            }
            else 
            {
                Purpose = purpose;
            }
            return Page();
        }
    }
}
