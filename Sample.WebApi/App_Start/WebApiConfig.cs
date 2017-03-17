using System.Web.Http;

namespace Sample.WebApi.App_Start
{
	/// <summary>
	/// 通过VS创建WebAPI项目时自动生成的文件
	/// </summary>
	public static class WebApiConfig
	{
		public static void Register(HttpConfiguration config)
		{
			// Web API configuration and services

			// Web API routes
			config.MapHttpAttributeRoutes();

//			config.Routes.MapHttpRoute(
//				"DefaultApi",
//				"api/{controller}/{id}",
//				new {id = RouteParameter.Optional}
//			);
		}
	}
}
