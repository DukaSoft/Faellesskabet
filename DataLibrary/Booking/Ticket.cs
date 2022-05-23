using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.Booking
{
	public class Ticket
	{
		public int Id { get; set; }
		public int ArrangementId { get; set; }
		public int TicketCountChild { get; set; }
		public int TicketPriceChild { get; set; }
		public int TicketCountAdult { get; set; }
		public int TicketPriceAdult { get; set; }
		public int OrderId { get; set; }
	}
}
