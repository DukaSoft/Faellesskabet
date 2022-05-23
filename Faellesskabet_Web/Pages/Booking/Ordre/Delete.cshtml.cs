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
	[Authorize(Roles = "BookingSystemManagerDelete")]
	public class DeleteModel : PageModel
	{
		private readonly Faellesskabet_Web.Data.ApplicationDbContext _context;

		public DeleteModel(Faellesskabet_Web.Data.ApplicationDbContext context)
		{
			_context = context;
		}

		[BindProperty]
		public Order Order { get; set; }

		public IList<Ticket> Tickets { get; set; }

		public IList<Customer> Customers { get; set; }

		public IList<Arrangement> Arrangementers { get; set; }

		public async Task<IActionResult> OnGetAsync(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			Customers = await _context.Customers.ToListAsync();

			Arrangementers = await _context.Arrangementer.ToListAsync();

			Order = await _context.Orders.Include(t => t.Tickets).FirstOrDefaultAsync(m => m.Id == id);

			if (Order == null)
			{
				return NotFound();
			}
			return Page();
		}

		public async Task<IActionResult> OnPostAsync(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			Order = await _context.Orders.FindAsync(id);
			Tickets = await _context.Tickets.Where(t => t.OrderId == id).ToListAsync();

			if (Order != null)
			{
				_context.Tickets.RemoveRange(Tickets);

				_context.Orders.Remove(Order);
				await _context.SaveChangesAsync();
			}

			return RedirectToPage("./Index");
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
