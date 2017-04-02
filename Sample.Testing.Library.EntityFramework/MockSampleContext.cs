using System.Data.Entity;
using System.Threading.Tasks;
using Library.Testing.EntityFramework;
using Sample.Datastore;
using Sample.Domain.Models;

namespace Sample.Testing.Library.EntityFramework
{
	/// <summary>
	/// 虚拟Context, 可以在测试中建立一个假的在内存中运行的数据库
	/// FakeDbContext是自定义的Library中的接口, 重写了DbContext接口, 提供全套假的数据库
	/// 如果使用EntityFrameworkTesting.Moq就可以不需要建立这个Mock的Context的
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
