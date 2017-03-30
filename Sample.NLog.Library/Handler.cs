using Library.Logging;
using Library.Logging.v0;

namespace Sample.NLog.Library
{
	public class Handler
	{
		private readonly ILog _logger;

		public Handler(ILogFactory logFactory)
		{
			// 需要在构造函数中调用Factory, 不知是否有更好的写法?
			_logger = logFactory.CreateLog(GetType());
		}

		public void ShowLog()
		{
			// Trace和Debug的接口没有实现完全, 只能调用FUNC
			_logger.Trace(() => "Sample trace message");
			_logger.Debug(() => "Sample debug message");

			_logger.Info("Sample informational message");
			_logger.Warn("Sample warning message");
			_logger.Error("Sample error message");
			_logger.Fatal("Sample fatal error message");

			// 调用ForLogging来解析Object类型
			_logger.Info(() => LoggingConvention.ForLogging("Sample object message,", new { P1 = 1, P2 = "ABC"}));
		}
	}
}