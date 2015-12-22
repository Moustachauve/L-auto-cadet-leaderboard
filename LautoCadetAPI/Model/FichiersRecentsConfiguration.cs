using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LautoCadetAPI.Model
{
	[JsonObject(IsReference = true)]
	public class FichiersRecentsConfiguration
	{
		private const int MAX_FICHIERS_RECENTS = 6;

		[JsonProperty]
		private LinkedList<FichierRecent> recentFiles = new LinkedList<FichierRecent>();

		public FichiersRecentsConfiguration()
		{}

		public List<FichierRecent> GetRecentFiles()
		{
			return recentFiles.ToList();
		}

		public void Add(FichierRecent fichierRecent)
		{
			FichierRecent existing = recentFiles.FirstOrDefault(f => f.CheminFichier == fichierRecent.CheminFichier);
			if (existing != null)
			{
				recentFiles.Remove(existing);
			}
			else if(recentFiles.Count >= MAX_FICHIERS_RECENTS)
			{
				recentFiles.RemoveLast();
			}

			recentFiles.AddFirst(fichierRecent);
		}
	}
}
