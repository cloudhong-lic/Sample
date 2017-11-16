using System.Web.Http;
using System.Web.Http.Cors;

namespace Sample.WebApi.App_Start
{
	/// <summary>
	/// 通过VS创建WebAPI项目时自动生成的文件
	/// </summary>
	public static class WebApiConfig
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="config"></param>
		public static void Register(HttpConfiguration config)
		{
			// 激活CORS, 这样就可以跨domain访问WEBAPI了
			EnableCrossSiteRequests(config);

			// Web API routes
			config.MapHttpAttributeRoutes();

			//AddRoutes(config);
		}

		private static void AddRoutes(HttpConfiguration config)
		{
			config.Routes.MapHttpRoute(
				name: "Default",
				routeTemplate: "api/{controller}/{action}",
				defaults: new { controller = "", action = "Get" }
			);
		}

		private static void EnableCrossSiteRequests(HttpConfiguration config)
		{
			var cors = new EnableCorsAttribute(
				origins: "*",
				headers: "*",
				methods: "*");

			config.EnableCors(cors);
		}
	}
}
