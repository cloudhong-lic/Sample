using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sample.NLog.Extensions;
using Moq;

//只需要加入ILogger接口所在的库, 并不需要真的加载NLog
using Ninject.Extensions.Logging;

namespace Sample.NLog.Test
{
	[TestClass]
	public class TestNLog
	{
		[TestMethod]
		public void TestNLogCall()
		{
			// Mock ILogger接口
			var mockLogger = new Mock<ILogger>();

			// 调用被测试类
			var handler = new Handler(mockLogger.Object);
			handler.ShowLog();

			// 检查某一个Log是否被调用到
			mockLogger.Verify(x => x.Info(It.Is<string>(s => s == "Sample informational message")), Times.Once);
		}
	}
}
