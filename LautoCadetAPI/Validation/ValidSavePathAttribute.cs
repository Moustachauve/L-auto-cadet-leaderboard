using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LautoCadetAPI.Validation
{
	public class ValidSavePathAttribute : ValidationAttribute
	{
		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			string path = (string)value;

			try {
				new FileInfo(path);
			}
			catch(ArgumentException)
			{
				return new ValidationResult("Le chemin \"" + path + "\" est invalide");
			}

			return ValidationResult.Success;
		}
	}
}
