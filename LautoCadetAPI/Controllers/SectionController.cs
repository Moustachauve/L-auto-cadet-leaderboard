﻿using LautoCadetAPI.DAL;
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
        Service service = Service.Instance;

		public IHttpActionResult Get(int id)
		{
			var section = service.GetSectionByID(id);

			if (section == null)
			{
				return BadRequest("Section not found");
			}

			return Json(new SectionDetails(section));
		}

		public IHttpActionResult GetAll()
        {
			var sections = service.GetAllSections();

			var result = new List<SectionListItem>();

			foreach (Section section in sections)
			{
				result.Add(new SectionListItem(section));
			}

			return Json(result.OrderBy(s => s.Nom));
        }

        [HttpPost]
        public IHttpActionResult Add(Section section)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

			SectionListItem result = new SectionListItem(service.AddSection(section.Nom));

			return Json(result);
        }

		[HttpGet]
		public IHttpActionResult Edit(int id)
		{
			var section = service.GetSectionByID(id);

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

			return Json(new SectionListItem(service.SectionEdit(sectionModel)));
		}

		[HttpDelete]
		public IHttpActionResult Delete(int id)
		{
			if (service.SectionDelete(id))
				return Json("Done");

			return BadRequest("Section not found");
		}
	}
}
