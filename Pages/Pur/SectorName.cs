using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Planer1.Models;

namespace Planer1.Pages.Pur
{
    public class SectorNamePageModel : PageModel
    {
        public SelectList CircleoflifeNameSL { get; set; }

        public void PopulateCirclesDropDownList(PlaneralContext _context,
            object selectedCircle = null)
        {
            var CirclesQuery = from m in _context.Circleoflives
                               orderby m.Namesector // Sort by name.
                               select m;

            CircleoflifeNameSL = new SelectList(CirclesQuery.AsNoTracking(),
                nameof(Circleoflife.IdSector),
                nameof(Circleoflife.Namesector),
                selectedCircle);
        }
    }
}
