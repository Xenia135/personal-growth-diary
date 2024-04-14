using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Planer1.Models;

namespace Planer1.Pages.Users
{
    public class EditModel : PageModel
    {
        private readonly Planer1.Models.PlaneralContext _context;

        public EditModel(Planer1.Models.PlaneralContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Useal Useal { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var useal =  await _context.Users.FirstOrDefaultAsync(m => m.IdUsers == id);
            if (useal == null)
            {
                return NotFound();
            }
            Useal = useal;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Useal).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsealExists(Useal.IdUsers))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool UsealExists(int id)
        {
          return (_context.Users?.Any(e => e.IdUsers == id)).GetValueOrDefault();
        }
    }
}
