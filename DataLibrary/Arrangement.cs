using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataLibrary
{
	public class Arrangement
	{
		public int Id { get; set; }

		[Display(Name = "Titel")]
		public string Title { get; set; }

		[Display(Name = "Dato")]
		public DateTime Date { get; set; }

		[Display(Name = "Tekst")]
		[DataType(DataType.MultilineText)]
		public string Content { get; set; }

		public byte[] ImageContent { get; set; }

		[Display(Name = "Aktiver")]
		public bool Active { get; set; }

		[Display(Name ="Antal Børnebilletter")]
		public int TicketTotalChild { get; set; }

		[Display(Name = "Pris Barn")]
		public int TicketPriceChild { get; set; }

		[Display(Name = "Antal Voksenbilletter")]
		public int TicketTotalAdult { get; set; }

		[Display(Name = "Pris Voksen")]
		public int TicketPriceAdult { get; set; }

		[Display(Name = "Billetsalg åben")]
		[Required]
		public bool TicketSaleOpen { get; set; }

	}
}
