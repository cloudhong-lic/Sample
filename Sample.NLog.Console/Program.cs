using Newtonsoft.Json;
using NLog;

namespace Sample.NLog.Console
{
	internal class Program
	{
		// 需要在每个类一开始实例化这个Logger类
		// 使用Ninject.Extensions.Logging是一个更好的选择, 可以被测试
		private static readonly Logger logger = LogManager.GetCurrentClassLogger();

		private static void Main(string[] args)
		{
			// Log等级排序, 从低到高
			ShowLogLevel();

			// 加入参数
			Parameterized();

			// 使用Newtonsoft.Json库后就可以不需要再使用自定义的LoggingConvention库中的ForLogging方法了.可以直接解析为Json模式
			LogObject();
		}

		private static void LogObject()
		{
			var o1 = new O1
			{
				a1 = "bar",
				a2 = 123,
				a3 = new O2
				{
					b1 = 23.32
				}
			};

			logger.Info("Object: {0}", JsonConvert.SerializeObject(o1));
		}

		private static void Parameterized()
		{
			const int k = 10;

			// 调用C#自带的字符串连接函数, 会损失一些性能, 但比较方便
			logger.Info($"Sample informational message, {k}");

			// 调用built-in formatting functionality of NLog, 性能更好一些
			logger.Info("Sample informational message, {0}", k);
		}

		private static void ShowLogLevel()
		{
			logger.Trace("Sample trace message");
			logger.Debug("Sample debug message");
			logger.Info("Sample informational message");
			logger.Warn("Sample warning message");
			logger.Error("Sample error message");
			logger.Fatal("Sample fatal error message");

			// 也可以直接调用Log方法, 然后传入LogLevel来控制等级
			logger.Log(LogLevel.Info, "Sample informational message");
		}
	}

	internal class O1
	{
		public string a1 { get; set; }
		public int a2 { get; set; }
		public O2 a3 { get; set; }
	}

	internal class O2
	{
		public double b1 { get; set; }
		public int b2 { get; set; }
	}

}