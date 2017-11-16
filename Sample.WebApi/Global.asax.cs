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
	/// <summary>
	/// 
	/// </summary>
	public class WebApiApplication : HttpApplication
	{
		/// <summary>
		/// 
		/// </summary>
		protected void Application_Start()
		{
			GlobalConfiguration.Configure(WebApiConfig.Register);

			// Allow cross scripting OPTIONS requests by default
			var corsUrls = ConfigurationManager.AppSettings["WebApiCorsSites"].Replace(" ", "").Split(',');
			GlobalConfiguration.Configuration.MessageHandlers.Add(new CorsHandler(corsUrls, allowCredentials: true));

			// Load all assemblies in bin directory into app domain
			// Directory.GetFiles(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "*.dll").Select(Assembly.LoadFile).ToList();

			// Use XML serializer, not data-contract serializer
			GlobalConfiguration.Configuration.Formatters.XmlFormatter.UseXmlSerializer = true;

			// Use JSON.NET settings so backing fields are ignored when serializable attribute exists
			GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings = new JsonSerializerSettings();

			// Allow cross scripting OPTIONS requests by default
			GlobalConfiguration.Configuration.MessageHandlers.Add(new CorsHandler());

			// 这两行本来是在App_Start/WebApiErrorLoggingConfig中昨晚错误处理filter使用的
			// GlobalConfiguration.Configuration.Filters.Add(new ValidateModelAttribute());
			// GlobalConfiguration.Configuration.MessageHandlers.Add(new AuthenticationHandler());

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
