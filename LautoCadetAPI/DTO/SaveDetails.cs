using LautoCadetAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LautoCadetAPI.DTO
{
	public class SaveDetails
	{
		public Settings Settings { get; set; }

		public List<FichierRecent> FichiersRecents { get; set; }
	}
}
