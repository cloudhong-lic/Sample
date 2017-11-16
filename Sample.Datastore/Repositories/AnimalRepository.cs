using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
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

		/// <summary>
		/// 通过EF获取数据
		/// </summary>
		/// <param name="animalKey"></param>
		/// <returns></returns>
		public async Task<Animal> Get(int animalKey)
		{
			return await _context.Animals.FirstOrDefaultAsync(x => x.AnimalKey == animalKey).ConfigureAwait(false);
		}

		/// <summary>
		/// 通过SQL QUERY获取数据
		/// 不带参数
		/// </summary>
		/// <param name="animalKeys"></param>
		/// <returns></returns>
		public async Task<List<Animal>> GetBySqlQueryWithoutParameter(int[] animalKeys)
		{
			return await Task.Run(() =>
			{
				var tempString = new StringBuilder();
				foreach (var animal in animalKeys)
					tempString = tempString.Append($@",{animal}");
				var keys = tempString.Remove(0, 1).ToString();

				var query = $@"SELECT [AnimalKey]
										,[BirthDate]
										,[SireAnimalKey]
										,[DamAnimalKey]
										,sex.[Description] as Sex
										,species.Description as Species
										,animal.UpdateTime
									FROM [Sample_local].[Sample].[Animal] as animal
									JOIN [Sample_local].[Sample].[Sex] as sex on sex.Id = animal.SexId
									JOIN [Sample_local].[Sample].[Species] as species on species.Id = animal.SpeciesId
									WHERE [AnimalKey] in ({keys})";

				var rawAnimalDatas = _context.Database.SqlQuery<Models.RawAnimalData>(query).ToList();

				// 将原始数据类型转换成Domain model中的类型
				// TODO: 此处应该有个mapping help类
				return rawAnimalDatas.Select(animal => new Animal
				{
					AnimalKey = animal.AnimalKey,
					BirthDate = animal.BirthDate,
					DamAnimalKey = animal.DamAnimalKey,
					SireAnimalKey = animal.SireAnimalKey,
					Sex = Sex.Male, // TODO
					Species = Species.Cattle,
					UpdateTime = animal.UpdateTime
				}).ToList();
			});
		}
	}
}