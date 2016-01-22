using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LautoCadetAPI.DTO;
using LautoCadetAPI.Model;
using System.IO;

namespace LautoCadetAPI.DAL
{
	internal class Service : IService
	{
		private IRepository<EscadronConfiguration> escadronManager;
		private IRepository<FichiersRecentsConfiguration> recentFilesManager;
		private EscadronConfiguration data;
		private FichiersRecentsConfiguration recentFiles;

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
		{
			escadronManager = new Repository<EscadronConfiguration>(WebApi.DEFAULT_FILE_PATH);
			recentFilesManager = new Repository<FichiersRecentsConfiguration>(WebApi.DEFAULT_RECENT_FILES_PATH);
			data = escadronManager.GetData();
			recentFiles = recentFilesManager.GetData();

			AddToRecentFiles();
		}

		public void Save()
		{
			escadronManager.Save();
		}

		#region Cadet

		public Cadet CadetGetByID(int id)
		{
			return data.Cadets.FirstOrDefault(c => c.CadetID == id);
		}

		public IEnumerable<Cadet> CadetGetAll()
		{
			List<Cadet> cadets = new List<Cadet>();
			cadets.AddRange(data.Cadets);
			return cadets;
		}

		public Cadet CadetAdd(CadetListItem cadetModel)
		{
			Cadet cadet = new Cadet();
			Section section = null;
			Grade grade = null;

			if (cadetModel.Section != null)
			{
				section = SectionGetByID(cadetModel.Section.SectionID);
				section.Cadets.Add(cadet);
			}
			if (cadetModel.Grade != null)
			{
				grade = GradeGetByID(cadetModel.Grade.GradeID);
				grade.Cadets.Add(cadet);
			}

			cadet.Grade = grade;
			cadet.NbBilletsDistribue = cadetModel.NbBilletsDistribue;
			cadet.NbBilletsVendu = cadetModel.NbBilletsVendu;
			cadet.Nom = cadetModel.Nom;
			cadet.Prenom = cadetModel.Prenom;
			cadet.Section = section;
			cadet.CadetID = data.GetNextCadetID();

			data.Cadets.Add(cadet);

			return cadet;
		}

		public Cadet CadetEdit(CadetListItem cadetModel)
		{
			Cadet cadet = CadetGetByID(cadetModel.CadetID);
			Section section = SectionGetByID(cadetModel.Section.SectionID);
			Grade grade = GradeGetByID(cadetModel.Grade.GradeID);

			cadet.NbBilletsDistribue = cadetModel.NbBilletsDistribue;
			cadet.NbBilletsVendu = cadetModel.NbBilletsVendu;
			cadet.Nom = cadetModel.Nom;
			cadet.Prenom = cadetModel.Prenom;

			if(cadet.Section != null)
				cadet.Section.Cadets.Remove(cadet);
			if(cadet.Grade != null)
				cadet.Grade.Cadets.Remove(cadet);

			cadet.Grade = grade;
			cadet.Section = section;

			if (section != null)
				section.Cadets.Add(cadet);
			if (grade != null)
				grade.Cadets.Add(cadet);

			return cadet;
		}

		public void CadetDelete(int id)
		{
			Cadet cadet = CadetGetByID(id);

			if(cadet.Section != null)
				cadet.Section.Cadets.Remove(cadet);

			if(cadet.Grade != null)
				cadet.Grade.Cadets.Remove(cadet);

			data.Cadets.Remove(cadet);
		}

		#endregion

		#region Section

		public Section SectionGetByID(int id)
		{
			return data.Sections.FirstOrDefault(s => s.SectionID == id);
		}

		public List<Section> SectionGetAll()
		{
			List<Section> sections = new List<Section>();
			sections.AddRange(data.Sections);
			return sections;
		}

		public Section SectionAdd(string name)
		{
			Section section = new Section();
			section.Nom = name;
			section.SectionID = data.GetNextSectionID();

			data.Sections.Add(section);

			return section;
		}

		public Section SectionEdit(SectionListItem sectionModel)
		{
			Section section = SectionGetByID(sectionModel.SectionID);
			section.Nom = sectionModel.Nom;

			return section;
		}

		public void SectionDelete(int id)
		{
			Section section = SectionGetByID(id);

			foreach (Cadet cadet in section.Cadets)
			{
				cadet.Section = null;
			}

			data.Sections.Remove(section);
		}

		#endregion

		#region Grade

		public Grade GradeGetByID(int id)
		{
			return data.Grades.FirstOrDefault(g => g.GradeID == id);
		}

		public List<Grade> GradeGetAll()
		{
			List<Grade> grades = new List<Grade>();
			grades.AddRange(data.Grades);
			return grades;
		}

