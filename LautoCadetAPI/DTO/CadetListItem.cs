using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LautoCadetAPI.Model;
using System.ComponentModel.DataAnnotations;

namespace LautoCadetAPI.DTO
{
	public class CadetListItem
	{
		public int CadetID { get; set; }

		[Required]
		public string Prenom { get; set; }

		[Required]
		public string Nom { get; set; }

		public SectionListItem Section { get; set; }

		public GradeListItem Grade { get; set; }

		[Required]
		public int NbBilletsDistribue { get; set; }

		[Required]
		public int NbBilletsVendu { get; set; }

		public string DisplayName
		{
			get
			{
				string grade = "";
				if(Grade != null)
				{
					grade += Grade.Abreviation + " ";
				}

				return grade + Nom + ", " + Prenom.Substring(0, 1) + ".";
			}
		}

		public string FullName
		{
			get
			{
				return Prenom + " " + Nom;
			}
		}

		public CadetListItem()
		{}

		public CadetListItem(Cadet cadet)
		{
			CadetID = cadet.CadetID;
			Prenom = cadet.Prenom;
			Nom = cadet.Nom;
			NbBilletsDistribue = cadet.NbBilletsDistribue;
			NbBilletsVendu = cadet.NbBilletsVendu;
			if(cadet.Grade == null)
			{
				Grade = new GradeListItem() { GradeID = -1 };
			}
			else
			{
				Grade = new GradeListItem(cadet.Grade);
			}
			if (cadet.Section == null)
			{
				Section = new SectionListItem() { SectionID = -1 };
			}
			else
			{
				Section = new SectionListItem(cadet.Section);
			}
		}
	}
}
