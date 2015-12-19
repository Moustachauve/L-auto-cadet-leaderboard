using LautoCadetAPI.DTO;
using LautoCadetAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace LautoCadetAPI.DAL
{
	public class Service
	{

		#region Singleton & Initialization

		private Repository repo = new Repository();
		private EscadronConfiguration escadron;

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

		#region Section

		public IEnumerable<Section> GetAllSections()
		{
			return repo.GetAllSections();
		}

		public Section AddSection(string name)
		{
			Reload();
			Section section = new Section();
			section.Nom = name;
			section.SectionID = escadron.GetNextSectionID();

			escadron.Sections.Add(section);
			Save();

			return section;
		}

		public Section GetSectionByID(int id)
		{
			return GetAllSections().First(s => s.SectionID == id);
		}

		public Section SectionEdit(SectionListItem sectionModel)
		{
			Reload();
			Section section = GetSectionByID(sectionModel.SectionID);

			section.Nom = sectionModel.Nom;

			Save();
			return section;
		}

		public bool SectionDelete(int sectionID)
		{
			return repo.SectionDelete(sectionID);
		}

		#endregion

		#region Cadet

		public Cadet GetCadetByID(int id)
		{
			Reload();
			return GetAllCadets().First(c => c.CadetID == id);
		}

		public IEnumerable<Cadet> GetAllCadets()
		{
			Reload();
			return repo.GetAllCadets();
		}

		public IEnumerable<Cadet> GetCadetsBySection(int sectionID)
		{
			return GetSectionByID(sectionID).Cadets;
		}

		public IEnumerable<Cadet> GetTopTenSeller()
		{
			Reload();
			return repo.GetAllCadets().OrderByDescending(c => c.NbBilletsVendu).Take(10);
		}

		public Cadet AddCadet(CadetListItem cadetModel)
		{
			Reload();
			Section section = GetSectionByID(cadetModel.SectionID);

			Cadet cadet = new Cadet();
			cadet.Grade = cadetModel.Grade;
			cadet.NbBilletsVendu = cadetModel.NbBilletsVendu;
			cadet.Nom = cadetModel.Nom;
			cadet.Prenom = cadetModel.Prenom;
			cadet.Section = section;

			cadet.CadetID = escadron.GetNextCadetID();

			section.Cadets.Add(cadet);
			Save();
			return cadet;
		}

		public Cadet EditCadet(CadetListItem cadetModel)
		{
			Reload();
			Cadet cadet = GetCadetByID(cadetModel.CadetID);
			Section section = GetSectionByID(cadetModel.SectionID);

			cadet.Grade = cadetModel.Grade;
			cadet.NbBilletsVendu = cadetModel.NbBilletsVendu;
			cadet.Nom = cadetModel.Nom;
			cadet.Prenom = cadetModel.Prenom;

			cadet.Section.Cadets.Remove(cadet);

			cadet.Section = section;
			section.Cadets.Add(cadet);
			Save();
			return cadet;
		}

		public bool DeleteCadet(int cadetID)
		{
			return repo.DeleteCadet(cadetID);
		}

		#endregion

		#region IO

		public string GetSaveName()
		{
			Reload();
			return escadron.Nom;
		}

		public void SetSaveName(string nom)
		{
			Reload();

			if (string.IsNullOrWhiteSpace(escadron.Nom))
				return;

			escadron.Nom = nom;
			Save();
		}

		public void Open(string path)
		{
			repo.SetSaveFile(path);
		}

		public void Create(string path)
		{
			repo.SetSaveFile(path, true);
		}

		public void Save()
		{
			repo.Save();
		}

		public void Reload()
		{
			repo.Load();
			escadron = repo.EscadronConfiguration;
		}
		
		public List<FichierRecent> RecentFilesGetAll()
		{
			return repo.GetRecentFiles();
		}

		#endregion

	}
}
