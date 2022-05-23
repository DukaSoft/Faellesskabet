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

namespace Faellesskabet_Web.Pages.Bestyrelsen.Settings
{
    [Authorize(Roles = "BestyrelsesManager")]
    public class DeleteModel : PageModel
    {
        private readonly Faellesskabet_Web.Data.ApplicationDbContext _context;

        public DeleteModel(Faellesskabet_Web.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Bestyrelse Bestyrelse { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Bestyrelse = await _context.Bestyrelsen.FirstOrDefaultAsync(m => m.Id == id);

            if (Bestyrelse == null)
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

            Bestyrelse = await _context.Bestyrelsen.FindAsync(id);

            if (Bestyrelse != null)
            {
                _context.Bestyrelsen.Remove(Bestyrelse);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
