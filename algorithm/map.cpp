#include <vector>

#include "map.h"

void Map::set(std::vector<std::vector<int> > Map){
	map.resize(32);
	for (int i = 0; i < 32; i++)
		map[i].resize(32);

	for (int i = 0; i < 32; i++)
		for (int j = 0; j < 32; j++)
			map[i][j] = Map[i][j];
}