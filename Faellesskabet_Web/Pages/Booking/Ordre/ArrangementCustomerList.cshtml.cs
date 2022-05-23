using DataLibrary;
using DataLibrary.Booking;
using Faellesskabet_Web.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Faellesskabet_Web.Pages.Booking.Ordre
{
	[Authorize(Roles = "BookingSystemManagerCreateEdit")]
	public class ArrangementCustomerListModel : PageModel
	{
		private readonly ApplicationDbContext _dbContext;

		public Arrangement arrangement { get; set; }
		public List<Ticket> tickets { get; set; }

		public List<Customer> customers { get; set; } = new();

		public ArrangementCustomerListModel(ApplicationDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<IActionResult> OnGetAsync(int id)
		{
			arrangement = await _dbContext.Arrangementer.FirstOrDefaultAsync(a => a.Id == id);
			tickets = await _dbContext.Tickets.Where(t => t.ArrangementId == id).ToListAsync();
			foreach (var t in tickets)
			{
				var order = await _dbContext.Orders.FirstOrDefaultAsync(o => o.Id == t.OrderId);
				var customer = await _dbContext.Customers.FirstOrDefaultAsync(c => c.Id == order.CustomerId);
				if (customers.Contains(customer) == false)
				{
					customers.Add(customer);
				}
			}

			return Page();
		}

		public int GetUserTicketCountTotal(int CustomerId)
		{
			int TicketCount = 0;

			foreach (var t in tickets)
			{
				var order = _dbContext.Orders.FirstOrDefault(o => o.Id == t.OrderId);
				if (order.CustomerId == CustomerId)
				{
					TicketCount += t.TicketCountChild + t.TicketCountAdult;
				}
			}

			return TicketCount;
		}

		public int GetUserTicketCountChild(int CustomerId)
		{
			int TicketCount = 0;

			foreach (var t in tickets)
			{
				var order = _dbContext.Orders.FirstOrDefault(o => o.Id == t.OrderId);
				if (order.CustomerId == CustomerId)
				{
					TicketCount += t.TicketCountChild;
				}
			}

			return TicketCount;
		}
		public int GetUserTicketCountAdult(int CustomerId)
		{
			int TicketCount = 0;

			foreach (var t in tickets)
			{
				var order = _dbContext.Orders.FirstOrDefault(o => o.Id == t.OrderId);
				if (order.CustomerId == CustomerId)
				{
					TicketCount += t.TicketCountAdult;
				}
			}

			return TicketCount;
		}

		public int GetTotalTickets()
		{
			int total = 0;

			foreach(var t in tickets)
			{
				total += t.TicketCountChild + t.TicketCountAdult;
			}

			return total;
		}

		public int GetTotalTicketsChild()
		{
			int total = 0;

			foreach (var t in tickets)
			{
				total += t.TicketCountChild;
			}

			return total;
		}
		public int GetTotalTicketsAdult()
		{
			int total = 0;

			foreach (var t in tickets)
			{
				total += t.TicketCountAdult;
			}

			return total;
		}

	}
}
