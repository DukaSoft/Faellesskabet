using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLibrary;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Faellesskabet_Web.Pages
{
	public class IndexModel : PageModel
	{
		private readonly ILogger<IndexModel> _logger;
		private readonly Faellesskabet_Web.Data.ApplicationDbContext _context;

		public IList<Nyhed> Nyheder { get; set; }
		public IList<Arrangement> Arrangements { get; set; }

		public int CacheMinutes { get; set; } = 1;

		public int NumberOfArrangementsToShow { get; set; } = 5;
		public int TotalArrangements { get; set; }

		public IndexModel(ILogger<IndexModel> logger, Faellesskabet_Web.Data.ApplicationDbContext context)
		{
			_logger = logger;
			_context = context;
		}

		public async Task OnGet()
		{
			Nyheder = await _context.Nyheder
				.Where(ed => ed.ExpireDate > DateTime.Now)
				.Where(i => i.ShowOnLanding == true)
				.OrderByDescending(d => d.PublishDate)
				.AsNoTracking()
				.ToListAsync();
			Arrangements = await _context.Arrangementer
				.Where(e => e.Date.AddHours(2) > DateTime.Now)
				.Where(a => a.Active == true)
				.OrderBy(d => d.Date)
				.Take(NumberOfArrangementsToShow)
				.AsNoTracking()
				.ToListAsync();

			foreach(var arr in Arrangements)
			{
				arr.Content = LinkHelper.ReplaceWithText(arr.Content, "Link");
			}

			TotalArrangements = _context.Arrangementer
				.Where(e => e.Date.AddHours(2) > DateTime.Now)
				.Where(a => a.Active == true)
				.AsNoTracking()
				.Count();
		}


	}
}
