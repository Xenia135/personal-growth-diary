using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Planer1.Models;

namespace Planer1.Pages.Remin
{
    public class StageNamePageModel : PageModel
    {
        public SelectList StageNameSL { get; set; }

        public void PopulateStagesDropDownList(PlaneralContext _context,
            object selectedStage = null)
        {
            var StagesQuery = from m in _context.Stages
                              orderby m.Name // Sort by name.
                              select m;

            StageNameSL = new SelectList(StagesQuery.AsNoTracking(),
                nameof(Stage.IdStage),
                nameof(Stage.Name),
                selectedStage);
        }
    }
}
