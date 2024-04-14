using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Planer1.Models;

namespace Planer1.Pages.Trekers
{
    public class IndexModel : PageModel
    {
        private readonly Planer1.Models.PlaneralContext _context;

        public IndexModel(Planer1.Models.PlaneralContext context)
        {
            _context = context;
        }
        public string NameSort { get; set; }
        public string DateSort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }
        public IList<Treker> Trekers { get;set; } = default!;

        /*public async Task OnGetAsync()
        {
            if (_context.Trekers != null)
            {
                Treker = await _context.Trekers
                .Include(t => t.IdStageNavigation).ToListAsync();
            }
        }*/
        public async Task OnGetAsync(string sortOrder, string searchString)
        {
            NameSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            DateSort = sortOrder == "Date" ? "date_desc" : "Date";

            CurrentFilter = searchString;

            IQueryable<Treker> trekersIQ = from s in _context.Trekers
                                           select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                trekersIQ = trekersIQ.Where(s => s.IdStageNavigation.Name.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    trekersIQ = trekersIQ.OrderByDescending(s => s.IdTreker);
                    break;
                case "Date":
                    trekersIQ = trekersIQ.OrderBy(s => s.Day);
                    break;
               case "date_desc":
                    trekersIQ = trekersIQ.OrderByDescending(s => s.Day);
                    break;
                default:
                    trekersIQ = trekersIQ.OrderBy(s => s.IdTreker);
                    break;
            }
            Trekers = await trekersIQ.AsNoTracking().Include(t => t.IdStageNavigation).ToListAsync();
            //Stages = await studentsIQ.AsNoTracking().Include(p => p.IdPurposeNavigation).ToListAsync();

        }
    }
}
