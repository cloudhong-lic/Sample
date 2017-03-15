using Ninject.Modules;

namespace Sample.Datastore
{
	public class SampleDatastoreModule : NinjectModule
	{
		public override void Load()
		{
			Bind<ISampleContext>().To<SampleContext>();
		}
	}
}