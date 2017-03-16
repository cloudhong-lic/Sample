using System.Data.Entity;
using Sample.Datastore.Mappings;
using Sample.Domain.Models;

namespace Sample.Datastore
{
	public class SampleContext : DbContext, ISampleContext
	{
		public IDbSet<Animal> Animals { get; set; }

		public SampleContext() : base("name=SampleDB")
		{
			Configuration.LazyLoadingEnabled = false;
			Database.SetInitializer<SampleContext>(null);
		}

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.HasDefaultSchema("Sample");

			modelBuilder.Configurations.Add(new AnimalMappings());
		}
	}
}