using Ninject;

namespace Sample.NLog.Extensions
{
	internal class Program
	{
		//并不需要手动Load NLogModule
		//Ninject.Extensions.Logging.nlog4已经自动做了这件事情
		private static readonly IKernel Kernel = new StandardKernel();

		private static void Main(string[] args)
		{
			Kernel.Get<Handler>().ShowLog();
		}
	}
}