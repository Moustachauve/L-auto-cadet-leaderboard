using LautoCadetAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LautoCadetAPI.DTO
{
	public class SectionLeaderboard
	{
		public List<SectionLeaderboardItem> Sections { get; set; }

		public int NbBilletsDistribue { get; set; }

		public int NbBilletsVendu { get; set; }

		public SectionLeaderboard()
		{}

		public SectionLeaderboard(EscadronConfiguration escadron)
		{
			Sections = new List<SectionLeaderboardItem>();

			foreach (Section section in escadron.Sections)
			{
				SectionLeaderboardItem sectionLeaderboard = new SectionLeaderboardItem(section);
				Sections.Add(sectionLeaderboard);

				NbBilletsDistribue += sectionLeaderboard.NbBilletsDistribue;
				NbBilletsVendu += sectionLeaderboard.NbBilletsVendu;
			}

			Sections = Sections.OrderBy(s => s.Nom).ToList();
		}
	}
}
