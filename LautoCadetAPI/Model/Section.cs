using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LautoCadetAPI.Model
{
	public class Section
	{
		public int SectionID { get; set; }

		public string Nom { get; set; }

		public List<Cadet> Cadets { get; set; }

		public Section()
		{
			Cadets = new List<Cadet>();
        }
	}
}
