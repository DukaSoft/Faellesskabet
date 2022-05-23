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
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace Faellesskabet_Web.Pages.Arrangementer
{
    [Authorize(Roles = "EventManager")]
    public class CreateModel : PageModel
    {
        private readonly Faellesskabet_Web.Data.ApplicationDbContext _context;

        public CreateModel(Faellesskabet_Web.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
			Arrangement = new Arrangement
			{
				Date =
				new DateTime(
					DateTime.Now.Year,
					DateTime.Now.Month,
					DateTime.Now.Day,
					DateTime.Now.Hour,
					DateTime.Now.Minute,
					0),

				Active = true
			};

			return Page();
        }

        [BindProperty]
        public Arrangement Arrangement { get; set; }

		[BindProperty]
		public IFormFile file { get; set; }

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

			_context.Arrangementer.Add(Arrangement);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
