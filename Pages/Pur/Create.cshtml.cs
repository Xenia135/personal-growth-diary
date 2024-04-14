using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Planer1.Models;

namespace Planer1.Pages.Pur
{
    public class CreateModel : SectorNamePageModel
    {
        private readonly Planer1.Models.PlaneralContext _context;

        public CreateModel(Planer1.Models.PlaneralContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            //ViewData["IdSector"] = new SelectList(_context.Circleoflives, "IdSector", "IdSector");
            PopulateCirclesDropDownList(_context);
            return Page();
        }

        [BindProperty]
        public Purpose Purpose { get; set; } = default!;


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        /*public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Purposes == null || Purpose == null)
            {
                _context.Purposes.Add(Purpose);
                await _context.SaveChangesAsync();
                //return Page();
            }

            //_context.Purposes.Add(Purpose);
            //await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }*/
        public async Task<IActionResult> OnPostAsync()
        {
            var emptyPurposes = new Purpose();

            if (await TryUpdateModelAsync<Purpose>(
                emptyPurposes,
                "Purpose",   // Prefix for form value.
                s => s.IdSector, s => s.Name, s => s.Description))
            {
                _context.Purposes.Add(emptyPurposes);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");

            }
            _context.Purposes.Add(emptyPurposes);
            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
            /*_context.Purposes.Add(emptyPurposes);
            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");*/
            PopulateCirclesDropDownList(_context, emptyPurposes.IdSector);
            return Page();
        }
    }
}
