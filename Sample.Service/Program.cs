using Sample.Datastore;
using Topshelf;
using Topshelf.Ninject;

namespace Sample.Service
{
	class Program
	{
		static void Main(string[] args)
		{
			HostFactory.Run(hf =>
			{
				hf.UseNinject(
					new SampleDatastoreModule(),
					new SampleModule()
				);
				hf.Service<SampleService>(svc =>
				{
					svc.ConstructUsingNinject();
					svc.WhenStarted((service, control) => service.Start(control));
					svc.WhenStopped(ais => ais.Stop());
				});

				// Set up dependencies
				hf.UseNLog();
				hf.DependsOnMsmq();

				// Set up identification strings
				hf.SetDescription("Sample Service to handle messages");
				hf.SetDisplayName("Sample - Service");
				hf.SetServiceName("Sample.Service");
			});
		}
	}
}