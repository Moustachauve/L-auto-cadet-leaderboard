using LautoCadetAPI.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LautoCadetAPI.Repository
{
	internal class EscadronRepository
	{
		public const string DEFAULT_FILE_PATH = @"sauvegarde\escadron.cadet";
		private EscadronConfiguration escadronConfiguration;
		private string savePath;

        public EscadronConfiguration EscadronConfiguration { get { return escadronConfiguration; } }

		/// <summary>
		/// Create a repository using the default file
		/// </summary>
		public EscadronRepository() : this(DEFAULT_FILE_PATH)
		{ }
		

		/// <summary>
		/// Create a repository using a custom file path
		/// </summary>
		public EscadronRepository(string path)
		{
			savePath = path;
			string directory = Path.GetDirectoryName(path);
            if (!Directory.Exists(directory))
				Directory.CreateDirectory(directory);

			Load();
		}

		public void Load()
		{
			if(File.Exists(savePath))
			{
				using (StreamReader reader = new StreamReader(savePath))
				{
					escadronConfiguration = JsonConvert.DeserializeObject<EscadronConfiguration>(reader.ReadToEnd());
				}
			}
			else
			{
				escadronConfiguration = new EscadronConfiguration();
				Save();
            }
		}

		public void Save()
		{
			using (StreamWriter writer = new StreamWriter(savePath))
			{
				writer.Write(JsonConvert.SerializeObject(escadronConfiguration, Formatting.Indented));
			}
		}
	}
}
