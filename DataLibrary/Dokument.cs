using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary
{
	public class Dokument
	{
		public int Id { get; set; }
		[Required]
		[Display(Name = "Titel")]
		public string Title { get; set; }
		[Required]
		[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
		[Display(Name = "Dato")]
		[DataType(DataType.Date)]
		public DateTime Date { get; set; }
		public byte[] File { get; set; }
	}
}
