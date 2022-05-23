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
    public class IndexModel : PageModel
    {
        private readonly Faellesskabet_Web.Data.ApplicationDbContext _context;

        public IndexModel(Faellesskabet_Web.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Arrangement> Arrangement { get;set; }

        public async Task OnGetAsync()
        {
            // Show all events to Admins and EventManagers
            // And hide Inactive events for normal users
            if (User.IsInRole("Admin") || User.IsInRole("EventManager"))
			{
                Arrangement = await _context.Arrangementer
                    .OrderBy(d => d.Date).Where(d => d.Date.AddHours(2) > DateTime.Now).ToListAsync();
            }
			else
			{
                Arrangement = await _context.Arrangementer
                    .OrderBy(d => d.Date).Where(d => d.Date.AddHours(2) > DateTime.Now).Where(a => a.Active == true).ToListAsync();
			}

            foreach(var arr in Arrangement)
			{
                arr.Content = LinkHelper.ReplaceWithText(arr.Content, "Link");
			
            }
        }
    }
}
