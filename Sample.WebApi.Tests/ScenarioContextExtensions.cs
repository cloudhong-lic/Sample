using TechTalk.SpecFlow;

namespace Sample.WebApi.Tests
{
	public static class ScenarioContextExtensions
	{
		private const string Response = "Response";

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
