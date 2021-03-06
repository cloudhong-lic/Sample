﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Sample.WebApi.Contract.v0;

namespace Sample.WebApi.Client.Interfaces.v0
{
	public interface IAnimalsProvider
	{
		Task<Animal> Get(int animalKey);

		Task<List<Animal>> GetByAnimalKeys(int[] animalKeys);
	}
}
