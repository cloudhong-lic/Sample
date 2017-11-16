using System.Collections.Generic;
using Sample.Domain.Models;
using TechTalk.SpecFlow;

namespace Sample.WebApi.Tests
{
	/// <summary>
	/// 在单元测试时将某一Step的结果暂时存入SpecFlow的Context中
	/// 然后在其他Step中再取出使用, 非常方便
	/// 可以是object, 也可以指定类型
	/// </summary>
	public static class ScenarioContextExtensions
	{
		private const string Response = "Response";
		private const string Animals = "Animals";

		/// <summary>
		/// Response为WebApi中的请求结果, 类型暂时为object, 以后再转换
		/// </summary>
		public static object GetResponse(this ScenarioContext context)
		{
			return context[Response];
		}

		public static void SetResponse(this ScenarioContext context, object response)
		{
			context.Set(response, Response);
		}

		public static List<Animal> GetAnimals(this ScenarioContext context)
		{
			return context.ContainsKey(Animals) ? context[Animals] as List<Animal> : null;
		}

		public static void SetAnimals(this ScenarioContext context, List<Animal> animals)
		{
			context.Set(animals, Animals);
		}
	}
}
