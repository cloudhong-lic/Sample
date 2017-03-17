using System;
using Sample.Domain.Models;

namespace Sample.TestFramework.Steps.DataTable
{
	public class AnimalTable
	{
		public int AnimalKey { get; set; }
		public string BirthDate { get; set; }
		public int? SireAnimalKey { get; set; }
		public int? DamAnimalKey { get; set; }
		public Sex Sex { get; set; }
		public Species Species { get; set; }
		public DateTimeOffset UpdateTime { get; set; }

		public Animal ToAnimal()
		{
			return new Animal
			{
				AnimalKey = AnimalKey,
				BirthDate = string.IsNullOrWhiteSpace(BirthDate) ? (DateTimeOffset?)null: DateTimeOffset.Parse(BirthDate),
				SireAnimalKey = SireAnimalKey,
				DamAnimalKey = DamAnimalKey,
				Sex = Sex,
				Species = Species,
				UpdateTime = UpdateTime
			};
		}
	}
}