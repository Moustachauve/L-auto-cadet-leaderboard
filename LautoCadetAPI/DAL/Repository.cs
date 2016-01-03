using LautoCadetAPI.DTO;
using LautoCadetAPI.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LautoCadetAPI.DAL
{
	internal class Repository
	{

		#region Variables & Constructor

		private FichiersRecentsConfiguration fichiersRecentsConfiguration;
		private EscadronConfiguration escadronConfiguration;
		private string savePath;

		public EscadronConfiguration EscadronConfiguration { get { return escadronConfiguration; } }

		/// <summary>
		/// Create a repository using the default file
		/// </summary>
		public Repository()
			: this(WebApi.DEFAULT_FILE_PATH)
		{ }


		/// <summary>
		/// Create a repository using a custom file path
		/// </summary>
		public Repository(string path)
		{
			SetSaveFile(path);
		}

		#endregion

		#region IO

		public void Load(bool overrideFile = false)
		{
			LoadFichiersRecents();

			if (!overrideFile && File.Exists(savePath))
			{
				using (StreamReader reader = new StreamReader(savePath))
				{
					escadronConfiguration = JsonConvert.DeserializeObject<EscadronConfiguration>(reader.ReadToEnd());
				}
			}
			else
			{
				escadronConfiguration = new EscadronConfiguration();
				CreerGradesDefaut();
			}

			FichierRecent fichierRecent = new FichierRecent();
			fichierRecent.CheminFichier = savePath;
			fichierRecent.NomSauvegarde = escadronConfiguration.Nom;

			fichiersRecentsConfiguration.Add(fichierRecent);
			SaveFichiersRecents();
		}

		private void LoadFichiersRecents()
		{
			if (File.Exists(WebApi.DEFAULT_RECENT_FILES_PATH))
			{
				using (StreamReader reader = new StreamReader(WebApi.DEFAULT_RECENT_FILES_PATH))
				{
					fichiersRecentsConfiguration = JsonConvert.DeserializeObject<FichiersRecentsConfiguration>(reader.ReadToEnd());
				}
			}
			else
			{
				fichiersRecentsConfiguration = new FichiersRecentsConfiguration();
			}
		}

		public void Save()
		{
			string saveDirectory = Path.GetDirectoryName(savePath);
			if (!Directory.Exists(saveDirectory))
				Directory.CreateDirectory(saveDirectory);

			using (StreamWriter writer = new StreamWriter(savePath))
			{
				writer.Write(JsonConvert.SerializeObject(escadronConfiguration, Formatting.Indented));
			}
		}

		private void SaveFichiersRecents()
		{
			string saveDirectory = Path.GetDirectoryName(WebApi.DEFAULT_RECENT_FILES_PATH);
			if (!Directory.Exists(saveDirectory))
				Directory.CreateDirectory(saveDirectory);

			using (StreamWriter writer = new StreamWriter(WebApi.DEFAULT_RECENT_FILES_PATH))
			{
				writer.Write(JsonConvert.SerializeObject(fichiersRecentsConfiguration, Formatting.Indented));
			}
		}

		public void SetSaveFile(string path, bool overrideFile = false)
		{
			savePath = Path.GetFullPath(path);
			Load(overrideFile);
		}

		public List<FichierRecent> GetRecentFiles()
		{
			return fichiersRecentsConfiguration.GetRecentFiles();
		}

		#endregion

		#region Cadet

		public List<Cadet> GetAllCadets()
		{
			List<Cadet> list = new List<Cadet>();

			foreach (Section section in escadronConfiguration.Sections)
			{
				list.AddRange(section.Cadets);
			}

			return list;
		}

		public bool DeleteCadet(int cadetID)
		{
			foreach (Section section in escadronConfiguration.Sections)
			{
				foreach (Cadet cadet in section.Cadets)
				{
					if (cadet.CadetID == cadetID)
					{
						section.Cadets.Remove(cadet);
						Save();
						return true;
					}
				}
			}

			return false;
		}

		#endregion

		#region Section

		public List<Section> GetAllSections()
		{
			List<Section> list = new List<Section>();

			list.AddRange(escadronConfiguration.Sections);

			return list;
		}

		public bool SectionDelete(int sectionID)
		{
			foreach (Section section in escadronConfiguration.Sections)
			{
				if (section.SectionID == sectionID)
				{
					escadronConfiguration.Sections.Remove(section);
					Save();
					return true;
				}
			}

			return false;
		}

		#endregion

		#region Grade

		public List<Grade> GetAllGrades()
		{
			List<Grade> list = new List<Grade>();

			list.AddRange(escadronConfiguration.Grades);

			return list;
		}

		public Grade GetGradeByID(int gradeID)
		{
			return escadronConfiguration.Grades.First(g => g.GradeID == gradeID);
		}

		public Grade GradeAdd(GradeListItem gradeModel)
		{
			Grade grade = new Grade();
			grade.GradeID = escadronConfiguration.GetNextGradeID();
			grade.Nom = gradeModel.Nom;
			grade.Abreviation = gradeModel.Abreviation;

			escadronConfiguration.Grades.Add(grade);

			return grade;
		}

		public bool GradeDelete(int gradeID)
		{
			foreach (Grade grade in escadronConfiguration.Grades)
			{
				if (grade.GradeID == gradeID)
				{
					escadronConfiguration.Grades.Remove(grade);

					Grade firstGrade = escadronConfiguration.Grades.First();
					foreach (Cadet cadet in grade.Cadets)
					{
						firstGrade.Cadets.Add(cadet);
						cadet.Grade = firstGrade;
					}

					return true;
				}
			}

			return false;
		}

		private void CreerGradesDefaut()
		{
			GradeAdd(new GradeListItem() { Nom = "Cadet",					 Abreviation = "Cdt" });
			GradeAdd(new GradeListItem() { Nom = "Cadet première classe",	 Abreviation = "LAC" });
			GradeAdd(new GradeListItem() { Nom = "Caporal",					 Abreviation = "Cpl" });
			GradeAdd(new GradeListItem() { Nom = "Caporal de section",		 Abreviation = "Cpl/s" });
			GradeAdd(new GradeListItem() { Nom = "Sergent",					 Abreviation = "Sgt" });
			GradeAdd(new GradeListItem() { Nom = "Sergent de section",		 Abreviation = "Sgt/s" });
			GradeAdd(new GradeListItem() { Nom = "Adjudant deuxième classe", Abreviation = "Adj2" });
			GradeAdd(new GradeListItem() { Nom = "Adjudant première classe", Abreviation = "Adj1" });
		}

		#endregion
	}
}
