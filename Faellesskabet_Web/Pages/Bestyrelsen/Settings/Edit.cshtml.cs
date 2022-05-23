using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DataLibrary;
using Faellesskabet_Web.Data;
using Microsoft.AspNetCore.Authorization;

namespace Faellesskabet_Web.Pages.Bestyrelsen.Settings
{
    [Authorize(Roles = "BestyrelsesManager")]
    public class EditModel : PageModel
    {
        private readonly Faellesskabet_Web.Data.ApplicationDbContext _context;

        public EditModel(Faellesskabet_Web.Data.ApplicationDbContext context)
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

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            string mt = Request.Form["MemberType"];
            Bestyrelse.MemberType = mt;

            _context.Attach(Bestyrelse).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BestyrelseExists(Bestyrelse.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool BestyrelseExists(int id)
        {
            return _context.Bestyrelsen.Any(e => e.Id == id);
        }
    }
}
