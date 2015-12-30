using LautoCadetAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LautoCadetAPI.DTO
{
	public class GradeListItem
	{
		public int GradeID { get; set; }

		public string Nom { get; set; }

		public string Abreviation { get; set; }

		public GradeListItem()
		{}

		public GradeListItem(Grade grade)
		{
			GradeID = grade.GradeID;
			Nom = grade.Nom;
			Abreviation = grade.Abreviation;
		}
	}
}
