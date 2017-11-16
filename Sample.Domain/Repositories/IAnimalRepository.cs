using System.Collections.Generic;
using System.Threading.Tasks;
using Sample.Domain.Models;

namespace Sample.Domain.Repositories
{
	public interface IAnimalRepository
	{
		Task<Animal> Get(int animalKey);

		Task<List<Animal>> GetBySqlQueryWithoutParameter(int[] animalKeys);
	}
}