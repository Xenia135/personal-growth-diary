using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Planer1.Models;

namespace Planer1.Pages.Stages
{
    public class CreateModel : PurposeNamePageModel
    {
        private readonly Planer1.Models.PlaneralContext _context;

        public CreateModel(Planer1.Models.PlaneralContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            PopulatePurposesDropDownList(_context);
            //ViewData["IdPurpose"] = new SelectList(_context.Purposes, "IdPurpose", "IdPurpose");
            return Page();
        }

        [BindProperty]
        public Stage Stage { get; set; } = default!;


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        /*public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Stages == null || Stage == null)
            {
                return Page();
            }

            _context.Stages.Add(Stage);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }*/
        public async Task<IActionResult> OnPostAsync()
        {
            var emptyStages = new Stage();

            if (await TryUpdateModelAsync<Stage>(
                emptyStages,
                "Stage",   // Prefix for form value.
                s => s.IdPurpose, s => s.Name, s => s.Reminder, s => s.Data, s => s.Description))
            {
                _context.Stages.Add(emptyStages);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");

            }
            _context.Stages.Add(emptyStages);
            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
            /*_context.Purposes.Add(emptyPurposes);
            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");*/
            PopulatePurposesDropDownList(_context, emptyStages.IdPurpose);
            return Page();
        }
    }
}
