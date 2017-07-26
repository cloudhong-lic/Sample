using System.Data.Entity;
using System.Threading.Tasks;
using Sample.Domain.Models;

namespace Sample.Datastore
{
	public interface ISampleContext
	{
		IDbSet<Animal> Animals { get; set; }
//		IDbSet<Sex> Sexes { get; set; }
//		IDbSet<Species> Species { get; set; }

		/// <summary>
		/// 引入Database以便使用SqlQuery接口
		/// 这样可以用SQL QUERY而不是EF去获取数据
		/// </summary>
		Database Database { get; }

		Task<int> SaveChangesAsync();
	}
}