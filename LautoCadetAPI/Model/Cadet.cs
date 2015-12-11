using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LautoCadetAPI.Model
{
	public class Cadet
	{
		public int CadetID { get; set; }

        [Required]
        [StringLength(32, MinimumLength = 1)]
		public string Prenom { get; set; }

        [Required]
        [StringLength(32, MinimumLength = 1)]
		public string Nom { get; set; }

        [StringLength(32, MinimumLength=0)]
		public string Grade { get; set; }

        [Required]
        [Range(0, Int32.MaxValue)]
		public int NbBilletsVendu { get; set; }
	}
}
