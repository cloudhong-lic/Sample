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

		/// <summary>
		/// 由于SpecFlow不能很好地支持nullable的数据类型, 尤其是DateTimeOffset类型
		/// 所以需要建立的Table将测试用例中的string类型转换成相应的数据类型
		/// </summary>
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
