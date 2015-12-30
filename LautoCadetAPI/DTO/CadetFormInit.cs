using LautoCadetAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LautoCadetAPI.DTO
{
	public class CadetFormInit
	{
		public List<SectionListItem> Sections { get; set; }

		public List<GradeListItem> Grades { get; set; }

		public CadetFormInit()
		{}

		public CadetFormInit(List<Section> sections, List<Grade> grades)
		{
			Sections = new List<SectionListItem>();
			Grades = new List<GradeListItem>();

			foreach (Section section in sections)
			{
				Sections.Add(new SectionListItem(section));
			}
			foreach (Grade grade in grades)
			{
				Grades.Add(new GradeListItem(grade));
			}

		}
	} 
}
