using Sample.Datastore;
using Topshelf;
using Topshelf.Ninject;

namespace Sample.Service
{
	class Program
	{
		static void Main(string[] args)
		{
			// Topshelf Service框架
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
				// 这些设置会在运行的Service中显示
				// TODO:如何让Service在Windows Services中运行
				hf.SetDescription("Sample Service to handle messages");
				hf.SetDisplayName("Sample - Service");
				hf.SetServiceName("Sample.Service");
			});
		}
	}
}
