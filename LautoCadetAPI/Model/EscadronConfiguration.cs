using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LautoCadetAPI.Model
{
	public class EscadronConfiguration
	{
		private int currentSectionID;
		private int currentCadetID;

		public string Nom { get; set; }

		public List<Section> Sections { get; set; }

		public int NbBilletsCirculation { get; set; }

		public int CurrentSectionID { get { return currentSectionID; } }
		public int CurrentCadetID { get { return currentCadetID; } }

		public EscadronConfiguration()
		{
			Sections = new List<Section>();
		}

		public int GetNextSectionID()
		{
			return currentSectionID++;
		}
	}
}
