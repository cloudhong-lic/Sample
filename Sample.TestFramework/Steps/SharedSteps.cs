using System;
using Ninject;
using Sample.Datastore;
using TechTalk.SpecFlow;

namespace Sample.TestFramework.Steps
{
	[Binding]
	public static class SharedSteps
	{
		/// <summary>
		/// 全局测试初始化静态方法
		/// 在具体测试模块中被调用, 以进行依赖注入, Exception初始化, 数据库设置等工作
		/// </summary>
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
