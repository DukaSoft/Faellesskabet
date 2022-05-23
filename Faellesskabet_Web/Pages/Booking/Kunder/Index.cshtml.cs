using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DataLibrary.Booking;
using Faellesskabet_Web.Data;
using Microsoft.AspNetCore.Authorization;

namespace Faellesskabet_Web.Pages.Booking.Kunder
{
    [Authorize(Roles = "BookingSystemManagerCreateEdit")]
    public class IndexModel : PageModel
    {
        private readonly Faellesskabet_Web.Data.ApplicationDbContext _context;

        public IndexModel(Faellesskabet_Web.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Customer> Customer { get;set; }

        public async Task OnGetAsync()
        {
            Customer = await _context.Customers.ToListAsync();
        }
    }
}
