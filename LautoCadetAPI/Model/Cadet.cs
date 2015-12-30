using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LautoCadetAPI.Model
{
	[JsonObject(IsReference = true)]
	public class Cadet
	{
		public int CadetID { get; set; }

        [Required]
		public string Prenom { get; set; }

        [Required]
		public string Nom { get; set; }

        [Required]
		public Grade Grade { get; set; }

		[Required]
		[Range(0, Int32.MaxValue)]
		public int NbBilletsDistribue { get; set; }

		[Required]
		[Range(0, Int32.MaxValue)]
		public int NbBilletsVendu { get; set; }

		public Section Section { get; set; }
	}
}
