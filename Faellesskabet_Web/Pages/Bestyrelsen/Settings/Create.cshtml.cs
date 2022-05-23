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

namespace Faellesskabet_Web.Pages.Bestyrelsen.Settings
{
	[Authorize(Roles = "BestyrelsesManager")]
	public class CreateModel : PageModel
	{
		private readonly Faellesskabet_Web.Data.ApplicationDbContext _context;

		public CreateModel(Faellesskabet_Web.Data.ApplicationDbContext context)
		{
			_context = context;
		}

		public IActionResult OnGet()
		{
			count = _context.Bestyrelsen.Count();
			return Page();
		}

		[BindProperty]
		public Bestyrelse Bestyrelse { get; set; }

		public int count { get; set; }

		public async Task<IActionResult> OnPostAsync()
		{
			if (!ModelState.IsValid)
			{
				return Page();
			}

			_context.Bestyrelsen.Add(Bestyrelse);
			await _context.SaveChangesAsync();

			return RedirectToPage("./Index");
		}
	}
}
