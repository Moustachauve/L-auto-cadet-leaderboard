using LautoCadetAPI.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LautoCadetAPI.Model
{
	public class FichierRecent
	{
		[Required]
		public string NomSauvegarde { get; set; }

		[Required]
		[ValidSavePath]
		public string CheminFichier { get; set; }
	}
}
