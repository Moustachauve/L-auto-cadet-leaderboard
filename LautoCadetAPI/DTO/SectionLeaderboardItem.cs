using LautoCadetAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LautoCadetAPI.DTO
{
	public class SectionLeaderboardItem
	{
		public string Nom { get; set; }

		public int NbBilletsDistribue { get; set; }

		public int NbBilletsVendu { get; set; }

		public SectionLeaderboardItem()
		{ }

		public SectionLeaderboardItem(Section section)
		{
			Nom = section.Nom;

			foreach (Cadet cadet in section.Cadets)
			{
				NbBilletsVendu += cadet.NbBilletsVendu;
				NbBilletsDistribue += cadet.NbBilletsDistribue;
			}
		}
	}
}
