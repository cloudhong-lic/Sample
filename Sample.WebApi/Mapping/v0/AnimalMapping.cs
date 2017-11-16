namespace Sample.WebApi.Mapping.v0
{
	/// <summary>
	/// 
	/// </summary>
	public static class AnimalMapping
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="animal"></param>
		/// <returns></returns>
		public static Contract.v0.Animal ToContract(this Domain.Models.Animal animal)
		{
			return new Contract.v0.Animal
			{
				AnimalKey = animal.AnimalKey,
				BirthDate = animal.BirthDate,
				SireAnimalKey = animal.SireAnimalKey,
				DamAnimalKey = animal.DamAnimalKey,
				Sex = animal.Sex.ToContract(),
				Species = animal.Species.ToContract(),
				UpdateTime = animal.UpdateTime
			};
		}
	}
}