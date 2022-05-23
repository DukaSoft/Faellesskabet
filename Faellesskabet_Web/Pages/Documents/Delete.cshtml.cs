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

namespace Faellesskabet_Web.Pages.Documents
{
    [Authorize(Roles = "ReferatsManager,RegnskabsManager")]
    public class DeleteModel : PageModel
    {
        private readonly Faellesskabet_Web.Data.ApplicationDbContext _context;

        public DeleteModel(Faellesskabet_Web.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Dokument Document { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Document = await _context.Dokumenter.FirstOrDefaultAsync(m => m.Id == id);

            if (Document == null)
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

            Document = await _context.Dokumenter.FindAsync(id);

            if (Document != null)
            {
                _context.Dokumenter.Remove(Document);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
