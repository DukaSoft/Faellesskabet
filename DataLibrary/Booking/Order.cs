using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.Booking
{
	public class Order
	{
		public int Id { get; set; }
		public int? CustomerId { get; set; }
		public List<Ticket> Tickets { get; set; }
		[Display(Name = "Betalings type")]
		[Required(ErrorMessage = "Betalings type skal angives")]
		[Range(0, 99999, ErrorMessage = "Betalings type skal angives")]
		public int PaymentMethod { get; set; }
		public DateTime Date { get; set; }
		[Display(Name = "Note")]
		[MaxLength(2000)]
		public string Notes { get; set; }
	}
}
