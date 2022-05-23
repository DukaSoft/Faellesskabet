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
using Microsoft.AspNetCore.Http;
using System.IO;

namespace Faellesskabet_Web.Pages.Arrangementer
{
    [Authorize(Roles = "EventManager")]
    public class EditModel : PageModel
    {
        private readonly Faellesskabet_Web.Data.ApplicationDbContext _context;

        public EditModel(Faellesskabet_Web.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Arrangement Arrangement { get; set; }

        [BindProperty]
        public IFormFile file { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Arrangement = await _context.Arrangementer.FirstOrDefaultAsync(m => m.Id == id);

            if (Arrangement == null)
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

            if (file != null)
            {

                if (file.Length > 0 && file.Length < 10000000)
                {
                    using (var target = new MemoryStream())
                    {
                        file.CopyTo(target);
                        Arrangement.ImageContent = target.ToArray();
                    }
                }
                else
                {
                    ModelState.AddModelError("CustomError", "File too big");
                }
            }

            _context.Attach(Arrangement).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArrangementExists(Arrangement.Id))
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

        private bool ArrangementExists(int id)
        {
            return _context.Arrangementer.Any(e => e.Id == id);
        }

        public async Task<IActionResult> OnPostRemoveImageAsync()
        {
            Arrangement.ImageContent = null;
            _context.Attach(Arrangement).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return LocalRedirect($"/Arrangementer/Edit?id={Arrangement.Id}");
        }
    }
}
