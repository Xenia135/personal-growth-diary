using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Planer1.Models;

namespace Planer1.Pages.Circle
{
    public class CreateModel : UserNamePageModel
    {
        private readonly Planer1.Models.PlaneralContext _context;

        public CreateModel(Planer1.Models.PlaneralContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            PopulateUsersDropDownList(_context);
            //ViewData["IdUsers"] = new SelectList(_context.Users, "IdUsers", "IdUsers");
            return Page();
        }

        [BindProperty]
        public Circleoflife Circleoflife { get; set; } = default!;


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Circleoflives == null || Circleoflife == null)
            {
                _context.Circleoflives.Add(Circleoflife);
                await _context.SaveChangesAsync();
                //return Page();
            }
            return RedirectToPage("./Index");
        }
        /*public async Task<IActionResult> OnPostAsync()
        {
            var emptyCircle = new Circleoflife();

            if (await TryUpdateModelAsync<Circleoflife>(
                emptyCircle,
                "Circle",   // Prefix for form value.
                s => s.IdUsers, s => s.Namesector, s => s.Fullness))
            {
                _context.Circleoflives.Add(emptyCircle);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");

            }
            _context.Circleoflives.Add(emptyCircle);
            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
            PopulateUsersDropDownList(_context, emptyCircle.IdUsers);
            return Page();
        }*/

    }
}
