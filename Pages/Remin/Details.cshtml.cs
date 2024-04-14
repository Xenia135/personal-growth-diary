using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Planer1.Models;

namespace Planer1.Pages.Remin
{
    public class DetailsModel : PageModel
    {
        private readonly Planer1.Models.PlaneralContext _context;

        public DetailsModel(Planer1.Models.PlaneralContext context)
        {
            _context = context;
        }

      public Reminder Reminder { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Reminders == null)
            {
                return NotFound();
            }

            var reminder = await _context.Reminders.FirstOrDefaultAsync(m => m.IdReminder == id);
            if (reminder == null)
            {
                return NotFound();
            }
            else 
            {
                Reminder = reminder;
            }
            return Page();
        }
    }
}
