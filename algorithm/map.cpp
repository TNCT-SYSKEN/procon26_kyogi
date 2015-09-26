#include <iostream>
#include <vector>
#include <map>

#include "map.h"
#include "piece.h"

bool Map::lay(Piece piece, int y, int x, int pattern){
	piece.routen(pattern);
	std::vector<std::vector<int> > parts = piece.parts;
	//ピースの位置の微調整
	if (adress.size()){
		//二つ目以降
	}
	else{
		//一番最初に敷く
	}
	//敷けるかチェック
	for (int i = 0; i < parts.size(); i++){
		for (int j = 0; j < parts[i].size(); j++){
			if (parts[i][j] >= 1){
				if (map[y + i][x + j] == 1 || y < 0 || x < 0 || y >= 32 || x >= 32){
					return false;
				}
			}
		}
	}
	//敷く
	for (int i = 0; i < parts.size(); i++){
		for (int j = 0; j < parts[i].size(); j++){
			if (parts[i][j] == 1){
				map[y + i][x + j] = 1;
			}
		}
	}
	return true;
}

bool Map::already(){
	int x[4] = { -1, 0, 1, 0 };
	int y[4] = { 0, 1, 0, -1 };
	adress.clear();
	int sum = 0;
	for (int i = 0; i < 32; i++){
		for (int j = 0; j < 32; j++){
			//敷いている場所があれば、
			if (map[i][j] == 2){
				for (int k = 0; k < 4; k++){
					int X = j + x[k];
					int Y = i + y[k];
					if (Y < 0 || Y > 31 || X < 0 || X > 31 || map[Y][X] == 0){
						adress.push_back(std::make_pair(Y, X));
					}
				}
			}
		}
	}
	return true;
}

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