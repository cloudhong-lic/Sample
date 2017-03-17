using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using NLog;
using Sample.Domain.Models;
using Sample.Domain.Repositories;

namespace Sample.WebApi.Controllers.v0
{
	// 在此进行Route map, 比较灵活, 每个controller可以单独配置
	[RoutePrefix("v0/Animal")]
	public class AnimalController : ApiController
	{
		// log应该使用static readonly类型
		private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

		// 在此引用Domain中的Interface, 而不是直接调用Datastore中的context
		// 这是一种良好的设计模式, 所有的模块围绕着Domain, 但却不互相调用
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
				_logger.Error(e, $"Failed to retrieve animal {animalKey}");
				throw new HttpResponseException(HttpStatusCode.InternalServerError);
			}
		}
	}
}
