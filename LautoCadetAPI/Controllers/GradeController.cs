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
	public class GradeController : ApiController
	{
		IService service = Service.Instance;

		public IHttpActionResult GetAll()
		{
			var grades = service.GradeGetAll().OrderBy(g => g.Ordre).ThenBy(g => g.Nom).ToList();

			return Json(new GradeList(grades));
		}

		[HttpPost]
		public IHttpActionResult Add(GradeListItem gradeModel)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			Grade grade = service.GradeAdd(gradeModel);
			service.Save();

			return Json(new GradeListItem(grade));
		}

		[HttpGet]
		public IHttpActionResult Edit(int id)
		{
			var grade = service.GradeGetByID(id);
			if (grade == null)
			{
				return BadRequest("Aucun grade trouvé avec cet ID");
			}

			return Json(new GradeListItem(grade));
		}

		[HttpPut]
		public IHttpActionResult Edit(GradeListItem gradeModel)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			Grade grade = service.GradeEdit(gradeModel);
			service.Save();

			return Json(new GradeListItem(grade));
		}

		[HttpPut]
		public IHttpActionResult EditOrder(IEnumerable<GradeListItem> gradeList)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			List<Grade> grade = service.GradeEditOrder(new GradeList(gradeList));
			service.Save();

			return Json(new GradeList(grade));
		}

		[HttpDelete]
		public IHttpActionResult Delete(int id)
		{
			service.GradeDelete(id);
			service.Save();

			return Ok();
		}
	}
}
