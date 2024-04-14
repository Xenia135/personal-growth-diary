using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Planer1.Models;

namespace Planer1.Pages.Circle
{
    public class UserNamePageModel : PageModel
    {
        public SelectList UserNameSL { get; set; }

        public void PopulateUsersDropDownList(PlaneralContext _context,
            object selectedUser = null)
        {
            var CirclesQuery = from m in _context.Users
                               orderby m.Name // Sort by name.
                               select m;

            UserNameSL = new SelectList(CirclesQuery.AsNoTracking(),
                nameof(Useal.IdUsers),
                nameof(Useal.Name),
                selectedUser);
        }
    }
}
