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
