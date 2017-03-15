using System.Data.Entity;
using System.Threading.Tasks;
using Sample.Domain.Models;
using Sample.Domain.Repositories;

namespace Sample.Datastore.Repositories
{
	public class AnimalRepository : IAnimalRepository
	{
		private readonly ISampleContext _context;

		public AnimalRepository(ISampleContext context)
		{
			_context = context;
		}

		public async Task<Animal> Get(int animalKey)
		{
			return await _context.Animals.FirstOrDefaultAsync(x => x.AnimalKey == animalKey).ConfigureAwait(false);
		}
	}
}