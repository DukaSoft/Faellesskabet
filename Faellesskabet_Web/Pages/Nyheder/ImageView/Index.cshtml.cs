using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Faellesskabet_Web.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Faellesskabet_Web.Pages.Nyheder.ImageView
{
    public class IndexModel : PageModel
    {
        public string Img { get; set; }
        private readonly Faellesskabet_Web.Data.ApplicationDbContext _context;

		public IndexModel(ApplicationDbContext context)
		{
			_context = context;
		}

        public void OnGet(int Id)
        {
            var nyhed= _context.Nyheder.FirstOrDefault(i => i.Id == Id);
            if(nyhed.ImageContent != null)
			{
                Img = "data:image/jpg;base64," + @Convert.ToBase64String(nyhed.ImageContent);
			}
        }
    }
}
