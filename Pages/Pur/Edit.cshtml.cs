using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Planer1.Models;

namespace Planer1.Pages.Pur
{
    public class EditModel : SectorNamePageModel
    {
        private readonly Planer1.Models.PlaneralContext _context;

        public EditModel(Planer1.Models.PlaneralContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Purpose Purpose { get; set; } = default!;

        /*public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Purposes == null)
            {
                return NotFound();
            }

            var purpose =  await _context.Purposes.FirstOrDefaultAsync(m => m.IdPurpose == id);
            if (purpose == null)
            {
                return NotFound();
            }
            Purpose = purpose;
           ViewData["IdSector"] = new SelectList(_context.Circleoflives, "IdSector", "IdSector");
            return Page();
        }*/

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Purpose = await _context.Purposes
                .Include(c => c.IdSectorNavigation).FirstOrDefaultAsync(m => m.IdPurpose == id);

            if (Purpose == null)
            {
                return NotFound();
            }

            // Select current IdSector.
            PopulateCirclesDropDownList(_context, Purpose.IdSector);
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        /*public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                _context.Attach(Purpose).State = EntityState.Modified;
                //return Page();
            }

            _context.Attach(Purpose).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PurposeExists(Purpose.IdPurpose))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }*/
        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var PurposeToUpdate = await _context.Purposes.FindAsync(id);

            if (PurposeToUpdate == null)
            {
                return NotFound();
            }

            if (await TryUpdateModelAsync<Purpose>(
                 PurposeToUpdate,
                 "Purpose",   // Prefix for form value.
                   p => p.IdSector, p => p.Name, p => p.Status, p => p.Description))
            {
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            // Select DepartmentID if TryUpdateModelAsync fails.
            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
            PopulateCirclesDropDownList(_context, PurposeToUpdate.IdSector);
            return Page();
        }
        private bool PurposeExists(int id)
        {
          return (_context.Purposes?.Any(e => e.IdPurpose == id)).GetValueOrDefault();
        }
    }
}
