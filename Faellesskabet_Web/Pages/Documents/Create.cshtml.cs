using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using DataLibrary;
using Faellesskabet_Web.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace Faellesskabet_Web.Pages.Documents
{
    [Authorize(Roles = "ReferatsManager,RegnskabsManager")]
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CreateModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Dokument Document { get; set; }

        [BindProperty]
        public IFormFile file { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            const long FileSize = 10000000; // 10mb
            if (file != null)
            {
                if (file.Length > 0 && file.Length < FileSize)
                {
                    using (var target = new MemoryStream())
                    {
                        file.CopyTo(target);
                        Document.File = target.ToArray();
                    }
                }
                else
                {
                    ModelState.AddModelError("CustomError", "Filen er for stor");
                }
            }
            else
            {
                ModelState.AddModelError("CustomError", "Filen mangler");
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Dokumenter.Add(Document);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
