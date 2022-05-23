using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DataLibrary;
using Faellesskabet_Web.Data;

namespace Faellesskabet_Web.Pages.Nyheder
{
	public class IndexModel : PageModel
	{
		private readonly ApplicationDbContext _context;

		public IndexModel(ApplicationDbContext context)
		{
			_context = context;
		}

		public IList<Nyhed> Nyheder { get; set; }

		public async Task OnGetAsync()
		{
			Nyheder = await _context.Nyheder.Where(ed => ed.ExpireDate > DateTime.Now).OrderByDescending(d => d.PublishDate).ToListAsync();
		}
	}
}
