using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;

namespace LautoCadetAPI.Controllers
{
	public class HomeController : ApiController
	{
		[HttpGet]
		public HttpResponseMessage Index()
		{
			string html = "";
			using (StreamReader reader = new StreamReader("www/index.html"))
			{
				html = reader.ReadToEnd();
			}


			var response = new HttpResponseMessage();
			response.Content = new StringContent(html);
			response.Content.Headers.ContentType = new MediaTypeHeaderValue("text/html");

			return response;
		}
	}
}
