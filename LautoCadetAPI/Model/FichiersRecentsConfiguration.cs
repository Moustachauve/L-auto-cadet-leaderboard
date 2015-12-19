using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LautoCadetAPI.Model
{
	public class FichiersRecentsConfiguration
	{
		private Queue<FichierRecent> recentFiles;

		public FichiersRecentsConfiguration()
		{
			recentFiles = new Queue<FichierRecent>(10);
		}

		public List<FichierRecent> GetRecentFiles()
		{
			return recentFiles.ToList();
		}
	}
}
