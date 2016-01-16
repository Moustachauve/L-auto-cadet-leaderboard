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
	public class LeaderboardController : ApiController
	{
		IService service = Service.Instance;

		public IHttpActionResult GetTopTenSeller()
		{
            var list = new CadetList(service.LeaderboardGetTopTenSeller());

			return Json(list);
		}

		public IHttpActionResult GetSectionLeaderboard()
		{
			return Json(service.LeaderboardGetSection());
		}
	}
}
