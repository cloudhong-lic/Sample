using Library.WebApi.Helpers;
using NLog;
using Sample.WebApi.Client.Interfaces.v0;
using Sample.WebApi.Client.v0;

namespace Sample.WebApi.Client.Console
{
	internal class Program
	{
		private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

		private static void Main(string[] args)
		{
			Get();

			GetByAnimalKeys();
		}

		private static void Get()
		{
			IAnimalsProvider animalsProvider = new AnimalsProvider(new HttpServiceHelper());

			Logger.Info($"Getting animal");

			var animal = animalsProvider.Get(1).Result;

			Logger.Info($"Get animal: {animal.AnimalKey}");

			// Log等级排序, 从低到高
			Logger.Trace("Trace");
			Logger.Debug("Debug");
			Logger.Info("Info");
			Logger.Warn("Warn");
			Logger.Error("Error");
			Logger.Fatal("Fatal");
		}

		private static void GetByAnimalKeys()
		{
			IAnimalsProvider animalsProvider = new AnimalsProvider(new HttpServiceHelper());

			Logger.Info($"Getting animals by animal keys");

			int[] animalKeys = {1, 2};

			var animals = animalsProvider.GetByAnimalKeys(animalKeys).Result;

			Logger.Info($"Get animal by animal keys:");

			foreach (var animal in animals)
				Logger.Info($"AnimalKey: {animal.AnimalKey}");
		}
	}
}