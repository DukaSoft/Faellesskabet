using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DataLibrary;
using Faellesskabet_Web.Data;
using System.Text.RegularExpressions;

namespace Faellesskabet_Web.Pages.Arrangementer
{
	public class DetailsModel : PageModel
	{
		private readonly Faellesskabet_Web.Data.ApplicationDbContext _context;

		public DetailsModel(Faellesskabet_Web.Data.ApplicationDbContext context)
		{
			_context = context;
		}

		public Arrangement Arrangement { get; set; }

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

			Arrangement.Content = LinkHelper.Convert(Arrangement.Content);

			return Page();
		}
	}
}
