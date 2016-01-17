using LautoCadetAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LautoCadetAPI.DTO
{
	public class GradeList : List<GradeListItem>
	{
		public GradeList() : base() { }

		public GradeList(IEnumerable<GradeListItem> grades) : base(grades) { }

		public GradeList(List<Grade> grades) : base()
		{
			foreach (Grade grade in grades)
			{
				this.Add(new GradeListItem(grade));
			}
		}
	}
}
