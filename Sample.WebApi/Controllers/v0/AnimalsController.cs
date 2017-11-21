using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using Library.WebApi.Filters;
using NLog;
using Sample.Domain.Repositories;
using Sample.WebApi.Contract.v0;
using Sample.WebApi.Mapping.v0;

namespace Sample.WebApi.Controllers.v0
{
	/// <summary>
	/// 在此进行Route map, 比较灵活, 每个controller可以单独配置
	/// </summary>
	[RoutePrefix("v0/Animals")]
	public class AnimalsController : ApiController
	{
		// log应该使用static readonly类型
		private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

		// 在此引用Domain中的Interface, 而不是直接调用Datastore中的context
		// 这是一种良好的设计模式, 所有的模块围绕着Domain, 但却不互相调用
		private readonly IAnimalRepository _animalRepository;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="animalRepository"></param>
		public AnimalsController(IAnimalRepository animalRepository)
		{
			_animalRepository = animalRepository;
		}

		/// <summary>
		/// 这个注释会显示在Swagger界面右边
		/// WebApiCache可以让cookie在浏览器中保存指定时间
		/// </summary>
		/// <remarks>
		/// 这个注释会显示在Swagger界面左边, Implement Notes下面
		/// </remarks>
		/// <param name="animalKey">animal key 这个会显示在参数旁边</param>
		/// <returns></returns>
		/// <exception cref="HttpResponseException"></exception>
		/// <response code="400">Bad request</response>
		/// <response code="500">Internal Server Error</response>
		[HttpGet]
		[Route("{animalKey}")]
		[WebApiCache(365 * 24 * 60 * 60, Private = false)]
		[EnableCors("*", "*", "*")]
		public async Task<Animal> Get(int animalKey)
		{
			return await GetAnimal(animalKey);
		}

		/// <summary>
		/// 需要Authorization的Api
		/// </summary>
		/// <remarks>
		/// Authorization access token: eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkNsb3VkIEhvbmciLCJhZG1pbiI6dHJ1ZSwiaXNzIjoid3d3Lmdvb2dsZS5jb20ifQ.c1cVQjrhBZkH5kfMwQ6pHgrMS789JDBH2fezC4dyBf7MHdPSPRXgof6Ke8Kt58BO_q3a61_oCK72aJkmsP7gkH3KRibLe-jj6lBRvp3YSI-0pRUEFROf5Y3LV9MmlRzB_5UeHm3Br0Tjw54Acm28fqRdIu8__kc8Waow-B7Ld_c
		/// 
		/// -----BEGIN RSA PRIVATE KEY-----
		/// MIICXgIBAAKBgQDj6Uvptmo3u21Us5Z+QclmNSKrQpM2ubMaj6kz/fE+FIiOldey
		/// UzvS5wA9k3MiS+PXE1gsU5xDmVVI7hHYRWdAvZiMTGS3dxXYAYnprTL4/wub6Y/9
		/// 6uxrcPoHKxtGIrQt2jqGhFGcitD1nWmdEtPjee/3fhduAuEiCtjOnqit1QIDAQAB
		/// AoGBAJdT4YbWCyLkPQzfjY5ZqhtGLrXeJ5dPp/974hJWi+b3hVB/Z8/M+kzn+r3n
		/// +KuODkNRYdtUzM4JspoRESIzuwD9tqAqhykSTP0gIvLKTYwJVx2+uSaCN+pyldgq
		/// tBoIxfb/t+nOfb7lTQhYP8ZWXWkdBsZcZvpgTOY3/GAGMGeFAkEA+SCDpYwczt9+
		/// LEDXbEWhcpkSVdc6+9Oq7/A7jah5nvVbwh6UyR0T0UepYOxPL2Wo9Wj0LFyJQ4IW
		/// EEdZMZVetwJBAOoy8bgCyCveRgk4aricBlcHUxhid8Or6vuCaqRP9gyvRe7REuE3
		/// 1QoEPlC894lW4EN5OIq/EmFsbvqkJKSUS9MCQQCI46fS0GGH/uBKmrqEYOJsoNWl
		/// W2WquE0mGH/wv9FMWg+4Y6tnstWP2mukuVRte9PSPYBl29cExDcxbLMC/suTAkAF
		/// jLl/o8k8iOLd+xFEWKYpz8mfTU4LO/qwhRGj3SU2fbzJgPjSj3Ej8J/NZ/zxqzZb
		/// QvcdCpQT7O7gT51yrPTzAkEAt4SopUbbd1sRNQZjrHSN611ymNdYHf4RKP9LquOI
		/// gdmhejU/CBlp7CAQqui8H3VVJuvkO1BJhSTorxSW21vvwA==
		/// -----END RSA PRIVATE KEY-----
		/// 
		/// -----BEGIN PUBLIC KEY-----
		/// MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQDj6Uvptmo3u21Us5Z+QclmNSKr
		/// QpM2ubMaj6kz/fE+FIiOldeyUzvS5wA9k3MiS+PXE1gsU5xDmVVI7hHYRWdAvZiM
		/// TGS3dxXYAYnprTL4/wub6Y/96uxrcPoHKxtGIrQt2jqGhFGcitD1nWmdEtPjee/3
		/// fhduAuEiCtjOnqit1QIDAQAB
		/// -----END PUBLIC KEY-----
		/// </remarks>
		/// <param name="animalKey">animal key</param>
		/// <returns></returns>
		/// <exception cref="HttpResponseException"></exception>
		/// <response code="400">Bad request</response>
		/// <response code="500">Internal Server Error</response>
		[HttpGet]
		[Route("Authorization/{animalKey}")]
		[Authorize]
		public async Task<Animal> GetWithAuthorization(int animalKey)
		{
			return await GetAnimal(animalKey);
		}

		private async Task<Animal> GetAnimal(int animalKey)
		{
			try
			{
				var animal = await _animalRepository.Get(animalKey).ConfigureAwait(false);
				if (animal == null)
				{
					throw new HttpResponseException(HttpStatusCode.BadRequest);
				}

				return animal.ToContract();
			}
			catch (Exception e)
			{
				_logger.Error(e, $"Failed to retrieve animal {animalKey}");
				throw new HttpResponseException(HttpStatusCode.InternalServerError);
			}
		}

		/// <summary>
		/// 通过Sql Query获取数据
		/// </summary>
		/// <param name="animalKeys"></param>
		/// <returns></returns>
		/// <exception cref="HttpResponseException"></exception>
		[HttpPost]
		[Route("")]
		[WebApiCache(365 * 24 * 60 * 60, Private = false)]
		public async Task<List<Animal>> Post(int[] animalKeys)
		{
			try
			{
				var result = await _animalRepository.GetBySqlQueryWithoutParameter(animalKeys).ConfigureAwait(false);
				if (result == null)
				{
					throw new HttpResponseException(HttpStatusCode.BadRequest);
				}

				// 此处使用了linq语句, 而不是foreach
				// 其实两种等价, 可以替换使用
				return result.Select(animal => animal.ToContract()).ToList();
			}
			catch (Exception e)
			{
				_logger.Error(e, "Failed to retrieve animals");
				throw new HttpResponseException(HttpStatusCode.InternalServerError);
			}
		}

	}
}
