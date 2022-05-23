using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DataLibrary;
using Faellesskabet_Web.Data;
using Microsoft.AspNetCore.Authorization;

namespace Faellesskabet_Web.Pages.Nyheder
{
    [Authorize(Roles = "NyhedsManager")]
    public class DeleteModel : PageModel
    {
        private readonly Faellesskabet_Web.Data.ApplicationDbContext _context;

        public DeleteModel(Faellesskabet_Web.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Nyhed Nyhed { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Nyhed = await _context.Nyheder.FirstOrDefaultAsync(m => m.Id == id);

            if (Nyhed == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Nyhed = await _context.Nyheder.FindAsync(id);

            if (Nyhed != null)
            {
                _context.Nyheder.Remove(Nyhed);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
