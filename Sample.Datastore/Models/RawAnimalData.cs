using System;

namespace Sample.Datastore.Models
{
	// SQL Query只能直接从数据库获取简单数据类型
	// 所以需要一个object来保存从数据库中获取的原始数据类型
	// 此类只在当前Datastore中使用, 不对外暴露
	internal class RawAnimalData
	{
		public int AnimalKey { get; set; }

		public DateTimeOffset? BirthDate { get; set; }

		public int? SireAnimalKey { get; set; }

		public int? DamAnimalKey { get; set; }

		// Sex在Domain model中是Enum type
		public string Sex { get; set; }

		// Species model中是Enum type
		public string Species { get; set; }

		public DateTimeOffset UpdateTime { get; set; }
	}
}
