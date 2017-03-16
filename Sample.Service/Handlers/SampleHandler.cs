using System.Threading.Tasks;
using NLog;
using Sample.Domain.Models;
using Sample.Domain.Repositories;

namespace Sample.Service.Handlers
{
	public class SampleHandler : ISampleHandle
	{
		private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

		private readonly IAnimalRepository _animalRepository;

		public SampleHandler(IAnimalRepository animalRepository)
		{
			_animalRepository = animalRepository;
		}

		public async Task<Animal> Handle(int animalKey)
		{
			_logger.Info($"Try to get animal object by {animalKey}");

			return await _animalRepository.Get(animalKey).ConfigureAwait(false);
		}
	}
}