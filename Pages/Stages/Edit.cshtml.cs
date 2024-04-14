using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Planer1.Models;

namespace Planer1.Pages.Stages
{
    public class EditModel : PurposeNamePageModel
    {
        private readonly Planer1.Models.PlaneralContext _context;

        public EditModel(Planer1.Models.PlaneralContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Stage Stage { get; set; } = default!;

        /*public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Stages == null)
            {
                return NotFound();
            }

            var stage =  await _context.Stages.FirstOrDefaultAsync(m => m.IdStage == id);
            if (stage == null)
            {
                return NotFound();
            }
            Stage = stage;
           ViewData["IdPurpose"] = new SelectList(_context.Purposes, "IdPurpose", "IdPurpose");
            return Page();
        }*/

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Stage = await _context.Stages
                .Include(c => c.IdPurposeNavigation).FirstOrDefaultAsync(m => m.IdStage == id);
            //var stage =  await _context.Stages.FirstOrDefaultAsync(m => m.IdStage == id);

            if (Stage == null)
            {
                return NotFound();
            }
            PopulatePurposesDropDownList(_context, Stage.IdPurpose);

            //Stage = stage;
            //ViewData["IdPurpose"] = new SelectList(_context.Purposes, "IdPurpose", "IdPurpose");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        /*public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Stage).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StageExists(Stage.IdStage))
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
            var StageToUpdate = await _context.Stages.FindAsync(id);

            if (StageToUpdate == null)
            {
                return NotFound();
            }

            if (await TryUpdateModelAsync<Stage>(
                 StageToUpdate,
                 "Stage",   // Prefix for form value.
                  p => p.IdPurpose, p => p.Name, p => p.Reminder, p => p.Data, p => p.Description))
            {
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            // Select DepartmentID if TryUpdateModelAsync fails.
            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
            PopulatePurposesDropDownList(_context, StageToUpdate.IdPurpose);
            return Page();

        }

        private bool StageExists(int id)
        {
          return (_context.Stages?.Any(e => e.IdStage == id)).GetValueOrDefault();
        }
    }
}
