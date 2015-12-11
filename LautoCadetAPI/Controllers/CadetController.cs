using LautoCadetAPI.DAL;
using LautoCadetAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace LautoCadetAPI.Controllers
{
    public class CadetController : ApiController
    {
        Service service = Service.Instance;

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
