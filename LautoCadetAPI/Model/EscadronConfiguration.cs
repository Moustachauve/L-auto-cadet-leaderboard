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
	public class EscadronConfiguration
	{
		public string Nom { get; set; }

		public List<Grade> Grades { get; set; }

		public List<Section> Sections { get; set; }

		public int NbBilletsCirculation { get; set; }

		public int CurrentSectionID { get; set; }
		public int CurrentCadetID { get; set; }
		public int CurrentGradeID { get; set; }

		public EscadronConfiguration()
		{
			Nom = "Nom par défaut";
			Sections = new List<Section>();
			Grades = new List<Grade>();
		}

        public int GetNextSectionID()
        {
			return ++CurrentSectionID;
        }
		public int GetNextCadetID()
		{
			return ++CurrentCadetID;
		}
		public int GetNextGradeID()
		{
			return ++CurrentGradeID;
		}
	}
}
