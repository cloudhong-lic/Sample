using System.Threading.Tasks;
using Newtonsoft.Json;
using Ninject.Extensions.Logging;
using Sample.Domain.Models;
using Sample.Domain.Repositories;

namespace Sample.Handlers
{
	public class Handler1
	{
		private readonly IAnimalRepository _animalRepository;
		private readonly ILogger _logger;

		public Handler1(IAnimalRepository animalRepository, ILogger logger)
		{
			_animalRepository = animalRepository;
			_logger = logger;
		}

		public async Task<Animal> GetAnimalByAnimalKey(int animalKey)
		{
			var animal = await _animalRepository.Get(animalKey).ConfigureAwait(false);
			_logger.Info($"Object: {JsonConvert.SerializeObject(animal)}");

			return animal;
		}
	}
}