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
    public class DetailsModel : PageModel
    {
        private readonly Planer1.Models.PlaneralContext _context;

        public DetailsModel(Planer1.Models.PlaneralContext context)
        {
            _context = context;
        }

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
    }
}
