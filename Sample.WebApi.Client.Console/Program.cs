using Library.WebApi.Helpers;
using NLog;
using Sample.WebApi.Client.Interfaces.v0;
using Sample.WebApi.Client.v0;

namespace Sample.WebApi.Client.Console
{
	internal class Program
	{
		private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

		private static void Main(string[] args)
		{
			IAnimalsProvider animalsProvider = new AnimalsProvider(new HttpServiceHelper());

			_logger.Info($"Getting animal");

			var animal = animalsProvider.Get(1).Result;

			_logger.Info($"Get animal: {animal.AnimalKey}");

			// Log等级排序, 从低到高
			_logger.Trace("Trace");
			_logger.Debug("Debug");
			_logger.Info("Info");
			_logger.Warn("Warn");
			_logger.Error("Error");
			_logger.Fatal("Fatal");
		}
	}
}