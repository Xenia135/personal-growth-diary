using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Planer1.Models;

namespace Planer1.Pages.Remin
{
    public class CreateModel : StageNamePageModel
    {
        private readonly Planer1.Models.PlaneralContext _context;

        public CreateModel(Planer1.Models.PlaneralContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            PopulateStagesDropDownList(_context);
            //ViewData["IdStage"] = new SelectList(_context.Stages, "IdStage", "IdStage");
            return Page();
        }

        [BindProperty]
        public Reminder Reminder { get; set; } = default!;


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        /*public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Reminders == null || Reminder == null)
            {
                return Page();
            }

            _context.Reminders.Add(Reminder);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }*/
        public async Task<IActionResult> OnPostAsync()
        {
            var emptyReminders = new Reminder();

            if (await TryUpdateModelAsync<Reminder>(
                emptyReminders,
                "Reminder",   // Prefix for form value.
                s => s.IdStage, s => s.Day, s => s.Time))
            {
                _context.Reminders.Add(emptyReminders);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");

            }
            _context.Reminders.Add(emptyReminders);
            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
            /*_context.Purposes.Add(emptyPurposes);
            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");*/
            PopulateStagesDropDownList(_context, emptyReminders.IdStage);
            return Page();
        }
    }
}
