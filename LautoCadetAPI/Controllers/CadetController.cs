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
		IService service = Service.Instance;

		public IHttpActionResult Get(int id)
		{
			var cadet = service.CadetGetByID(id);

			if (cadet == null)
			{
				return BadRequest("Aucun cadet avec cet ID");
			}

			return Json(new CadetListItem(cadet));
		}

		public IHttpActionResult GetFormInit()
		{
			var sections = service.SectionGetAll().OrderBy(s => s.Nom).ToList();
			var grades = service.GradeGetAll().OrderBy(g => g.Ordre).ThenBy(g => g.Nom).ToList();

			return Json(new CadetFormInit(sections, grades));
		}

		public IHttpActionResult GetList()
		{
			var cadets = service.CadetGetAll();
			var grades = service.GradeGetAll();
			var sections = service.SectionGetAll();

			var result = new CadetListData(cadets, sections, grades);

			return Json(result);
		}

		[HttpPost]
		public IHttpActionResult Add(CadetListItem cadetModel)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			Cadet newCadet = service.CadetAdd(cadetModel);
			service.Save();

			return Json(new CadetListItem(newCadet));
		}

		[HttpPut]
		public IHttpActionResult Edit(CadetListItem cadetModel)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			Cadet cadet = service.CadetEdit(cadetModel);
			service.Save();

			return Json(new CadetListItem(cadet));
		}

		[HttpDelete]
		public IHttpActionResult Delete(int id)
		{
			service.CadetDelete(id);
			service.Save();

			return Ok();
		}
	}
}
