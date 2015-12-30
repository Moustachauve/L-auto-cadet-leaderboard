using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LautoCadetAPI.Model
{
	[JsonObject(IsReference = true)]
	public class Grade
	{
		public int GradeID { get; set; }

		public string Nom { get; set; }

		public string Abreviation { get; set; }

		public List<Cadet> Cadets { get; set; }

		public Grade()
		{
			Cadets = new List<Cadet>();
		}
	}
}
