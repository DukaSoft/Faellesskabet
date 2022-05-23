using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DataLibrary;
using DataLibrary.Booking;
using Faellesskabet_Web.Data;
using Microsoft.AspNetCore.Authorization;

namespace Faellesskabet_Web.Pages.Booking.Ordre
{
	[Authorize(Roles = "BookingSystemManagerCreateEdit")]
	public class DetailsModel : PageModel
	{
		private readonly Faellesskabet_Web.Data.ApplicationDbContext _context;

		public DetailsModel(Faellesskabet_Web.Data.ApplicationDbContext context)
		{
			_context = context;
		}

		public Order Order { get; set; }
		public Customer Customer { get; set; }
		public List<Ticket> Tickets { get; set; }
		public List<Arrangement> Arrangements { get; set; }

		public async Task<IActionResult> OnGetAsync(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			Order = await _context.Orders.FirstOrDefaultAsync(m => m.Id == id);

			if (Order == null)
			{
				return NotFound();
			}

			Customer = await _context.Customers.FirstOrDefaultAsync(c => c.Id == Order.CustomerId);
			Tickets = await _context.Tickets.Where(o => o.OrderId == id).ToListAsync();

			Arrangements = new();
			foreach (var item in Tickets)
			{
				Arrangement arrangement = await _context.Arrangementer.FirstOrDefaultAsync(i => i.Id == item.ArrangementId);
				Arrangements.Add(arrangement);
			}

			return Page();
		}

		public decimal TicketTotal(int id)
		{
			decimal total = 0;

			Ticket ticket = Tickets.FirstOrDefault(i => i.Id == id);

			total += (ticket.TicketPriceAdult * ticket.TicketCountAdult);
			total += (ticket.TicketPriceChild * ticket.TicketCountChild);


			return total;
		}

		public decimal OrderTotal()
		{
			decimal total = 0;
			foreach(var t in Tickets)
			{
				total += (t.TicketPriceAdult * t.TicketCountAdult);
				total += (t.TicketPriceChild * t.TicketCountChild);
			}

			return total;
		}

		public async Task<string> PayedWith(int id)
		{
			var x = await _context.PaymentMethods.FirstOrDefaultAsync(i => i.Id == id);

			if (x == null)
			{
				return "Ikke Betalt";
			}

			return x.Name;
		}
	}
}
