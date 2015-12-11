using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LautoCadetAPI.Model;

namespace LautoCadetAPI.DTO
{
    public class SectionListItem
    {
        public int SectionID { get; set; }

        public string Nom { get; set; }

		public int NbCadets { get; set; }

		public SectionListItem(Section section)
		{
			SectionID = section.SectionID;
			Nom = section.Nom;
			NbCadets = section.Cadets.Count;
		}
    }
}
