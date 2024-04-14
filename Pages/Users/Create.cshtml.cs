using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Planer1.Models;

namespace Planer1.Pages.Users
{
    public class CreateModel : PageModel
    {
        private readonly Planer1.Models.PlaneralContext _context;

        public CreateModel(Planer1.Models.PlaneralContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Useal Useal { get; set; } = default!;

        public class HashPasswordClass
        {
            public static string HashPassword(string password)
            {
                using (var md5 = SHA1.Create())
                {
                    var hashedBytes = md5.ComputeHash(Encoding.UTF8.GetBytes(password));
                    var hash = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
                    return hash;
                }
            }
        }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            Useal.Password = HashPasswordClass.HashPassword(Useal.Password);
            if (!ModelState.IsValid || _context.Users == null || Useal == null)
             {
                return Page();
             }

            _context.Users.Add(Useal);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

    }
}
