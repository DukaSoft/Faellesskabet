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
using System.ComponentModel.DataAnnotations;

namespace Faellesskabet_Web.Pages.Booking.Ordre
{
	[Authorize(Roles = "BookingSystemManagerCreateEdit")]
	public class PrintModel : PageModel
	{
		private readonly Faellesskabet_Web.Data.ApplicationDbContext _context;

		public PrintModel(Faellesskabet_Web.Data.ApplicationDbContext context)
		{
			_context = context;
		}

		[BindProperty(Name = "SearchDate")]
		[DataType(DataType.Date)]
		public DateTime SearchDate { get; set; }

		public IList<Order> Order { get; set; }
		public IList<Arrangement> Arrangementer { get; set; }
		public List<Ticket> Tickets { get; set; } = new();
		public List<tSold> TicketsSoldMobilePay { get; set; } = new();
		public List<tSold> TicketsSoldCash { get; set; } = new();

		public int PaymentTotalMobilePay { get; set; } = 0;
		public int PaymentTotalKontant { get; set; } = 0;

		public string SearchDateText { get; set; } = DateTime.Now.ToShortDateString();

		public async Task OnGetAsync(string searchDate = "")
		{
			Arrangementer = await _context.Arrangementer.ToListAsync();

			if (searchDate != string.Empty)
			{
				SearchDate = DateTime.Parse(searchDate);
				SearchDateText = SearchDate.ToShortDateString();
			}
			else
			{
				SearchDate = DateTime.Now;
			}
			Order = await _context.Orders.Where(d => d.Date.Date == SearchDate.Date).Include(t => t.Tickets).ToListAsync();

			foreach (Order orders in Order)
			{
				if (orders.PaymentMethod == 1)
				{
					// If MobilePay
					foreach (Ticket ticket in orders.Tickets)
					{
						Tickets.Add(ticket);
						bool found = false;
						for (int i = 0; i < TicketsSoldMobilePay.Count; i++)
						{
							if (TicketsSoldMobilePay[i].ArrangementId == ticket.ArrangementId)
							{
								TicketsSoldMobilePay[i].ChildTicketsSold += ticket.TicketCountChild;
								TicketsSoldMobilePay[i].ChildPriceTotal += ticket.TicketPriceChild * ticket.TicketCountChild;
								TicketsSoldMobilePay[i].AdultTicketsSold += ticket.TicketCountAdult;
								TicketsSoldMobilePay[i].AdultPriceTotal += ticket.TicketPriceAdult * ticket.TicketCountAdult;

								PaymentTotalMobilePay += (ticket.TicketPriceChild * ticket.TicketCountChild) +
									(ticket.TicketPriceAdult * ticket.TicketCountAdult);


								found = true;
							}
						}
						if (found == false)
						{
							TicketsSoldMobilePay.Add(new tSold
							{
								ArrangementId = ticket.ArrangementId,
								ChildTicketsSold = ticket.TicketCountChild,
								ChildPriceTotal = ticket.TicketPriceChild * ticket.TicketCountChild,
								AdultTicketsSold = ticket.TicketCountAdult,
								AdultPriceTotal = ticket.TicketPriceAdult * ticket.TicketCountAdult,
								PaymentType = orders.PaymentMethod
							});
							if (orders.PaymentMethod == 1)
							{
								PaymentTotalMobilePay += (ticket.TicketPriceChild * ticket.TicketCountChild) +
									(ticket.TicketPriceAdult * ticket.TicketCountAdult);
							}
							else if (orders.PaymentMethod == 2)
							{
								PaymentTotalKontant += (ticket.TicketPriceChild * ticket.TicketCountChild) +
									(ticket.TicketPriceAdult * ticket.TicketCountAdult);
							}

						}
					}
				}
			}

			Tickets = new();
			// 
			foreach (Order orders in Order)
			{
				if (orders.PaymentMethod == 2)
				{
					// If Cash
					foreach (Ticket ticket in orders.Tickets)
					{
						Tickets.Add(ticket);
						bool found = false;
						for (int i = 0; i < TicketsSoldCash.Count; i++)
						{
							if (TicketsSoldCash[i].ArrangementId == ticket.ArrangementId)
							{
								TicketsSoldCash[i].ChildTicketsSold += ticket.TicketCountChild;
								TicketsSoldCash[i].ChildPriceTotal += ticket.TicketPriceChild * ticket.TicketCountChild;
								TicketsSoldCash[i].AdultTicketsSold += ticket.TicketCountAdult;
								TicketsSoldCash[i].AdultPriceTotal += ticket.TicketPriceAdult * ticket.TicketCountAdult;

								PaymentTotalKontant += (ticket.TicketPriceChild * ticket.TicketCountChild) +
									(ticket.TicketPriceAdult * ticket.TicketCountAdult);

								found = true;
							}
						}
						if (found == false)
						{
							TicketsSoldCash.Add(new tSold
							{
								ArrangementId = ticket.ArrangementId,
								ChildTicketsSold = ticket.TicketCountChild,
								ChildPriceTotal = ticket.TicketPriceChild * ticket.TicketCountChild,
								AdultTicketsSold = ticket.TicketCountAdult,
								AdultPriceTotal = ticket.TicketPriceAdult * ticket.TicketCountAdult,
								PaymentType = orders.PaymentMethod
							});
							if (orders.PaymentMethod == 1)
							{
								PaymentTotalMobilePay += (ticket.TicketPriceChild * ticket.TicketCountChild) +
									(ticket.TicketPriceAdult * ticket.TicketCountAdult);
							}
							else if (orders.PaymentMethod == 2)
							{
								PaymentTotalKontant += (ticket.TicketPriceChild * ticket.TicketCountChild) +
									(ticket.TicketPriceAdult * ticket.TicketCountAdult);
							}

						}
					}
				}
			}
		}

		public IActionResult OnPost()
		{
			return LocalRedirect($"/Booking/Ordre/Print?SearchDate={SearchDate.ToString("yyyy-MM-dd")}");
		}

		public int Total(int child, int adult)
		{
			return child + adult;
		}

		public int TotalSold()
		{
			return TotalSoldKontant() + TotalSoldMobilePay();
		}

		public int TotalSoldMobilePay()
		{
			int total = 0;
			foreach (var t in TicketsSoldMobilePay)
			{
				if (t.PaymentType == 1)
				{
					total += t.AdultPriceTotal;
					total += t.ChildPriceTotal;
				}
			}
			return total;
		}

		public int TotalSoldKontant()
		{
			int total = 0;
			foreach (var t in TicketsSoldCash)
			{
				if (t.PaymentType == 2)
				{
					total += t.AdultPriceTotal;
					total += t.ChildPriceTotal;
				}
			}
			return total;
		}
	}

	public class tSold
	{
		public int ArrangementId;
		public int ChildTicketsSold;
		public int AdultTicketsSold;
		public int ChildPriceTotal;
		public int AdultPriceTotal;
		public int PaymentType;
	}
}
