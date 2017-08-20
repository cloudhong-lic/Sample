using System.Collections.Generic;
using System.Configuration;
using System.Threading.Tasks;
using Library.WebApi.Interfaces;
using Sample.WebApi.Client.Interfaces.v0;
using Sample.WebApi.Contract.v0;

namespace Sample.WebApi.Client.v0
{
	public class AnimalsProvider : IAnimalsProvider
	{
		private readonly IHttpServiceHelper _serviceHelper;

		public AnimalsProvider(IHttpServiceHelper serviceHelper)
		{
			_serviceHelper = serviceHelper;
		}

		public async Task<Animal> Get(int animalKey)
		{
			var uri = ConfigurationManager.AppSettings["ApisBaseUrl"] + ResourceLocator.Animals.Get(animalKey);
			return await _serviceHelper.GetAsync<Animal>(uri).ConfigureAwait(false);
		}

		public async Task<List<Animal>> GetByAnimalKeys(int[] animalKeys)
		{
			var uri = ConfigurationManager.AppSettings["ApisBaseUrl"] + ResourceLocator.Animals.GetByAnimalkeys();
			return await _serviceHelper.PostAsync<List<Animal>>(uri, animalKeys).ConfigureAwait(false);
		}
	}
}