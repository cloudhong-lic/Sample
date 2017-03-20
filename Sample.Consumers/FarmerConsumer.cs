using System.Threading.Tasks;
using Library.Logging;
using MassTransit;
using NLog;
using Sample.Domain.Repositories;
using Sample2.Messages.Events.v0;

namespace Sample.Consumers
{
	public class FarmerConsumer : IConsumer<INewFarmerEvent>
	{
		private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
		private readonly IAnimalRepository _animalRepository;

		public FarmerConsumer(IAnimalRepository animalRepository)
		{
			_animalRepository = animalRepository;
		}

		public async Task Consume(ConsumeContext<INewFarmerEvent> context)
		{
			var message = context.Message;
			_logger.Info(() => LoggingConvention.ForLogging("Received a INewFarmerEvent message", message));

			var animal = await _animalRepository.Get(message.Id).ConfigureAwait(false);
			_logger.Info(() => LoggingConvention.ForLogging("Get animal", animal));
		}
	}
}