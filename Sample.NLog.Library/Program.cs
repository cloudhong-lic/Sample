using Library.Logging.NLog;
using Library.Logging.v0;
using Ninject;

namespace Sample.NLog.Library
{
	internal class Program
	{
		private static readonly IKernel Kernel = new StandardKernel();

		private static void Main(string[] args)
		{
			// 需要手动绑定LogFactory, 也许有更好的办法, 更新Library??
			Kernel.Bind<ILogFactory>().To<LogFactory>();

			Kernel.Get<Handler>().ShowLog();
		}
	}
}