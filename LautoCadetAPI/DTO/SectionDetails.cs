using LautoCadetAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LautoCadetAPI.DTO
{
	public class SectionDetails
	{
		public int SectionID { get; set; }

		public string Nom { get; set; }

		public int NbCadets { get; set; }

		public int NbBilletsDistribue { get; set; }

		public int NbBilletsVendu { get; set; }

		public CadetList Cadets { get; set; }

		public SectionDetails()
		{ }

		public SectionDetails(Section section)
		{
			SectionID = section.SectionID;
			Nom = section.Nom;
			NbCadets = section.Cadets.Count;
			Cadets = new CadetList(section.Cadets);

			foreach (Cadet cadet in section.Cadets)
			{
				NbBilletsVendu += cadet.NbBilletsVendu;
				NbBilletsDistribue += cadet.NbBilletsDistribue;
			}
		}

	}
}
