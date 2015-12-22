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
	}
}
