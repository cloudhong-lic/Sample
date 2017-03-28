
使用个人扩展的Logging库

包括:
Ninject.Extensions.Logging
Ninject.Extensions.Logging.nlog4
Newtonsoft.Json

使用了Ninject.Extensions.Logging系列库后可以不需要再使用自己定义的Library.Logging和Library.Logging.Nlog库了, 都包含在里面了, 而且更容易使用


使用Newtonsoft.Json库后就可以不需要再使用自定义的LoggingConvention库中的ForLogging方法了. 可以直接解析为Json模式
JsonConvert.SerializeObject(new Layer1
			{
				a1 = "bar",
				a2 = 123,
				a3 = new Layer2
				{
					b1 = 23.32
				}
			})
