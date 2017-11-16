﻿using System;
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
