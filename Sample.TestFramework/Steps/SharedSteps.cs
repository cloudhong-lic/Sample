using System;
using Ninject;
using Sample.Datastore;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace Sample.TestFramework.Steps
{
	[Binding]
	public static class SharedSteps
	{
		[BeforeTestRun]
		public static void BeforeScenario()
		{
			//添加此行可以不需要再添加一个中间的Table class去转换feature文件中的测试数据到真实的model了
			//TODO: SpecFlow自带的NullableDateTimeOffsetValueRetriever目前似乎还有问题, 即使不是Null的测试数据也会被强制转换为Null, 原因不明
			//暂时创建一个名字一模一样的类, 复制代码进去, NullableDateTimeOffsetValueRetriever就可以用了
			Service.Instance.RegisterValueRetriever(new NullableDateTimeOffsetValueRetriever());
		}

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
