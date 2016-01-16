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
    public class SectionController : ApiController
    {
        IService service = Service.Instance;

		public IHttpActionResult Get(int id)
		{
			var section = service.SectionGetByID(id);
			if (section == null)
			{
				return BadRequest("Section not found");
			}

			return Json(new SectionDetails(section));
		}

		public IHttpActionResult GetAll()
        {
			var sections = service.SectionGetAll();

			var result = new List<SectionListItem>();

			foreach (Section section in sections)
			{
				result.Add(new SectionListItem(section));
			}

			return Json(result.OrderBy(s => s.Nom));
        }

        [HttpPost]
        public IHttpActionResult Add(Section sectionModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

			Section section = service.SectionAdd(sectionModel.Nom);
			service.Save();

			return Json(new SectionListItem(section));
        }

		[HttpGet]
		public IHttpActionResult Edit(int id)
		{
			var section = service.SectionGetByID(id);
			if (section == null)
			{
				return BadRequest("Section not found");
			}

			return Json(new SectionListItem(section));
		}

		[HttpPut]
		public IHttpActionResult Edit(SectionListItem sectionModel)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			Section section = service.SectionEdit(sectionModel);
			service.Save();

			return Json(new SectionListItem(section));
		}

		[HttpDelete]
		public IHttpActionResult Delete(int id)
		{
			service.SectionDelete(id);
			service.Save();

			return Ok();
		}
	}
}
