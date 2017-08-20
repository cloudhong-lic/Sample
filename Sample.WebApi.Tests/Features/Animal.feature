Feature: Animal

# 使用Outline可以测试一组用例, 在Examples表中设置不同的参数以对应不同的用例
Scenario Outline: 1 Get animal by key
	Given the following animal exists in database
		| AnimalKey | BirthDate  | Sex    | Species | SireAnimalKey | DamAnimalKey | UpdateTime |
		| 1         | 2017-03-01 | Male   | Cattle  |               |              | 2017-03-01 |
		| 2         | 2017-02-01 | Female | Deer    |               |              | 2017-03-01 |
	When I request animal <AnimalKey>
	Then the request is successful
		And the following animal is returned
		# Table可以是横表也可以是竖表, 以下例子是竖表
		# 对于单个Object来说, 可以用竖表也可以用横表
		# 对于一个集合来说, 只可以用横表
		| Field         | Value       |
		| AnimalKey     | <AnimalKey> |
		| BirthDate     | <BirthDate> |
		| Sex           | <Sex>       |
		| Species       | <Species>   |
		| SireAnimalKey |             |
		| DamAnimalKey  |             |
		| UpdateTime    | 2017-03-01  |

	Examples: 
		| Description | AnimalKey | BirthDate  | Sex    | Species | SireAnimalKey | DamAnimalKey |
		| 1           | 1         | 2017-03-01 | Male   | Cattle  |               |              |
		| 2           | 2         | 2017-02-01 | Female | Deer    |               |              |

Scenario: 2 Get animals by keys
	Given the following animal exists in database - local
		| AnimalKey | BirthDate  | Sex    | Species | SireAnimalKey | DamAnimalKey | UpdateTime |
		| 1         | 2017-03-01 | Male   | Cattle  |               |              | 2017-02-01 |
		| 2         | 2017-02-01 | Female | Deer    |               |              | 2017-03-01 |
	When I request following animals
		| AnimalKey |
		| 1         |
		| 2         |
	Then the request is successful
		And the following animals are returned
		| AnimalKey | BirthDate  | Sex    | Species | SireAnimalKey | DamAnimalKey | UpdateTime |
		| 1         | 2017-03-01 | Male   | Cattle  |               |              | 2017-02-01 |
		| 2         | 2017-02-01 | Female | Deer    |               |              | 2017-03-01 |
