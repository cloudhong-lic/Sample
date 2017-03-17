using Ninject.Modules;

namespace Sample.WebApi
{
	public class WebApiModule : NinjectModule
	{
		public override void Load()
		{
			// 在NinjectWebCommon中load了SampleDatastoreModule
		}
	}
}
