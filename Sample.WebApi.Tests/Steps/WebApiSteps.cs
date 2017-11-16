using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Ninject;
using Sample.Domain.Repositories;
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

		private readonly Mock<IAnimalRepository> _animalRepository = new Mock<IAnimalRepository>();

		private AnimalsController _animalsController;

		public WebApiSteps(ScenarioContext context)
		{
			_context = context;
		}

		[When(@"I request animal (\d*)")]
		public void WhenIRequestAnimal(int animalKey)
		{
			// 从context中获取AnimalsController
			// 测试的数据从TestFrameWork项目中的DataSteps中获取, 如果多个测试项目共用同一份数据库, 这种方法可以共用代码
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
			// 使用CreateInstance来获取Table中的Object, 横表竖表都可以
			// 但table的结构必须和Object保持一致
			var expected = table.CreateInstance<Animal>();

			var animal = _context.GetResponse() as Animal;

			Assert.IsNotNull(animal);

			Assert.AreEqual(expected.AnimalKey, animal.AnimalKey);
			Assert.AreEqual(expected.BirthDate, animal.BirthDate);
			Assert.AreEqual(expected.Sex, animal.Sex);
			Assert.AreEqual(expected.Species, animal.Species);
		}

		// Step名称不能重复
		// the following animal exists in database已经在TestFrameWork项目中的DataSteps中使用过了
		[Given(@"the following animal exists in database - local")]
		public void GivenTheFollowingAnimalExistsInDatabase(Table table)
		{
			var animals = table.CreateSet<Domain.Models.Animal>().ToList();
			_context.SetAnimals(animals);
		}

		[When(@"I request following animals")]
		public void WhenIRequestFollowingAnimals(Table table)
		{
			// 测试数据为当前Mock
			// It.Is<>可以测试是否输入和期待值相符, 使用It.IsAny则忽略输入
			_animalRepository
				.Setup(x => x.GetBySqlQueryWithoutParameter(It.Is<int[]>(y => y.SequenceEqual(new int[] { 1, 2 }))))
				.ReturnsAsync(_context.GetAnimals());

			// 获取本地的AnimalController副本
			_animalsController = new AnimalsController(_animalRepository.Object);

			try
			{
				// 使用table.Rows去做循环, 可以解决全部table问题
				// 如果table的结构和Object不一致, 可以用这种方法产生一个临时表来处理
				var animalKeys = table.Rows.Select(row => int.Parse(row["AnimalKey"])).ToArray();

				var response = _animalsController.Post(animalKeys).Result;
				_context.SetResponse(response);

				// 测试内部某函数是否被调用, 以及被调用的次数
				_animalRepository.Verify(x => x.GetBySqlQueryWithoutParameter(It.Is<int[]>(y => y.SequenceEqual(new int[] { 1, 2 }))), Times.Once());
			}
			catch (Exception e)
			{
				_context.Set(e);
			}
		}

		[Then(@"the following animals are returned")]
		public void ThenTheFollowingAnimalsAreReturned(Table table)
		{
			// 使用CreateSet来获取Table中的List
			// 但table的结构必须和Object保持一致
			var expected = table.CreateSet<Animal>().ToList();

			var animals = _context.GetResponse() as List<Animal>;

			Assert.IsNotNull(animals);
			Assert.AreEqual(table.RowCount, animals.Count);

			for (var i = 0; i < animals.Count; i++)
			{
				// 添加message可以方便调试
				Assert.AreEqual(expected[i].AnimalKey, animals[i].AnimalKey, $"Expected animal key:{expected[i].AnimalKey} != Returned animal key:{animals[i].AnimalKey}");
				Assert.AreEqual(expected[i].BirthDate, animals[i].BirthDate);
				Assert.AreEqual(expected[i].Sex, animals[i].Sex);

				// 使用重载的AreEqual方法, 来简化message
				AreEqual(expected[i].Species, animals[i].Species, "Species");
			}
		}

		// 重载AreEqual方法, 来简化message
		// 其他Assert的方法也可以考虑重载, 应该讲这些重载放入另外一个公共的library中
		private void AreEqual<T>(T expected, T actual, string message)
		{
			Assert.AreEqual(expected, actual, $"Expected {message}:{expected} != Returned {message}:{actual}");
		}
	}
}