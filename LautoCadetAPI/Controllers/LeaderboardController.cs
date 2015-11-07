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
	public class LeaderboardController : ApiController
	{
		Service service = Service.Instance;

		public IEnumerable<Cadet> GetTopTenSeller()
		{
            return service.GetTopTen();
		}
	}
}
