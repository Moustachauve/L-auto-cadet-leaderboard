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
		IService service = Service.Instance;

		[HttpGet]
		public IHttpActionResult Details()
		{
			SaveDetails details = new SaveDetails();

			details.Settings = service.SettingsGet();
			details.FichiersRecents = service.FileGetRecentlyOpened();

			return Json(details);
		}

		[HttpPost]
		public IHttpActionResult Create(FichierRecent fileInfo)
		{
			if(!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			service.FileCreate(fileInfo.CheminFichier, fileInfo.NomSauvegarde);

			return Json(fileInfo);
		}

		[HttpPost]
		public IHttpActionResult Open(FichierRecent fileInfo)
		{
			service.FileOpen(fileInfo.CheminFichier);
			fileInfo.NomSauvegarde = service.SettingsGet().Nom;

			return Json(fileInfo);
		}

		[HttpPost]
		public IHttpActionResult Save(Settings setting)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			service.SettingsUpdate(setting);
			return Ok();
		}
	}
}
