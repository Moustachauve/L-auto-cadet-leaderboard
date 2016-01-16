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

		public void GradeDelete(int id)
		{
			Grade grade = GradeGetByID(id);

			foreach (Cadet cadet in grade.Cadets)
			{
				cadet.Grade = null;
			}

			data.Grades.Remove(grade);
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

		public string FileGetSaveName()
		{
			return data.Nom;
		}

		public void FileSetSaveName(string nom)
		{
			data.Nom = nom;
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
			if (File.Exists(path))
				File.Delete(path);

			escadronManager.Dispose();
			escadronManager = new Repository<EscadronConfiguration>(path, true);
			data = escadronManager.GetData();

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
