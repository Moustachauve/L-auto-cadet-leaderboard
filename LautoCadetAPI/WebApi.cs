using LautoCadetAPI.Repository;
using Microsoft.Owin.FileSystems;
using Microsoft.Owin.Hosting;
using Microsoft.Owin.StaticFiles;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace LautoCadetAPI
{
	public class WebApi
	{
		public const string API_URL = "http://localhost:8080";
		private static IDisposable webApp;
		private static EscadronRepository repository;

        internal static EscadronRepository Repository { get { return repository; } }

		public static void Start()
		{
			webApp = WebApp.Start<WebApi>(API_URL);

			repository = new EscadronRepository();
        }

		public static void Stop()
		{
			repository.Save();
			webApp.Dispose();
			webApp = null;
		}

		public void Configuration(IAppBuilder appBuilder)
		{
			// Configure Web API for self-host. 
			HttpConfiguration config = new HttpConfiguration();
			config.Routes.MapHttpRoute(
				name: "DefaultApi",
				routeTemplate: "api/{controller}/{id}",
				defaults: new { id = RouteParameter.Optional } 
			);

			appBuilder.UseWebApi(config);

			var physicalFileSystem = new PhysicalFileSystem(@"./www");
			var options = new FileServerOptions
			{
				EnableDefaultFiles = true,
				FileSystem = physicalFileSystem
			};
			options.StaticFileOptions.FileSystem = physicalFileSystem;
			options.StaticFileOptions.ServeUnknownFileTypes = true;
			options.DefaultFilesOptions.DefaultFileNames = new[]
			{
				"index.html"
			};

			appBuilder.UseFileServer(options);
		}

	}
}
