using System;
using System.Net.Http;
using System.Web.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sample.TestFramework.Steps;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace Sample.WebApi.Tests.Steps
{
	[Binding]
	internal class CommonSteps
	{
		private readonly ScenarioContext _context;

		public CommonSteps(ScenarioContext context)
		{
			_context = context;
		}

		[BeforeScenario]
		public void Init()
		{
			// 调用TestFramework中的SharedSteps, 这个是用于比较复杂的项目
			// 对于简单的unit test项目, 可以将Shared step中的内容直接放到这里
			SharedSteps.Init(_context);

			// TODO: 对于需要mock kernel的情况可以考虑使用MoqMockingKernel
			// ITOPS-ANIMAL的unit test使用这种技术
		}

		[Then(@"the request is successful")]
		public void ThenTheRequestIsSuccessful()
		{
			var exception = _context.Get<Exception>();
			Assert.IsNull(exception);
		}

		[Then(@"the request is not successful")]
		public void ThenTheRequestIsNotSuccessful(Table table)
		{
			var exceptedException = table.CreateInstance<HttpResponseMessage>();
			var exception = (HttpResponseException)_context.Get<Exception>().InnerException;

			Assert.AreEqual(exceptedException.StatusCode, exception.Response.StatusCode);
			Assert.AreEqual(exceptedException.ReasonPhrase, exception.Response.ReasonPhrase);
		}
	}
}