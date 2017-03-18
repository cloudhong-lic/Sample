using Ninject;
using Sample.Datastore;
using System.Linq;
using Sample.TestFramework.Steps.DataTable;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace Sample.TestFramework.Steps
{
	/// <summary>
	/// 全局测试步骤
	/// 可以将虚拟数据库等内容放在这里
	/// </summary>
	[Binding]
	public class DataSteps
	{
		private readonly ScenarioContext _context;

		public DataSteps(ScenarioContext context)
		{
			_context = context;
		}

		[Given(@"the following animal exists in database")]
		public void GivenTheFollowingAnimalExistsInDatabase(Table table)
		{
			var animals = table.CreateSet<AnimalTable>().Select(x => x.ToAnimal()).ToList();
			animals.ForEach(x => _context.Get<IKernel>().Get<ISampleContext>().Animals.Attach(x));
		}
	}
}
