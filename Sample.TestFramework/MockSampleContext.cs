using System.Data.Entity;
using System.Threading.Tasks;
using Sample.ContextMocks;
using Sample.Datastore;
using Sample.Domain.Models;

namespace Sample.TestFramework
{
	/// <summary>
	/// 虚拟Context, 可以在测试中建立一个假的在内存中运行的数据库
	/// FakeDbContext是重写了DbContext接口, 提供全套假的数据库
	/// </summary>
	public class MockSampleContext : FakeDbContext, ISampleContext
	{
		public IDbSet<Animal> Animals { get; set; }

		public Task<int> SaveChangesAsync()
		{
			return Task.FromResult(1);
		}
	}
}
