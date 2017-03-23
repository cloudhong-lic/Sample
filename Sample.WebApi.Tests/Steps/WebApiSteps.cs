using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using Sample.TestFramework.Steps.DataTable;
using Sample.WebApi.Contract.v0;
using Sample.WebApi.Controllers.v0;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace Sample.WebApi.Tests.Steps
{
	[Binding]
	public class WebApiSteps
	{
		private readonly ScenarioContext _context;

		public WebApiSteps(ScenarioContext context)
		{
			_context = context;
		}

		[When(@"I request animal (\d*)")]
		public void WhenIRequestAnimal(int animalKey)
		{
			var controller = _context.Get<IKernel>().Get<AnimalsController>();
			try
			{
				var response = controller.Get(animalKey).Result;
				_context.SetResponse(response);
			}
			catch (Exception e)
			{
				_context.Set(e);
			}
		}

		[Then(@"the following animal is returned")]
		public void ThenTheFollowingAnimalIsReturned(Table table)
		{
			var expected = table.CreateInstance<AnimalTable>().ToAnimal();
			var animal = _context.GetResponse() as Animal;
			Assert.IsNotNull(animal);

			Assert.AreEqual(expected.AnimalKey, animal.AnimalKey);
			Assert.AreEqual(expected.BirthDate, animal.BirthDate);
			Assert.AreEqual(expected.Sex, animal.Sex);
			Assert.AreEqual(expected.Species, animal.Species);
		}
	}
}