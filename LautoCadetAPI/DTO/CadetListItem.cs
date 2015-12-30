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

		[Required]
		public GradeListItem Grade { get; set; }

		[Required]
		public int NbBilletsDistribue { get; set; }

		[Required]
		public int NbBilletsVendu { get; set; }

		[Required]
		public SectionListItem Section { get; set; }

		public string DisplayName
		{
			get
			{
				return Grade.Abreviation + " " + Nom + ", " + Prenom.Substring(0, 1) + ".";
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
			Grade = new GradeListItem(cadet.Grade);
			NbBilletsDistribue = cadet.NbBilletsDistribue;
			NbBilletsVendu = cadet.NbBilletsVendu;
			Section = new SectionListItem(cadet.Section);
		}
	}
}
