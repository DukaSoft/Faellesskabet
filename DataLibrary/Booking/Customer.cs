using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.Booking
{
	public class Customer
	{
		[DisplayName("Kunde ID")]
		public int Id { get; set; }
		[DisplayName("Navn")]
		public string Name { get; set; }
		[DisplayName("Adresse")]
		public string Address { get; set; }
		[DisplayName("Tlf")]
		public string Phone { get; set; }
		[DisplayName("E-Mail")]
		public string Email { get; set; }

	}
}
