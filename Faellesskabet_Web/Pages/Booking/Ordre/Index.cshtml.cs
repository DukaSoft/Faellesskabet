using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DataLibrary.Booking;
using Faellesskabet_Web.Data;
using DataLibrary;
using Microsoft.AspNetCore.Authorization;

namespace Faellesskabet_Web.Pages.Booking.Ordre
{
    [Authorize(Roles = "BookingSystemManagerCreateEdit")]
    public class IndexModel : PageModel
    {
        private readonly Faellesskabet_Web.Data.ApplicationDbContext _context;

        public IndexModel(Faellesskabet_Web.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Order> Order { get;set; }
        public IList<Arrangement> Arrangementers { get; set; }
        public IList<Customer> Customers { get; set; }

        public async Task OnGetAsync()
        {
            Order = await _context.Orders.Include(t => t.Tickets).OrderByDescending(Order => Order.Date).ThenByDescending(Order=> Order.Id).ToListAsync();
            Arrangementers = await _context.Arrangementer.ToListAsync();
            Customers = await _context.Customers.ToListAsync();
        }

        public async Task<string> PayedWith(int id)
		{
            var x = await _context.PaymentMethods.FirstOrDefaultAsync(i => i.Id == id);

            if(x == null)
			{
                return "Ikke Betalt";
			}

            return x.Name;
		}
    }
}
