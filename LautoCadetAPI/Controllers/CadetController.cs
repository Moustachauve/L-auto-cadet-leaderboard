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

		public IHttpActionResult GetAll()
		{
			var cadets = service.GetAllCadets();

			var result = new List<CadetListItem>();

			foreach (Cadet cadet in cadets)
			{
				result.Add(new CadetListItem(cadet));
			}

			return Json<IEnumerable<CadetListItem>>(result.OrderBy(c => c.DisplayName));
		}

        [HttpPost]
        public IHttpActionResult Add(Cadet cadet)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Json<Cadet>(service.AddCadet(cadet));
        }
    }
}
