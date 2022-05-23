using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Faellesskabet_Web.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace Faellesskabet_Web.Pages.AdminPanel
{
	[Authorize(Roles = "Admin")]
	public class EditUserModel : PageModel
	{
		private readonly ApplicationDbContext _context;

		public UserManager<ApplicationUser> UserManager { get; set; }

		public EditUserModel(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
		{
			_context = context;

			UserManager = userManager;

			ApplicationRoles = _context.Roles.ToList();
		}


		[BindProperty]
		public ApplicationUser ApplicationUser { get; set; }

		public List<IdentityUserRole<int>> ApplicationUserRoles { get; set; }

		public List<ApplicationRole> ApplicationRoles { get; set; }

		[BindProperty]
		public List<int> CheckedRoles { get; set; }

		public async Task<IActionResult> OnGetAsync(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			ApplicationUser = await _context.Users.FirstOrDefaultAsync(m => m.Id == id);

			if (ApplicationUser == null)
			{
				return NotFound();
			}

			ApplicationRoles = await _context.Roles.ToListAsync();
			ApplicationUserRoles = await _context.UserRoles.Where(u => u.UserId == ApplicationUser.Id).ToListAsync();

			return Page();
		}

		public async Task<IActionResult> OnPostAsync()
		{
			ApplicationRoles = await _context.Roles.ToListAsync();
			ApplicationUser = await _context.Users.FirstAsync(id => id.Id == ApplicationUser.Id);

			foreach (var role in ApplicationRoles)
			{
				if (CheckedRoles.Contains(role.Id))
				{
					if (await UserManager.IsInRoleAsync(ApplicationUser,
						ApplicationRoles.First(id => id.Id == role.Id).Name) == false)
					{
						await UserManager.AddToRoleAsync(ApplicationUser, ApplicationRoles.First(id => id.Id == role.Id).Name);
					}
				}
				else
				{
					if (await UserManager.IsInRoleAsync(ApplicationUser,
						ApplicationRoles.First(id => id.Id == role.Id).Name) == true)
					{
						await UserManager.RemoveFromRoleAsync(ApplicationUser, ApplicationRoles.First(id => id.Id == role.Id).Name);
						
					}
				}
			}

			return RedirectToPage("./Index");
		}
	}
}
