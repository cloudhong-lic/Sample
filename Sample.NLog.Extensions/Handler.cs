//使用了Ninject.Extensions.Logging系列库后可以不需要再使用自己定义的Library.Logging和Library.Logging.Nlog库了, 都包含在里面了, 而且更容易使用
using Ninject.Extensions.Logging;

namespace Sample.NLog.Extensions
{
	public class Handler
	{
		// 非常清晰的注入, 方便好用, 而且是可以在测试中被Mock的
		// 应该始终使用这种方式
		private readonly ILogger _logger;

		public Handler(ILogger logger)
		{
			_logger = logger;
		}

		public void ShowLog()
		{
			// 如果使用这种方法log参数, 在测试中就不太方便了.
			// 还是建议使用字符串拼接, 虽然可能损失一些性能, 但方便
			_logger.Trace("Sample trace message, {0}", 1);

			_logger.Debug("Sample debug message");
			_logger.Info("Sample informational message");
			_logger.Warn("Sample warning message");
			_logger.Error("Sample error message");
			_logger.Fatal("Sample fatal error message");
		}
	}
}