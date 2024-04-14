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
    public class IndexModel : PageModel
    {
        private readonly Planer1.Models.PlaneralContext _context;

        public IndexModel(Planer1.Models.PlaneralContext context)
        {
            _context = context;
        }

        public IList<Reminder> Reminder { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Reminders != null)
            {
                Reminder = await _context.Reminders
                .Include(r => r.IdStageNavigation).ToListAsync();
            }
        }
    }
}
