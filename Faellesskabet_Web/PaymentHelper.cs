using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Faellesskabet_Web
{
	public static class PaymentHelper
	{
		public enum PaymentTypeCreate
		{
			Kontant = 1,
			MobilePay = 2
		}
		public enum PaymentTypeView
		{
			Ubetalt = 0,
			Kontant = 1,
			MobilePay = 2
		}
	}
}
