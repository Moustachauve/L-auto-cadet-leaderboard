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

		public List<Section> GetAllSections()
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

		public Cadet AddCadet(CadetListItem cadetModel)
		{
			Reload();
			Section section = GetSectionByID(cadetModel.Section.SectionID);
			Grade grade = GetGradeByID(cadetModel.Grade.GradeID);

			Cadet cadet = new Cadet();
			cadet.Grade = grade;
			cadet.NbBilletsDistribue = cadetModel.NbBilletsDistribue;
			cadet.NbBilletsVendu = cadetModel.NbBilletsVendu;
			cadet.Nom = cadetModel.Nom;
			cadet.Prenom = cadetModel.Prenom;
			cadet.Section = section;

			cadet.CadetID = escadron.GetNextCadetID();

			section.Cadets.Add(cadet);
			grade.Cadets.Add(cadet);
			Save();
			return cadet;
		}

		public Cadet EditCadet(CadetListItem cadetModel)
		{
			Reload();
			Cadet cadet = GetCadetByID(cadetModel.CadetID);
			Section section = GetSectionByID(cadetModel.Section.SectionID);
			Grade grade = GetGradeByID(cadetModel.Grade.GradeID);

			cadet.NbBilletsDistribue = cadetModel.NbBilletsDistribue;
			cadet.NbBilletsVendu = cadetModel.NbBilletsVendu;
			cadet.Nom = cadetModel.Nom;
			cadet.Prenom = cadetModel.Prenom;

			cadet.Section.Cadets.Remove(cadet);
			cadet.Grade.Cadets.Remove(cadet);

			cadet.Grade = grade;
			cadet.Section = section;

			section.Cadets.Add(cadet);
			grade.Cadets.Add(cadet);

			Save();
			return cadet;
		}

		public bool DeleteCadet(int cadetID)
		{
			return repo.DeleteCadet(cadetID);
		}

		#endregion

		#region Grade

		public List<Grade> GradeGetAll()
		{
			Reload();
			return repo.GetAllGrades();
		}

		public Grade GetGradeByID(int gradeId)
		{
			return repo.GetGradeByID(gradeId);
		}

		public Grade GradeAdd(GradeListItem gradeModel)
		{
			Reload();

			Grade grade = repo.GradeAdd(gradeModel);

			Save();
			return grade;
		}

		public Grade GradeEdit(GradeListItem gradeModel)
		{
			Reload();
			Grade grade = GetGradeByID(gradeModel.GradeID);

			grade.Nom = gradeModel.Nom;
			grade.Abreviation = gradeModel.Abreviation;

			Save();
			return grade;
		}

		public bool GradeDelete(int gradeID)
		{
			bool success = repo.GradeDelete(gradeID);
			if (success)
				Save();

			return success;
		}

		#endregion

		#region Leaderboard

		public IEnumerable<Cadet> GetTopTenSeller()
		{
			Reload();
			return repo.GetAllCadets().OrderByDescending(c => c.NbBilletsVendu).Take(10);
		}

		public SectionLeaderboard GetSectionLeaderboard()
		{
			Reload();
			return new SectionLeaderboard(escadron);
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

		public void Create(string path, string saveName = null)
		{
			repo.SetSaveFile(path, true);
			if(!string.IsNullOrWhiteSpace(saveName))
			{
				SetSaveName(saveName);
			}
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
