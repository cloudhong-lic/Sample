using Ninject.Modules;
using Sample.Datastore.Repositories;
using Sample.Domain.Repositories;

namespace Sample.Datastore
{
	/// <summary>
	/// 对于Ninject来说, 每个Project都应该有自己完整的Module
	/// 这样, 其他project就可以Load这个Module去获取依赖注入
	/// </summary>
	public class SampleDatastoreModule : NinjectModule
	{
		public override void Load()
		{
			// TODO: 目前bind的scope还不是很清楚, 需要搞懂
			// 另外, 一些复杂的绑定, 全局性的绑定也不太会用
			Bind<ISampleContext>().To<SampleContext>();

			Bind<IAnimalRepository>().To<AnimalRepository>();
		}
	}
}
