using System;
using Ninject;
using Sample.Datastore;
using TechTalk.SpecFlow;

namespace Sample.TestFramework.Steps
{
	[Binding]
	public static class SharedSteps
	{
		public static void Init(ScenarioContext context)
		{
			var kernel = new StandardKernel(new SampleDatastoreModule());

//			kernel.Bind<IProvideInstances>().To<NinjectInstanceProvider>();
//
//			//Load all assemblies in bin directory into app domain
//			Directory.GetFiles(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "*.dll")
//				.Select(Assembly.LoadFile).ToList();

//			kernel.BindAllImplementationsOf(typeof(IAutoMap));
//			AutoMapConfig.RegisterAutoMaps(kernel.Get<IProvideInstances>());

			// Database context
			var sampleContext = new MockSampleContext();
			kernel.Rebind<MockSampleContext>().ToConstant(sampleContext);
			kernel.Rebind<ISampleContext>().ToConstant(sampleContext);

			context.Set<IKernel>(kernel);

			context.Set((Exception)null);
		}
	}
}