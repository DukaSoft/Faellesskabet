using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DataLibrary;
using Faellesskabet_Web.Data;

namespace Faellesskabet_Web.Pages.Documents
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Dokument> Document { get;set; }

        public async Task OnGetAsync()
        {
            this.Document = await _context.Dokumenter.OrderByDescending(d => d.Date).ToListAsync();
        }

        public async Task<IActionResult> OnPostDownloadAsync(int? id)
        {
            var myInv = await _context.Dokumenter.FirstOrDefaultAsync(m => m.Id == id);
            if (myInv == null)
            {
                return NotFound();
            }

            if (myInv.File == null)
            {
                return Page();
            }
            else
            {
                byte[] byteArr = myInv.File;
                string mimeType = "application/pdf";
                return new FileContentResult(byteArr, mimeType)
                {
                    FileDownloadName = $"{myInv.Title} {myInv.Date.ToShortDateString()}.pdf"
                };
            }

        }
    }
}
