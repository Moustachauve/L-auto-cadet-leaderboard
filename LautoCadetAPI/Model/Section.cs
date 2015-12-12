using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LautoCadetAPI.Model
{
	[JsonObject(IsReference = true)]
	public class Section
	{
		public int SectionID { get; set; }

        [Required]
        [StringLength(30, MinimumLength=1)]
		public string Nom { get; set; }

		public List<Cadet> Cadets { get; set; }

		public Section()
		{
			Cadets = new List<Cadet>();
        }
	}
}
