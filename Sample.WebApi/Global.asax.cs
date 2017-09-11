using System.Configuration;
using System.Reflection;
using System.Web;
using System.Web.Http;
using Library.WebApi.Handlers;
using Newtonsoft.Json;
using NLog;
using Sample.WebApi.App_Start;

namespace Sample.WebApi
{
	public class WebApiApplication : HttpApplication
	{
		protected void Application_Start()
		{
			GlobalConfiguration.Configure(WebApiConfig.Register);

			// Allow cross scripting OPTIONS requests by default
			var corsUrls = ConfigurationManager.AppSettings["WebApiCorsSites"].Replace(" ", "").Split(',');
			GlobalConfiguration.Configuration.MessageHandlers.Add(new CorsHandler(corsUrls, allowCredentials: true));
			//GlobalConfiguration.Configuration.MessageHandlers.Add(new AuthenticationHandler());

			// Use XML serializer, not data-contract serializer
			GlobalConfiguration.Configuration.Formatters.XmlFormatter.UseXmlSerializer = true;

			// Use JSON.NET settings so backing fields are ignored when serializable attribute exists
			GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings = new JsonSerializerSettings();

			// Allow cross scripting OPTIONS requests by default
			GlobalConfiguration.Configuration.MessageHandlers.Add(new CorsHandler());

//			GlobalConfiguration.Configuration.Filters.Add(new ValidateModelAttribute());

			GlobalConfiguration.Configuration.EnsureInitialized();

			// 也可以不在这里加log, 个人觉得没有太大必要
			VerifyLogging();
		}

		private void VerifyLogging()
		{
			var version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
			LogManager.GetCurrentClassLogger().Info("Application started - Version: " + version);
		}
	}
}
