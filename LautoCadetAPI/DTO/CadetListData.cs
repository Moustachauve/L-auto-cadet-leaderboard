using LautoCadetAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LautoCadetAPI.DTO
{
	public class CadetListData
	{
		public CadetList Cadets { get; set; }

		public List<SectionListItem> Sections { get; set; }

		public GradeList Grades { get; set; }

		public CadetListData() {}

		public CadetListData(IEnumerable<Cadet> cadets, IEnumerable<Section> sections, IEnumerable<Grade> grades)
		{
			cadets = cadets.OrderBy(c => c.Nom).ThenBy(c => c.Prenom).ThenBy(c => c.Grade == null ? int.MaxValue : c.Grade.Ordre);
			Cadets = new CadetList(cadets);
			Grades = new GradeList(grades);

			Sections = new List<SectionListItem>();
			foreach (Section section in sections)
			{
				Sections.Add(new SectionListItem(section));
			}
		}
	}
}
