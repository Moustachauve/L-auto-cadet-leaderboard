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

			return Ok();
		}

		[HttpPost]
		public IHttpActionResult Open(FichierRecent fileInfo)
		{
			service.Open(fileInfo.CheminFichier);

			return Ok();
		}
	}
}
