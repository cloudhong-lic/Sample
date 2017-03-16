using System.Threading.Tasks;
using Sample.Domain.Models;

namespace Sample.Service.Handlers
{
	public interface ISampleHandle
	{
		Task<Animal> Handle(int animalKey);
	}
}
