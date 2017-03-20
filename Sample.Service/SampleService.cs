using System;
using System.Threading;
using GreenPipes;
using Library.Configuration.MassTransit;
using MassTransit;
using MassTransit.NLogIntegration;
using Ninject;
using NLog;
using Sample.Consumers;
using Sample.Service.Handlers;
using Sample2.Messages.Events.v0;
using Topshelf;

namespace Sample.Service
{
	public class SampleService
	{
		private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
		private readonly CancellationTokenSource _cancellationToken = new CancellationTokenSource();
		private readonly IKernel _kernel;
		private IBusControl _busControl;

		public SampleService(IKernel kernel)
		{
			_kernel = kernel;
		}

		public bool Start(HostControl hostControl)
		{
			_logger.Info("Starting service");
			hostControl.RequestAdditionalTime(TimeSpan.FromSeconds(60));

			try
			{
				_logger.Info("Start MassTransit");
				_busControl = ConfigureMassTransit();
				_busControl.Start();

				// TODO: 在这里直接调用了ISampleHandle, 不太好
				// 需要寻求更好的方法
				var animal = _kernel.Get<ISampleHandle>().Handle(1).Result;
				_logger.Info($"AnimalKey:{animal.AnimalKey}, Sex:{animal.Sex}, Species:{animal.Species}");

				_logger.Info("Service started");
				return true;
			}
			catch (Exception ex)
			{
				_logger.Fatal(ex, "Error starting service");
				return false;
			}
		}

		public void Stop()
		{
			_logger.Info("Stopping service");
			//_busControl?.Stop();
			_cancellationToken.Cancel();
			_logger.Info("Service stopped");
		}

		private IBusControl ConfigureMassTransit()
		{
			var busControl = Bus.Factory.CreateUsingRabbitMq(sbc =>
			{
				var host = sbc.SetupRabbitMqHostFromConfig();

				sbc.UseInMemoryOutbox();
				sbc.UseNLog();

				sbc.ReceiveEndpoint(
					host,
					"Sample.Service",
					e =>
					{
						e.ApplySettingsFromConfig();
						e.Consumer(
							() => _kernel.Get<FarmerConsumer>(),
							x =>
							{
								x.Message<INewFarmerEvent>(messageConfigurator =>
								{
									messageConfigurator.UsePartitioner(MassTransitConfiguration.ConcurrencyLimit, context => context.Message.Id);
								});
							});

						e.UseRetry(r =>
						{
							r.Ignore<ActivationException>();
							r.Immediate(2);
						});
					});
			});

			_kernel.Bind<IBusControl>().ToConstant(busControl);
			_kernel.Bind<IBus>().ToConstant(busControl);

			return busControl;
		}
	}
}
