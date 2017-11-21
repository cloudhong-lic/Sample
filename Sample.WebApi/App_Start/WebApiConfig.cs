using System.Configuration;
using System.Web.Http;
using System.Web.Http.Cors;
using Library.WebApi.Jwt.Filters;
using Library.WebApi.Jwt.Helpers;

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
			// 在Controller中指定Router, 而不是在这里通过AddRoutes进行统一管理, 这样更灵活可读
			config.MapHttpAttributeRoutes();
			//AddRoutes(config);

			// Web API filters
			// 添加各种Filters, 例如Authorization管理
			config.Filters.Add(AuthorizeUsingJwtAttribute());
		}

		//private static void AddRoutes(HttpConfiguration config)
		//{
		//	config.Routes.MapHttpRoute(
		//		name: "Default",
		//		routeTemplate: "api/{controller}/{action}",
		//		defaults: new { controller = "", action = "Get" }
		//	);
		//}

		private static void EnableCrossSiteRequests(HttpConfiguration config)
		{
			var cors = new EnableCorsAttribute(
				origins: "*",
				headers: "*",
				methods: "*");

			config.EnableCors(cors);
		}

		private static AuthorizeUsingJwtFilter AuthorizeUsingJwtAttribute()
		{
			// 这个是Authorization的Public key, 可以在这里写死, 也可以在Web.config中配置
			// 然后通过ConfigurationManager.AppSettings["PublicKey"]获取
			const string publicKeyXml = @"<RSAKeyValue><Modulus>4+lL6bZqN7ttVLOWfkHJZjUiq0KTNrmzGo+pM/3xPhSIjpXXslM70ucAPZNzIkvj1xNYLFOcQ5lVSO4R2EVnQL2YjExkt3cV2AGJ6a0y+P8Lm+mP/ersa3D6BysbRiK0Ldo6hoRRnIrQ9Z1pnRLT43nv934XbgLhIgrYzp6ordU=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";

			return AuthorizationHelper.GetAuthorizeUsingJwtFilter(publicKeyXml);
		}
	}
}
