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

namespace Faellesskabet_Web.Pages.Nyheder
{
    [Authorize(Roles = "NyhedsManager")]
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public EditModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Nyhed Nyhed { get; set; }

        [BindProperty]
        public IFormFile file { get; set; }

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

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
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
                        Nyhed.ImageContent = target.ToArray();
                    }
                }
                else
                {
                    ModelState.AddModelError("CustomError", "File too big");
                }
            }

            _context.Attach(Nyhed).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NyhedExists(Nyhed.Id))
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

        private bool NyhedExists(int id)
        {
            return _context.Nyheder.Any(e => e.Id == id);
        }

        public async Task<IActionResult> OnPostRemoveImageAsync()
        {
            Nyhed.ImageContent = null;
            _context.Attach(Nyhed).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return LocalRedirect($"/Nyheder/Edit?id={Nyhed.Id}");
        }
    }
}
