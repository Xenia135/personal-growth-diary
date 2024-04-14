using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Planer1.Models;
using Planer1.Pages.Remin;

namespace Planer1.Pages.Trekers
{
    public class EditModel : PageModel
    {
        private readonly Planer1.Models.PlaneralContext _context;

        public EditModel(Planer1.Models.PlaneralContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Treker Treker { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? idt, int? ids)
        {
            if (idt == null || ids == null || _context.Trekers == null)
            {
                return NotFound();
            }
            //Treker = await _context.Trekers
                //.Include(c => c.IdStageNavigation).FirstOrDefaultAsync(m => (m.IdTreker == idt) && (m.IdStage == ids));
            var treker = await _context.Trekers.FirstOrDefaultAsync(m => (m.IdTreker == idt)&&(m.IdStage == ids));
            if (treker == null)
            {
                return NotFound();
            }
            //PopulateStagesDropDownList(_context, Treker.IdStage);
            Treker = treker;
            ViewData["IdStage"] = new SelectList(_context.Stages, "IdStage", "IdStage");
            return Page();
        }


        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                _context.Attach(Treker).State = EntityState.Modified;
                //return Page();
            }

            _context.Attach(Treker).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TrekerExists(Treker.IdTreker))
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
        
        private bool TrekerExists(int id)
        {
          return (_context.Trekers?.Any(e => e.IdTreker == id)).GetValueOrDefault();
        }
    }
}
