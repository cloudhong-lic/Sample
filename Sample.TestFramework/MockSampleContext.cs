using System.Data.Entity;
using System.Threading.Tasks;
using Sample.ContextMocks;
using Sample.Datastore;
using Sample.Domain.Models;

namespace Sample.TestFramework
{
	public class MockSampleContext : FakeDbContext, ISampleContext
	{
		public IDbSet<Animal> Animals { get; set; }

		public Task<int> SaveChangesAsync()
		{
			return Task.FromResult(1);
		}
	}
}