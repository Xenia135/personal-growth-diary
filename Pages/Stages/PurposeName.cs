using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Planer1.Models;

namespace Planer1.Pages.Stages
{
    public class PurposeNamePageModel : PageModel
    {
        public SelectList PurposeNameSL { get; set; }

        public void PopulatePurposesDropDownList(PlaneralContext _context,
            object selectedPurpose = null)
        {
            var PurposesQuery = from m in _context.Purposes
                                orderby m.Name // Sort by name.
                                select m;

            PurposeNameSL = new SelectList(PurposesQuery.AsNoTracking(),
                nameof(Purpose.IdPurpose),
                nameof(Purpose.Name),
                selectedPurpose);
        }
    }
}
