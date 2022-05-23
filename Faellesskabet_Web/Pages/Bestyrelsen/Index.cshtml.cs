using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DataLibrary;
using Faellesskabet_Web.Data;

namespace Faellesskabet_Web.Pages.Bestyrelsen
{
    public class IndexModel : PageModel
    {
        private readonly Faellesskabet_Web.Data.ApplicationDbContext _context;

        public IndexModel(Faellesskabet_Web.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Bestyrelse> Bestyrelse { get;set; }

        public async Task OnGetAsync()
        {
            Bestyrelse = await _context.Bestyrelsen.OrderBy(o => o.Order).ToListAsync();
        }
    }
}
