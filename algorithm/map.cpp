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

	for (int i = 0; i < 32; i++){
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

	//まわりを文字列化
	flag = 0;
	int vec[32][32] = { 0 };
	int data[32][32];
	for (int i = 0; i < 32; i++)
		for (int j = 0; j < 32; j++)
			data[i][j] = map[i][j];
	for (int YY = 0; YY < 32; YY++){
		for (int XX = 0; XX < 32; XX++)
			if (map[YY][XX] == 0){
				Y = YY;
				X = XX;
				search(Y, X, vec, 1);
				break;
			}
		if (v[Y][X] == 0)
			break;
	}
}

void Map::search(int y, int x, int vec[][32], int way){
	std::cout << str.size() << std::endl;
	if (vec[y][x] == 1 && y == Y && x == X){
		flag = 1;
		return;
	}
	if (vec[y][x] == 1 || flag == 1)
		return;
	vec[y][x] = 1;
	//wayの向き:下から来た場合は1、つまり前の動作は下向きのとき
	//上
	if (way == 3 || way == 1){
		if (y > 0 && map[y - 1][x] == 0)
			search(y - 1, x, vec, 4);
		else
			str.push_back(4);
	}
	//右
	if (way != 4){
		if (x < 31 && map[y][x + 1] == 0)
			search(y, x + 1, vec, 3);
		else
			str.push_back(3);
	}
	//下
	if (y < 31 && map[y + 1][x] == 0)
		search(y + 1, x, vec, 1);
	else
		str.push_back(1);
	//左
	if (x > 0 && map[y][x - 1] == 0)
		search(y, x - 1, vec, 2);
	else
		str.push_back(2);

	//左、下から来た場合の上の判定のタイミング
	if (way == 4 || way == 2){
		if (y > 0 && map[y - 1][x] == 0)
			search(y - 1, x, vec, 4);
		else
			str.push_back(4);
	}
	if (way == 4){
		if (x < 31 && map[y][x + 1] != 0)
			search(y, x + 1, vec, 3);
		else
			str.push_back(3);
	}
}