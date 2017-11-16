using Ninject.Modules;

namespace Sample.WebApi
{
	/// <summary>
	/// 
	/// </summary>
	public class WebApiModule : NinjectModule
	{
		/// <summary>
		/// 
		/// </summary>
		public override void Load()
		{
			// 在NinjectWebCommon中load了SampleDatastoreModule
			// 因此此处暂时没有更多的Module需要Load
		}
	}
}
