using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Ninject;
using Ninject.Extensions.Logging;
using Sample.Datastore;
using Sample.Domain.Models;
using Sample.Handlers;

namespace Sample.Testing.Library.EntityFramework
{
	[TestClass]
	public class UnitTest
	{
		private readonly Mock<ILogger> _logger = new Mock<ILogger>();
		private readonly DateTimeOffset _date = DateTimeOffset.Now;
		private Handler1 _handler1;

		[TestInitialize]
		public void Init()
		{
			var kernel = new StandardKernel(new SampleDatastoreModule());

			// Rebind
			kernel.Rebind<ISampleContext>().ToConstant(new MockSampleContext());
			kernel.Rebind<ILogger>().ToConstant(_logger.Object);

			// 生成一些数据
			var data = new List<Animal>
				{
					new Animal { AnimalKey = 1, BirthDate = _date, Sex = Sex.Female, Species = Species.Cattle },
					new Animal { AnimalKey = 2, BirthDate = _date.AddYears(1), Sex = Sex.Male, Species = Species.Deer },
					new Animal { AnimalKey = 3 }
				};
			data.ForEach(x => kernel.Get<ISampleContext>().Animals.Attach(x));

			// new一个新的Handler做测试之用
			_handler1 = kernel.Get<Handler1>();
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

			_logger.Verify(x => x.Info(It.Is<string>(s => s.StartsWith("Object:"))), Times.Once);
		}
	}
}
