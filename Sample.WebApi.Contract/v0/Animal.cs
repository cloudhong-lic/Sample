using System;

namespace Sample.WebApi.Contract.v0
{
	public class Animal
	{
		public int AnimalKey { get; set; }
		public DateTimeOffset? BirthDate { get; set; }
		public int? SireAnimalKey { get; set; }
		public int? DamAnimalKey { get; set; }
		public Sex Sex { get; set; }
		public Species Species { get; set; }
		public DateTimeOffset UpdateTime { get; set; }
	}

	public enum Sex
	{
		Male,
		Female,
	}

	public enum Species
	{
		Cattle,
		Deer,
	}
}
