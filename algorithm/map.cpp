#include <iostream>
#include <vector>

#include "map.h"

void Map::set(std::vector<std::vector<int> > Map){
	map.resize(32);
	for (int i = 0; i < 32; i++)
		map[i].resize(32);
	v.resize(32);
	for (int i = 0; i < 32; i++)
		v[i].resize(32);

	for (int i = 0; i < 32; i++)
		for (int j = 0; j < 32; j++)
			map[i][j] = Map[i][j];
	updata_v();
}

void Map::updata_v(){
	int x[4] = { -1, 0, 1, 0 };
	int y[4] = { 0, 1, 0, -1 };

	for (int i = 0; i < 32; i++)
		for (int j = 0; j < 32; j++){
			int sum = 0;
			if (map[i][j] == 1){
				v[i][j] = -1;
				continue;
			}
			for (int k = 0; k < 4; k++){
				int X = j + x[k];
				int Y = i + y[k];
				if (Y < 0 || Y > 31 || X < 0 || X > 31 || map[Y][X] == 1)
					sum++;
			}
			v[i][j] = sum;
		}
}