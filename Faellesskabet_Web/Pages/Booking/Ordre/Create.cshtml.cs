using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using DataLibrary.Booking;
using Faellesskabet_Web.Data;
using DataLibrary;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace Faellesskabet_Web.Pages.Booking.Ordre
{
	[Authorize(Roles = "BookingSystemManagerCreateEdit")]
	public class CreateModel : PageModel
	{
		private readonly Faellesskabet_Web.Data.ApplicationDbContext _context;

		public CreateModel(Faellesskabet_Web.Data.ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<IActionResult> OnGet(int? Id)
		{
			if (Id != null)
			{
				CustomerId = Id;
			}

			Customers = await _context.Customers.ToListAsync();
			InitalPaymentMethod = await _context.PaymentMethods.FirstOrDefaultAsync(i => i.Id == 1);

			Arrangements = await _context.Arrangementer
				.Where(d => d.Date > DateTime.Now)
				.Where(o => o.TicketSaleOpen)
				.OrderBy(d => d.Date)
				.ThenBy(t => t.Title)
				.ToListAsync();

			Tickets = new();
			foreach (var item in Arrangements)
			{
				Tickets.Add(new Ticket
				{
					ArrangementId = item.Id,
					TicketPriceChild = item.TicketPriceChild,
					TicketPriceAdult = item.TicketPriceAdult
				});
			}

			return Page();
		}
		[BindProperty]
		public IList<Arrangement> Arrangements { get; set; }

		[BindProperty]
		public Order Order { get; set; }

		[BindProperty]
		public List<Ticket> Tickets { get; set; }

		[BindProperty]
		public int? CustomerId { get; set; }

		public List<Customer> Customers { get; set; }

		[BindProperty]
		public PaymentMethod InitalPaymentMethod { get; set; }

		public async Task<IActionResult> OnPostAsync()
		{



			if (!ModelState.IsValid)
			{
				return Page();
			}

			Order.CustomerId = CustomerId;
			Order.Date = DateTime.Now;

			if (Order.Tickets == null)
			{
				Order.Tickets = new();
			}

			bool hasTickets = false;
			foreach (var item in Tickets)
			{
				if (item.TicketCountAdult > 0 || item.TicketCountChild > 0)
				{
					hasTickets = true;
					Order.Tickets.Add(item);
				}
			}

			if (hasTickets == false)
			{
				return RedirectToPage("./Create");
			}

			var r = _context.Orders.Add(Order);

			await _context.SaveChangesAsync();

			return LocalRedirect($"/Booking/Ordre/Finish/{r.Entity.Id}");
		}

		public async Task<int> TicketsLeftChild(int ArrangementId)
		{
			int ticketsSold = 0;
			var ticketList = await _context.Tickets.Where(i => i.ArrangementId == ArrangementId).ToListAsync();
			foreach (var ticket in ticketList)
			{
				ticketsSold += ticket.TicketCountChild;
			}
			var ArrangementTicketsTotal = (await _context.Arrangementer.FirstOrDefaultAsync(i => i.Id == ArrangementId)).TicketTotalChild;
			return ArrangementTicketsTotal - ticketsSold;
		}

		public async Task<int> TicketsLeftAdult(int ArrangementId)
		{
			int ticketsSold = 0;
			var ticketList = await _context.Tickets.Where(i => i.ArrangementId == ArrangementId).ToListAsync();
			foreach (var ticket in ticketList)
			{
				ticketsSold += ticket.TicketCountAdult;
			}
			var ArrangementTicketsTotal = (await _context.Arrangementer.FirstOrDefaultAsync(i => i.Id == ArrangementId)).TicketTotalAdult;
			return ArrangementTicketsTotal - ticketsSold;
		}
	}
}
