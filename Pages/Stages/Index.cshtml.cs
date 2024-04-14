using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Planer1.Models;

namespace Planer1.Pages.Stages
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

        public IList<Stage> Stages { get;set; } = default!;

        /*public async Task OnGetAsync()
        {
            if (_context.Stages != null)
            {
                Stage = await _context.Stages
                .Include(s => s.IdPurposeNavigation).ToListAsync();
            }
        }*/
        public async Task OnGetAsync(string sortOrder, string searchString)
        {
            NameSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            DateSort = sortOrder == "Date" ? "date_desc" : "Date";

            CurrentFilter = searchString;

            IQueryable<Stage> studentsIQ = from s in _context.Stages
                                           select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                studentsIQ = studentsIQ.Where(s => s.Name.Contains(searchString)
                                   || s.IdPurposeNavigation.Name.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    studentsIQ = studentsIQ.OrderByDescending(s => s.Name);
                    break;
                case "Date":
                    studentsIQ = studentsIQ.OrderBy(s => s.Data);
                    break;
                case "date_desc":
                    studentsIQ = studentsIQ.OrderByDescending(s => s.Data);
                    break;
                default:
                    studentsIQ = studentsIQ.OrderBy(s => s.Name);
                    break;
            }
            Stages = await studentsIQ.AsNoTracking().Include(p => p.IdPurposeNavigation).ToListAsync();

        }
    }
}
