using LautoCadetAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LautoCadetAPI.DAL
{
	public class Service
	{
		#region Singleton

		private static Service instance;
		public static Service Instance
		{
			get
			{
				if (instance == null)
					instance = new Service();
				return instance;
			}
		}

		private Service()
		{ }

		#endregion

		private Repository repo = new Repository();

		public void Save()
		{
			repo.Save();
		}

		public void Reload()
		{
			repo.Load();
		}

		public IEnumerable<Cadet> GetTopTen()
		{
			return repo.GetAllCadets().OrderByDescending(c => c.NbBilletsVendu);//.Take(10);
		}

		public Section CreateSection(string name)
		{
			Section section = new Section();
			section.Nom = name;

			Cadet cadet1 = new Cadet();
			cadet1.Nom = "Pôl";
			cadet1.Prenom = "Jean";
			cadet1.Grade = "Sergent";
			cadet1.NbBilletsVendu = 5;

			Cadet cadet2 = new Cadet();
			cadet2.Nom = "Deschanp";
			cadet2.Prenom = "Yvons";
			cadet2.Grade = "Caporal";
			cadet2.NbBilletsVendu = 3;

			Cadet cadet3 = new Cadet();
			cadet3.Nom = "Gagnier";
			cadet3.Prenom = "Eva";
			cadet3.Grade = "Cadet";
			cadet3.NbBilletsVendu = 15;

			section.Cadets.Add(cadet1);
			section.Cadets.Add(cadet2);
			section.Cadets.Add(cadet3);

			repo.EscadronConfiguration.Sections.Add(section);
			Save();

			return section;
		}

		public Cadet AddCadet(Cadet cadet)
		{
			Section section = repo.EscadronConfiguration.Sections.FirstOrDefault();

			section.Cadets.Add(cadet);

			return cadet;
        }
	}
}
