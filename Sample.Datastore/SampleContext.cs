using System.Data.Entity;
using Sample.Datastore.Mappings;
using Sample.Domain.Models;

namespace Sample.Datastore
{
	/// <summary>
	/// 继承了DbContext, 原理不详
	/// </summary>
	public class SampleContext : DbContext, ISampleContext
	{
		public IDbSet<Animal> Animals { get; set; }

		/// <summary>
		/// 数据库的context方法, 原理还不清楚
		/// base关键字还需要搞清楚具体的原理
		/// name=SampleDB是在具体调用这个数据库的project中的app.config或web.config文件中设置的connectionString的名称
		/// TODO: 这个connectionString是否需要统一管理? 应该如何管理?
		/// </summary>
		public SampleContext() : base("name=SampleDB")
		{
			// 何时应该使用LazyLoading, 应该如何使用??
			Configuration.LazyLoadingEnabled = false;

			Database.SetInitializer<SampleContext>(null);
		}

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			// 这个是数据库中的Schema??
			modelBuilder.HasDefaultSchema("Sample");

			// 与mappings文件关联
			// TODO: 也许应该增加几个mappings文件的例子
			modelBuilder.Configurations.Add(new AnimalMappings());
		}
	}
}
