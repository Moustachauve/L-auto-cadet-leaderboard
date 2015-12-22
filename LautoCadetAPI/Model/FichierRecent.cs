using LautoCadetAPI.Validation;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LautoCadetAPI.Model
{
	[JsonObject(IsReference = true)]
	public class FichierRecent
	{
		[Required]
		public string NomSauvegarde { get; set; }

		[Required]
		[ValidSavePath]
		public string CheminFichier { get; set; }
	}
}
