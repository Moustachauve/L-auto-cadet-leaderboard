using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LautoCadetAPI.DTO
{
	public class Settings
	{
		[Required]
		public string Nom { get; set; }

		[Required]
		public int NbBilletsCirculation { get; set; }
	}
}
