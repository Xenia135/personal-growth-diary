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
    public class DetailsModel : PageModel
    {
        private readonly Planer1.Models.PlaneralContext _context;

        public DetailsModel(Planer1.Models.PlaneralContext context)
        {
            _context = context;
        }

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
    }
}