		public Grade GradeAdd(GradeListItem gradeModel)
		{
			Grade grade = new Grade();
			grade.Nom = gradeModel.Nom;
			grade.Abreviation = gradeModel.Abreviation;
			grade.GradeID = data.GetNextGradeID();

			data.Grades.Add(grade);

			return grade;
		}

		public Grade GradeEdit(GradeListItem gradeModel)
		{
			Grade grade = GradeGetByID(gradeModel.GradeID);

			grade.Nom = gradeModel.Nom;
			grade.Abreviation = gradeModel.Abreviation;

			return grade;
		}

		public List<Grade> GradeEditOrder(GradeList gradeList)
		{
			List<Grade> results = new List<Grade>();
			foreach (GradeListItem gradeModel in gradeList)
			{
				Grade grade = GradeGetByID(gradeModel.GradeID);
				if (grade == null)
					continue;

				grade.Ordre = gradeModel.Ordre;
				results.Add(grade);
			}

			return results;
		}

		public void GradeDelete(int id)
		{
			Grade grade = GradeGetByID(id);

			foreach (Cadet cadet in grade.Cadets)
			{
				cadet.Grade = null;
			}

			data.Grades.Remove(grade);
		}

		private void InitializeGrades()
		{
			GradeAdd(new GradeListItem() { Nom = "Cadet", Abreviation = "Cdt" });
			GradeAdd(new GradeListItem() { Nom = "Cadet première classe", Abreviation = "LAC" });
			GradeAdd(new GradeListItem() { Nom = "Caporal", Abreviation = "Cpl" });
			GradeAdd(new GradeListItem() { Nom = "Caporal de section", Abreviation = "Cpl/s" });
			GradeAdd(new GradeListItem() { Nom = "Sergent", Abreviation = "Sgt" });
			GradeAdd(new GradeListItem() { Nom = "Sergent de section", Abreviation = "Sgt/s" });
			GradeAdd(new GradeListItem() { Nom = "Adjudant deuxième classe", Abreviation = "Adj2" });
			GradeAdd(new GradeListItem() { Nom = "Adjudant première classe", Abreviation = "Adj1" });
		}

		#endregion

		#region Leaderboard

		public IEnumerable<Cadet> LeaderboardGetTopTenSeller()
		{
			return CadetGetAll().Where(c => c.Section != null).OrderByDescending(c => c.NbBilletsVendu).Take(10);
		}

		public SectionLeaderboard LeaderboardGetSection()
		{
			return new SectionLeaderboard(data);
		}

		#endregion

		#region File

		public Settings SettingsGet()
		{
			Settings settings = new Settings()
			{
				NbBilletsCirculation = data.NbBilletsCirculation,
				Nom = data.Nom
			};

			return settings;
		}

		public void SettingsUpdate(Settings settings)
		{
			data.Nom = settings.Nom;
			data.NbBilletsCirculation = settings.NbBilletsCirculation;
			Save();

			AddToRecentFiles();
		}

		public void FileOpen(string path)
		{
			if (!File.Exists(path))
				throw new FileNotFoundException();

			escadronManager.Dispose();
			escadronManager = new Repository<EscadronConfiguration>(path);
			data = escadronManager.GetData();

			AddToRecentFiles();
		}

		public void FileCreate(string path, string saveName = null)
		{
			escadronManager.Dispose();

			if (File.Exists(path))
				File.Delete(path);

			escadronManager = new Repository<EscadronConfiguration>(path, true);
			data = escadronManager.GetData();

			if (saveName != null)
			{
				data.Nom = saveName;
			}

			InitializeGrades();
			escadronManager.Save();

			AddToRecentFiles();
		}

		public List<FichierRecent> FileGetRecentlyOpened()
		{
			List<FichierRecent> recentFilesCol = new List<FichierRecent>();
			recentFilesCol.AddRange(recentFiles.GetRecentFiles());

			return recentFilesCol;
		}

		#endregion

		#region Recent Files

		private void AddToRecentFiles()
		{
			FichierRecent recent = new FichierRecent();
			recent.CheminFichier = escadronManager.Path;
			recent.NomSauvegarde = data.Nom;

			recentFiles.Add(recent);
			recentFilesManager.Save();
		}

		#endregion

		#region IDisposable

		private bool disposed = false;
		protected virtual void Dispose(bool disposing)
		{
			if (!this.disposed)
			{
				if (disposing)
				{
					escadronManager.Dispose();
					recentFilesManager.Dispose();
				}
			}
			this.disposed = true;
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}
		#endregion


	}
}
