using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

//只需要加入ILogger接口所在的库, 并不需要真的加载NLog
using Ninject.Extensions.Logging;
using Library.Logging.v0;

namespace Sample.NLog.Test
{
	[TestClass]
	public class TestNLog
	{
		[TestMethod]
		public void TestNLogExtensions()
		{
			// Mock ILogger接口
			var mockLogger = new Mock<ILogger>();

			// 调用被测试类
			var handler = new Sample.NLog.Extensions.Handler(mockLogger.Object);
			handler.ShowLog();

			// 检查某一个Log是否被调用到
			mockLogger.Verify(x => x.Info(It.Is<string>(s => s == "Sample informational message")), Times.Once);
			mockLogger.Verify(x => x.Trace(It.Is<string>(s => s == "Sample trace message, {0}"), It.IsAny<object[]>()), Times.Once);
		}

		[TestMethod]
		public void TestNLogLibrary()
		{
			// Mock ILog接口
			var mockLogger = new Mock<ILog>();

			// Setup Log Factory, 这点很麻烦
			var mockLogFactory = new Mock<ILogFactory>();
			mockLogFactory.Setup(x => x.CreateLog(It.IsAny<Type>())).Returns(mockLogger.Object);

			// 调用被测试类
			var handler = new Sample.NLog.Library.Handler(mockLogFactory.Object);
			handler.ShowLog();

			// 检查某一个Log是否被调用到
			mockLogger.Verify(x => x.Info(It.Is<string>(s => s == "Sample informational message")), Times.Once);
			mockLogger.Verify(x => x.Info(It.Is<Func<string>>(s => s.Invoke().StartsWith("Sample object message"))), Times.Once);
		}
	}
}
