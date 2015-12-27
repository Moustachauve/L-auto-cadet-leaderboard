using LautoCadetAPI.DAL;
using LautoCadetAPI.DTO;
using LautoCadetAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace LautoCadetAPI.Controllers
{
	public class SaveController : ApiController
	{
		Service service = Service.Instance;

		[HttpGet]
		public IHttpActionResult Details()
		{
			SaveDetails details = new SaveDetails();

			details.Nom = service.GetSaveName();
			details.FichiersRecents = service.RecentFilesGetAll();

			return Json(details);
		}

		[HttpPost]
		public IHttpActionResult Create(FichierRecent fileInfo)
		{
			if(!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			service.Create(fileInfo.CheminFichier, fileInfo.NomSauvegarde);

			return Json(fileInfo);
		}

		[HttpPost]
		public IHttpActionResult Open(FichierRecent fileInfo)
		{
			service.Open(fileInfo.CheminFichier);
			fileInfo.NomSauvegarde = service.GetSaveName();

			return Json(fileInfo);
		}

		[HttpPost]
		public IHttpActionResult Save([FromBody]string nomSauvegarde)
		{
			if (string.IsNullOrWhiteSpace(nomSauvegarde))
				return BadRequest("Le nom ne peut pas être vide");

			service.SetSaveName(nomSauvegarde);
			return Ok();
		}
	}
}
