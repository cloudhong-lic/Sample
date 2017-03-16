using Ninject.Modules;
using Sample.Datastore.Repositories;
using Sample.Domain.Repositories;

namespace Sample.Datastore
{
	public class SampleDatastoreModule : NinjectModule
	{
		public override void Load()
		{
			Bind<ISampleContext>().To<SampleContext>();

			Bind<IAnimalRepository>().To<AnimalRepository>();
		}
	}
}