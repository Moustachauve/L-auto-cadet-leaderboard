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
				return BadRequest("Cadet not found");
			}

			return Json<CadetListItem>(new CadetListItem(cadet));
		}

		public IHttpActionResult GetAll()
		{
			var cadets = service.GetAllCadets();

			var result = new CadetList(cadets);

			return Json<IEnumerable<CadetListItem>>(result.OrderBy(c => c.DisplayName));
		}

		[HttpPost]
		public IHttpActionResult Add(CadetListItem cadetModel)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			return Json<Cadet>(service.AddCadet(cadetModel));
		}

		[HttpDelete]
		public IHttpActionResult Delete(int id)
		{
			if (service.DeleteCadet(id))
				return Json<String>("Done");

			return BadRequest("Cadet not found");
		}
	}
}
