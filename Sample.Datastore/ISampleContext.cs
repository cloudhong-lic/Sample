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

		Task<int> SaveChangesAsync();
	}
}