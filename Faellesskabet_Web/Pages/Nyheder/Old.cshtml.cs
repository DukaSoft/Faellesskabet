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

namespace Faellesskabet_Web.Pages.Nyheder
{
    [Authorize(Roles = "NyhedsManager")]
    public class OldModel : PageModel
    {
        private readonly Faellesskabet_Web.Data.ApplicationDbContext _context;

        public OldModel(Faellesskabet_Web.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Nyhed> Nyheder { get;set; }

        [BindProperty(SupportsGet = true)]
        public int CurrentPage { get; set; } = 1;
        public int Count { get; set; }
        public int PageSize { get; set; } = 100;

        public int TotalPages => (int)Math.Ceiling(decimal.Divide(Count, PageSize));

        public List<Nyhed> Data { get; set; }

        public async Task OnGetAsync()
        {
            Nyheder = await _context.Nyheder.Where(d=> d.ExpireDate < DateTime.UtcNow).ToListAsync();
            Data = GetPaginatedResult(CurrentPage, PageSize);
            Count = Nyheder.Count;
        }

        public List<Nyhed> GetPaginatedResult(int currentPage, int pageSize = 10)
        {
            var data = Nyheder;
            return data.OrderByDescending(d => d.ExpireDate).Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
        }
    }
}
