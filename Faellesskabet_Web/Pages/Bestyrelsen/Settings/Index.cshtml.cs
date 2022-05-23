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

namespace Faellesskabet_Web.Pages.Bestyrelsen.Settings
{
	[Authorize(Roles = "BestyrelsesManager")]
	public class IndexModel : PageModel
	{
		private readonly Faellesskabet_Web.Data.ApplicationDbContext _context;

		public IndexModel(Faellesskabet_Web.Data.ApplicationDbContext context)
		{
			_context = context;
		}

		[BindProperty]
		public IList<Bestyrelse> Bestyrelse { get; set; }

		public async Task OnGetAsync()
		{
			Bestyrelse = await _context.Bestyrelsen.OrderBy(o => o.Order).ToListAsync();
			int nextnumber = 1;
			bool changes = false;
			for (int i = 0; i < Bestyrelse.Count; i++)
			{
				if(Bestyrelse[i].Order != nextnumber)
				{
					Bestyrelse[i].Order = nextnumber;
					changes = true;
				}
				nextnumber++;
			}
			if(changes == true)
			{
				await _context.SaveChangesAsync();
			}

		}

		public async Task OnPost()
		{
			Bestyrelse = await _context.Bestyrelsen.OrderBy(o => o.Order).ToListAsync();
			string Id = Request.Form["Id"];
			string UpDown = Request.Form["UpDown"];
			int idAsInt = int.Parse(Id);
			
			for (int i = 0; i < Bestyrelse.Count; i++)
			{
				if(Bestyrelse[i].Id == idAsInt)
				{
					idAsInt = i;
					break;
				}
			}

			if (UpDown == "Up")
			{
				if (Bestyrelse[idAsInt].Order > 1)
				{

					for (int i = 0; i < Bestyrelse.Count; i++)
					{
						if (Bestyrelse[i].Order == Bestyrelse[idAsInt].Order - 1)
						{
							Bestyrelse[i].Order = Bestyrelse[idAsInt].Order;
							Bestyrelse[idAsInt].Order -= 1;
							break;
						}
					}

				}
			}
			else if (UpDown == "Down")
			{
				if (Bestyrelse[idAsInt].Order < Bestyrelse.Count)
				{
					for (int i = 0; i < Bestyrelse.Count; i++)
					{
						if (Bestyrelse[i].Order == Bestyrelse[idAsInt].Order + 1)
						{
							Bestyrelse[i].Order = Bestyrelse[idAsInt].Order;
							Bestyrelse[idAsInt].Order += 1;
							break;
						}
					}
				}
			}


		await _context.SaveChangesAsync();

		// Refresh the list
		Bestyrelse = await _context.Bestyrelsen.OrderBy(o => o.Order).ToListAsync();
	}
}
}
