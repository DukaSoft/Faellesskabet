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

namespace Faellesskabet_Web.Pages.Nyheder
{
	[Authorize(Roles = "NyhedsManager")]
	public class CreateModel : PageModel
	{
		private readonly ApplicationDbContext _context;

		public CreateModel(ApplicationDbContext context)
		{
			_context = context;
		}

		public IActionResult OnGet()
		{
			Nyhed = new Nyhed
			{
				PublishDate =
					new DateTime(
						DateTime.Now.Year,
						DateTime.Now.Month,
						DateTime.Now.Day,
						DateTime.Now.Hour,
						DateTime.Now.Minute,
						0),

				ExpireDate = 
					new DateTime(
						DateTime.Now.Year + 1,
						DateTime.Now.Month,
						DateTime.Now.Day,
						DateTime.Now.Hour,
						DateTime.Now.Minute,
						0),

				Important = false,
				ShowOnLanding = false
			};
			return Page();
		}

		[BindProperty]
		public Nyhed Nyhed { get; set; }

		[BindProperty]
		public IFormFile file { get; set; }

		// To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
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
			//else
			//{
			//	ModelState.AddModelError("CustomError", "File missing");
			//}

			_context.Nyheder.Add(Nyhed);
			await _context.SaveChangesAsync();

			return RedirectToPage("./Index");
		}
	}
}
