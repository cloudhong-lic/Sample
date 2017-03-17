using System;
using System.Web.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sample.TestFramework.Steps;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace Sample.WebApi.Tests.Steps
{
	[Binding]
	class CommonSteps
	{
		private readonly ScenarioContext _context;

		public CommonSteps(ScenarioContext context)
		{
			_context = context;
		}

		[BeforeScenario]
		public void Init()
		{
			SharedSteps.Init(_context);
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
			var exceptedException = table.CreateInstance<HttpResponseException>();
			var exception = (HttpResponseException)_context.Get<Exception>().InnerException;

			Assert.AreEqual(exceptedException.Response.StatusCode, exception.Response.StatusCode);
			Assert.AreEqual(exceptedException.Response.ReasonPhrase, exception.Response.ReasonPhrase);
		}
	}
}