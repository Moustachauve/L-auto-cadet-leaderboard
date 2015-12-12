using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LautoCadetAPI.Model;

namespace LautoCadetAPI.DTO
{
	public class CadetListItem
	{
		public int CadetID { get; set; }

		public string Prenom { get; set; }

		public string Nom { get; set; }

		public string Grade { get; set; }

		public int NbBilletsVendu { get; set; }

		public int SectionID { get; set; }

		public string NomSection { get; set; }

		public string DisplayName
		{
			get
			{
				return Nom + ", " + Prenom.Substring(0, 1) + ".";
			}
		}

		public CadetListItem()
		{}

		public CadetListItem(Cadet cadet)
		{
			CadetID = cadet.CadetID;
			Prenom = cadet.Prenom;
			Nom = cadet.Nom;
			Grade = cadet.Grade;
			NbBilletsVendu = cadet.NbBilletsVendu;
			SectionID = cadet.Section.SectionID;
			NomSection = cadet.Section.Nom;
		}
	}
}
