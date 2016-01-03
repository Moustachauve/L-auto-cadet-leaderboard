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
		Service service = Service.Instance;

		public IHttpActionResult GetAll()
		{
			var gradeModels = service.GradeGetAll();
			var grades = new List<GradeListItem>();

			foreach (Grade grade in gradeModels)
			{
				grades.Add(new GradeListItem(grade));
			}

			return Json(grades);
		}

		[HttpPost]
		public IHttpActionResult Add(GradeListItem gradeModel)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			GradeListItem grade = new GradeListItem(service.GradeAdd(gradeModel));

			return Json(grade);
		}

		[HttpGet]
		public IHttpActionResult Edit(int id)
		{
			var grade = service.GetGradeByID(id);

			if (grade == null)
			{
				return BadRequest("Aucun grade trouvé avec cet ID");
			}

			return Json(new GradeListItem(grade));
		}

		[HttpPut]
		public IHttpActionResult Edit(GradeListItem grade)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			return Json(new GradeListItem(service.GradeEdit(grade)));
		}

		[HttpDelete]
		public IHttpActionResult Delete(int id)
		{
			if (service.GradeDelete(id))
				return Json("Done");

			return BadRequest("Aucun grade avec cet ID");
		}
	}
}
