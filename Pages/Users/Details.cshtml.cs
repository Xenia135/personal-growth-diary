using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Planer1.Models;

namespace Planer1.Pages.Users
{
    public class DetailsModel : PageModel
    {
        private readonly Planer1.Models.PlaneralContext _context;

        public DetailsModel(Planer1.Models.PlaneralContext context)
        {
            _context = context;
        }

      public Useal Useal { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var useal = await _context.Users.FirstOrDefaultAsync(m => m.IdUsers == id);
            if (useal == null)
            {
                return NotFound();
            }
            else 
            {
                Useal = useal;
            }
            return Page();
        }
    }
}
