using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Ninject.Extensions.Logging;
using Sample.Datastore;
using Sample.Datastore.Repositories;
using Sample.Domain.Models;
using Sample.Domain.Repositories;
using Sample.Handlers;

namespace Sample.Testing.MsTest
{
	[TestClass]
	public class UnitTest
	{
		private IAnimalRepository _animalRepository;
		private readonly Mock<ILogger> _logger = new Mock<ILogger>();

		private readonly DateTimeOffset _date = DateTimeOffset.Now;
		private Handler1 _handler1;

		[TestInitialize]
		public void Init()
		{
			// 生成一些数据
			var data = new List<Animal>
				{
					new Animal { AnimalKey = 1, BirthDate = _date, Sex = Sex.Female, Species = Species.Cattle },
					new Animal { AnimalKey = 2, BirthDate = _date.AddYears(1), Sex = Sex.Male, Species = Species.Deer },
					new Animal { AnimalKey = 3 }
				};

			// 将数据存入Fake数据库
			var set = new Mock<DbSet<Animal>>().SetupData(data);

			// Mock一个Context
			var context = new Mock<ISampleContext>();
			context.Setup(c => c.Animals).Returns(set.Object);

			// new一个新的Handler做测试之用
			_animalRepository = new AnimalRepository(context.Object);
			_handler1 = new Handler1(_animalRepository, _logger.Object);
		}

		[TestMethod]
		public async Task TestMethod()
		{
			// 调用被测对象
			var animal = await _handler1.GetAnimalByAnimalKey(1).ConfigureAwait(false);

			// Check the results
			Assert.IsNotNull(animal);
			Assert.AreEqual(1, animal.AnimalKey);
			Assert.AreEqual(_date, animal.BirthDate);
			Assert.AreEqual(Sex.Female, animal.Sex);
			Assert.AreEqual(Species.Cattle, animal.Species);
		}

		// TODO: 由于未知原因EntityFramework.Testing.Moq.Ninject不工作, 可能是由于Ninject的方法不对造成的
		[TestMethod]
		public async Task TestMethod2()
		{
			using (var kernel = new MoqMockingKernel())
			{
				kernel.Load(new EntityFrameworkTestingMoqModule(), new SampleDatastoreModule());

				var date = DateTimeOffset.Now;
				var data = new List<Animal>
				{
					new Animal { AnimalKey = 1, BirthDate = date, Sex = Sex.Female, Species = Species.Cattle },
					new Animal { AnimalKey = 2, BirthDate = date.AddYears(1), Sex = Sex.Male, Species = Species.Deer },
					new Animal { AnimalKey = 3 }
				};

				// Create a mock set and context
				var set = kernel.GetMock<DbSet<Animal>>().SetupData(data);

				// Setup mock set
				//var context = new Mock<ISampleContext>();
				var context = kernel.GetMock<SampleContext>();
				kernel.Rebind<ISampleContext>().ToConstant(context.Object);

				var handler1 = kernel.Get<Handler1>();
				var animal = await handler1.GetAnimalByAnimalKey(1).ConfigureAwait(false);

				// Check the results
				Assert.IsNotNull(animal);
				Assert.AreEqual(1, animal.AnimalKey);
				Assert.AreEqual(date, animal.BirthDate);
				Assert.AreEqual(Sex.Female, animal.Sex);
				Assert.AreEqual(Species.Cattle, animal.Species);
			}
		}
	}
}