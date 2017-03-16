using System.Reflection;
using System.Web;
using System.Web.Http;
using NLog;
using Sample.WebApi.App_Start;

namespace Sample.WebApi
{
	public class WebApiApplication : HttpApplication
	{
		protected void Application_Start()
		{
			GlobalConfiguration.Configure(WebApiConfig.Register);

			VerifyLogging();
		}

		private void VerifyLogging()
		{
			var version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
			LogManager.GetCurrentClassLogger().Info("Application started - Version: " + version);
		}
	}
}