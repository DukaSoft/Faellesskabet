using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DataLibrary;
using Faellesskabet_Web.Data;

namespace Faellesskabet_Web.Pages.Arrangementer
{
    public class OldModel : PageModel
    {
        private readonly Faellesskabet_Web.Data.ApplicationDbContext _context;

        public OldModel(Faellesskabet_Web.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Arrangement> Arrangement { get;set; }

        [BindProperty(SupportsGet = true)]
        public int CurrentPage { get; set; } = 1;
        public int Count { get; set; }
        public int PageSize { get; set; } = 100;

        public int TotalPages => (int)Math.Ceiling(decimal.Divide(Count, PageSize));

        public List<Arrangement> Data { get; set; }

        public async Task OnGetAsync()
        {
            Arrangement = await _context.Arrangementer.Where(d=> d.Date < DateTime.Now).ToListAsync();
            Data = GetPaginatedResult(CurrentPage, PageSize);
            Count = Arrangement.Count;
        }

        public List<Arrangement> GetPaginatedResult(int currentPage, int pageSize = 10)
        {
            var data = Arrangement;
            return data.OrderByDescending(d => d.Date).Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
        }
    }
}
