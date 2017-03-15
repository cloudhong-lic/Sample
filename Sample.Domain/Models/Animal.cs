using System;

namespace Sample.Domain.Models
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
}
