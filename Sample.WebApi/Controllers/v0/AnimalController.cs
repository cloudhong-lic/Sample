using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using NLog;
using Sample.Domain.Models;
using Sample.Domain.Repositories;

namespace Sample.WebApi.Controllers.v0
{
	[RoutePrefix("v0/Animal")]
	public class AnimalController : ApiController
	{
		private static readonly Logger _log = LogManager.GetCurrentClassLogger();
		private readonly IAnimalRepository _animalRepository;

		public AnimalController(IAnimalRepository animalRepository)
		{
			_animalRepository = animalRepository;
		}

		[HttpGet]
		[Route("{animalKey}")]
		//[WebApiCache(365 * 24 * 60 * 60, Private = false)]
		public async Task<Animal> Get(int animalKey)
		{
			try
			{
				var animal = await _animalRepository.Get(animalKey).ConfigureAwait(false);
				if (animal == null)
				{
					throw new HttpResponseException(HttpStatusCode.BadRequest);
				}

				return animal;
			}
			catch (Exception e)
			{
				_log.Error(e, $"Failed to retrieve animal {animalKey}");
				throw new HttpResponseException(HttpStatusCode.InternalServerError);
			}
		}
	}
}