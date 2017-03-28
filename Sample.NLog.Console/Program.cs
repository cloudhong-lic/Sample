using NLog;

namespace Sample.NLog.Console
{
	internal class Program
	{
		private static readonly Logger logger = LogManager.GetCurrentClassLogger();

		private static void Main(string[] args)
		{
			// Log等级排序, 从低到高
			ShowLogLevel();

			// 加入参数
			Parameterized();
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
}