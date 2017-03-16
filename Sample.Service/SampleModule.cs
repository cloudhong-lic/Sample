using Ninject.Modules;
using NLog;
using Sample.Service.Handlers;

namespace Sample.Service
{
	internal class SampleModule : NinjectModule
	{
		private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

		public override void Load()
		{
			_logger.Info("Setting up bindings");

			Bind<ISampleHandle>().To<SampleHandler>();
		}
	}
}
