using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataLibrary
{
	public class Nyhed
	{
		public int Id { get; set; }
		public DateTime PublishDate { get; set; }
		public DateTime ExpireDate { get; set; }

		[Required(ErrorMessage = "Der mangler en Titel")]
		public string Title { get; set; }
		public string TextContent { get; set; }
		public byte[] ImageContent { get; set; }
		public bool Important { get; set; }
		public bool ShowOnLanding { get; set; }

	}
}
