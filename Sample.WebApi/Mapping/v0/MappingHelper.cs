using System;

namespace Sample.WebApi.Mapping.v0
{
	/// <summary>
	/// 
	/// </summary>
	public static class MappingHelper
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="sex"></param>
		/// <returns></returns>
		public static Contract.v0.Sex ToContract(this Domain.Models.Sex sex)
		{
			switch (sex)
			{
				case Domain.Models.Sex.Male:
					return Contract.v0.Sex.Male;
				case Domain.Models.Sex.Female:
					return Contract.v0.Sex.Female;
				default:
					throw new Exception("Sex cannot be mapped");
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="species"></param>
		/// <returns></returns>
		public static Contract.v0.Species ToContract(this Domain.Models.Species species)
		{
			switch (species)
			{
				case Domain.Models.Species.Cattle:
					return Contract.v0.Species.Cattle;
				case Domain.Models.Species.Deer:
					return Contract.v0.Species.Deer;
				default:
					throw new Exception("Species cannot be mapped");
			}
		}
	}
}