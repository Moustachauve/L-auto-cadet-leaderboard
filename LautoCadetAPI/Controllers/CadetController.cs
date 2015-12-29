using LautoCadetAPI.DAL;
using LautoCadetAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using LautoCadetAPI.DTO;

namespace LautoCadetAPI.Controllers
{
	public class CadetController : ApiController
	{
		Service service = Service.Instance;

		public IHttpActionResult Get(int id)
		{
			var cadet = service.GetCadetByID(id);

			if (cadet == null)
			{
				return BadRequest("Aucun cadet avec cet ID");
			}

			return Json(new CadetListItem(cadet));
		}

		public IHttpActionResult GetAll()
		{
			var cadets = service.GetAllCadets();

			var result = new CadetList(cadets)
							.OrderBy(c => c.Nom).ThenBy(c => c.Prenom).ThenBy(c => c.Grade);

			return Json(result);
		}

		[HttpPost]
		public IHttpActionResult Add(CadetListItem cadetModel)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			CadetListItem cadet = new CadetListItem(service.AddCadet(cadetModel));

			return Json(cadet);
		}

		[HttpPut]
		public IHttpActionResult Edit(CadetListItem cadetModel)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			CadetListItem cadet = new CadetListItem(service.EditCadet(cadetModel));

			return Json(cadet);
		}

		[HttpDelete]
		public IHttpActionResult Delete(int id)
		{
			if (service.DeleteCadet(id))
				return Json("Done");

			return BadRequest("Aucun cadet avec cet ID");
		}
	}
}
