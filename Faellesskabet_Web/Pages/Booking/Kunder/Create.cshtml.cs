using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using DataLibrary.Booking;
using Faellesskabet_Web.Data;
using Microsoft.AspNetCore.Authorization;

namespace Faellesskabet_Web.Pages.Booking.Kunder
{
	[Authorize(Roles = "BookingSystemManagerCreateEdit")]
	public class CreateModel : PageModel
	{
		private readonly Faellesskabet_Web.Data.ApplicationDbContext _context;

		public CreateModel(Faellesskabet_Web.Data.ApplicationDbContext context)
		{
			_context = context;
		}


		public IActionResult OnGet(string doreturn)
		{
			DoReturn = doreturn;
			return Page();
		}
		[BindProperty]
		public string DoReturn { get; set; }

		[BindProperty]
		public Customer Customer { get; set; }

		public async Task<IActionResult> OnPostAsync()
		{
			if (!ModelState.IsValid)
			{
				return Page();
			}

			var customer =_context.Customers.Add(Customer);
			await _context.SaveChangesAsync();

			if(DoReturn == null)
			{
				return RedirectToPage("./Index");
			}
			else
			{
				return RedirectToPage("../Ordre/Create/", new { customer.Entity.Id });
			}
		}
	}
}
