using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LautoCadetAPI.Model
{
	public class Cadet
	{
		public int CadetID { get; set; }

		public string Prenom { get; set; }

		public string Nom { get; set; }

		public string Rank { get; set; }

		public int NbBilletsVendu { get; set; }
	}
}
