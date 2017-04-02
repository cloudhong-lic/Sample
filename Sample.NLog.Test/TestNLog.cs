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
		// 测试Ninject.Extensions.Logging的ILogger接口
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

			// Verify带参数的Log方法, 由于接受两个参数, 所以需要检查一个It.IsAny<object[]>(), 不是很方便
			// 所以在实际log的时候使用拼接字符串更方便一些, 虽然损失一些性能
			mockLogger.Verify(x => x.Trace(It.Is<string>(s => s == "Sample trace message, {0}"), It.IsAny<object[]>()), Times.Once);
		}

		[TestMethod]
		public void TestNLogLibrary()
		{
			// Mock ILog接口
			var mockLogger = new Mock<ILog>();

			// Mock Log Factory, 这点很麻烦
			var mockLogFactory = new Mock<ILogFactory>();
			mockLogFactory.Setup(x => x.CreateLog(It.IsAny<Type>())).Returns(mockLogger.Object);

			// 调用被测试类
			var handler = new Sample.NLog.Library.Handler(mockLogFactory.Object);
			handler.ShowLog();

			// 检查某一个Log是否被调用到
			mockLogger.Verify(x => x.Info(It.Is<string>(s => s == "Sample informational message")), Times.Once);

			// Verify一个委托Func的Log方法, 需要Invoke一下
			// TODO: 不确定这是一个正确的方法, 但是可以用
			mockLogger.Verify(x => x.Info(It.Is<Func<string>>(s => s.Invoke().StartsWith("Sample object message"))), Times.Once);
		}
	}
}
