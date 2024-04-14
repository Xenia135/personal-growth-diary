using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Planer1.Models;

namespace Planer1.Pages.Remin
{
    public class EditModel : StageNamePageModel
    {
        private readonly Planer1.Models.PlaneralContext _context;

        public EditModel(Planer1.Models.PlaneralContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Reminder Reminder { get; set; } = default!;

        /*public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Reminders == null)
            {
                return NotFound();
            }

            var reminder =  await _context.Reminders.FirstOrDefaultAsync(m => m.IdReminder == id);
            if (reminder == null)
            {
                return NotFound();
            }
            Reminder = reminder;
           ViewData["IdStage"] = new SelectList(_context.Stages, "IdStage", "IdStage");
            return Page();
        }*/

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Reminder = await _context.Reminders
                .Include(c => c.IdStageNavigation).FirstOrDefaultAsync(m => m.IdReminder == id);

            //var reminder =  await _context.Reminders.FirstOrDefaultAsync(m => m.IdReminder == id);
            if (Reminder == null)
            {
                return NotFound();
            }
            PopulateStagesDropDownList(_context, Reminder.IdStage);
            //Reminder = Reminder;
            //ViewData["IdStage"] = new SelectList(_context.Stages, "IdStage", "IdStage");
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

            _context.Attach(Reminder).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReminderExists(Reminder.IdReminder))
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

            var ReminderToUpdate = await _context.Reminders.FindAsync(id);

            if (ReminderToUpdate == null)
            {
                return NotFound();
            }

            if (await TryUpdateModelAsync<Reminder>(
                 ReminderToUpdate,
                 "Reminder",   // Prefix for form value.
                   p => p.IdStage, p => p.Day, p => p.Time))
            {
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            // Select DepartmentID if TryUpdateModelAsync fails.
            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
            PopulateStagesDropDownList(_context, ReminderToUpdate.IdStage);
            return Page();
        }

        private bool ReminderExists(int id)
        {
          return (_context.Reminders?.Any(e => e.IdReminder == id)).GetValueOrDefault();
        }
    }
}
