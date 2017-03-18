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
	}
}
