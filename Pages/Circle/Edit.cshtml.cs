using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Planer1.Models;

namespace Planer1.Pages.Circle
{
    public class EditModel : UserNamePageModel
    {
        private readonly Planer1.Models.PlaneralContext _context;

        public EditModel(Planer1.Models.PlaneralContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Circleoflife Circleoflife { get; set; } = default!;

        /*public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Circleoflives == null)
            {
                return NotFound();
            }

            var circleoflife =  await _context.Circleoflives.FirstOrDefaultAsync(m => m.IdSector == id);
            if (circleoflife == null)
            {
                return NotFound();
            }
            Circleoflife = circleoflife;
           ViewData["IdUsers"] = new SelectList(_context.Users, "IdUsers", "IdUsers");
            return Page();
        }*/

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Circleoflife =  await _context.Circleoflives
                .Include(c => c.IdUsersNavigation).FirstOrDefaultAsync(m => m.IdSector == id);

            if (Circleoflife == null)
            {
                return NotFound();
            }
            //Circleoflife = Circleoflife;
            //ViewData["IdUsers"] = new SelectList(_context.Users, "IdUsers", "IdUsers");
            PopulateUsersDropDownList(_context, Circleoflife.IdUsers);
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (!ModelState.IsValid)
            {
                _context.Attach(Circleoflife).State = EntityState.Modified;
                //return Page();
            }

            _context.Attach(Circleoflife).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CircleoflifeExists(Circleoflife.IdSector))
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

        /*public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var CircleToUpdate = await _context.Circleoflives.FindAsync(id);

            if (CircleToUpdate == null)
            {
                return NotFound();
            }

            if (await TryUpdateModelAsync<Circleoflife>(
                 CircleToUpdate,
                 "Circle",   // Prefix for form value.
                   p => p.IdUsers, p => p.Namesector, p => p.Fullness))
            {
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            // Select DepartmentID if TryUpdateModelAsync fails.
            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
            PopulateUsersDropDownList(_context, CircleToUpdate.IdUsers);
            return Page();
        }*/

        private bool CircleoflifeExists(int id)
        {
          return (_context.Circleoflives?.Any(e => e.IdSector == id)).GetValueOrDefault();
        }
    }
}
