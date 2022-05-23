using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DataLibrary.Booking;
using Faellesskabet_Web.Data;
using DataLibrary;
using Microsoft.AspNetCore.Authorization;

namespace Faellesskabet_Web.Pages.Booking.Ordre
{
    [Authorize(Roles = "BookingSystemManagerCreateEdit")]
    public class FinishModel : PageModel
    {
        private readonly Faellesskabet_Web.Data.ApplicationDbContext _context;

        public FinishModel(Faellesskabet_Web.Data.ApplicationDbContext context)
        {
            _context = context;
            Arrangements = _context.Arrangementer.ToList();
            PaymentMethods = _context.PaymentMethods.OrderBy(n => n.Name).ToList();
        }

        [BindProperty]
        public Order Order { get; set; }

        public List<Arrangement> Arrangements { get; set; }
        public List<PaymentMethod> PaymentMethods { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Order = await _context.Orders.Include(t => t.Tickets).FirstOrDefaultAsync(m => m.Id == id);

            if (Order == null)
            {
                return NotFound();
            }

            

            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Order).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(Order.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        public async Task<IActionResult> OnPostCancelAsync()
		{
            if (Order.Id == 0)
            {
                return NotFound();
            }

            Order = await _context.Orders.FindAsync(Order.Id);
            var Tickets = await _context.Tickets.Where(t => t.OrderId == Order.Id).ToListAsync();

            if (Order != null)
            {
                _context.Tickets.RemoveRange(Tickets);

                _context.Orders.Remove(Order);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }

        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.Id == id);
        }

        public decimal TicketCost(int Price, int Ammount)
		{
            return Ammount * Price;
		}

        public decimal OrderTotal()
		{
            decimal total = 0;

            foreach(var item in Order.Tickets)
			{
                total += (item.TicketCountChild * item.TicketPriceChild) + (item.TicketCountAdult * item.TicketPriceAdult);
			}

            return total;
		}
    }
}
