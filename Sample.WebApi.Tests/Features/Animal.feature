Feature: Animal

Scenario Outline: 1 Get animal by key
	Given the following animal exists in database
		| AnimalKey | BirthDate  | Sex    | Species | SireAnimalKey | DamAnimalKey | UpdateTime |
		| 1         | 2017-03-01 | Male   | Cattle  |               |              | 2017-03-01 |
		| 2         | 2017-02-01 | Female | Deer    |               |              | 2017-03-01 |
	When I request animal <AnimalKey>
	Then the request is successful
		And the following animal is returned
		| AnimalKey   | BirthDate   | Sex   | Species   | SireAnimalKey | DamAnimalKey | UpdateTime |
		| <AnimalKey> | <BirthDate> | <Sex> | <Species> |               |              | 2017-03-01 |

	Examples: 
		| Description | AnimalKey | BirthDate  | Sex    | Species | SireAnimalKey | DamAnimalKey |
		| 1           | 1         | 2017-03-01 | Male   | Cattle  |               |              |
		| 2           | 2         | 2017-02-01 | Female | Deer    |               |              |
