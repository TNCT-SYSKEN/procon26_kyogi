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
	map.resize(34);
	for (int i = 0; i < 34; i++)
		map[i].resize(34);
	v.resize(34);
	for (int i = 0; i < 34; i++)
		v[i].resize(34);

	for (int i = 0; i < 34; i++)
		for (int j = 0; j < 34; j++){
			if (i == 0 || j == 0 || i == 33 || j == 33)
				v[i][j] = 1;
			map[i][j] = Map[i][j];
		}
	updata_v();
}

void Map::updata_v(){
	int x[4] = { -1, 0, 1, 0 };
	int y[4] = { 0, 1, 0, -1 };

	for (int i = 0; i < 34; i++){
		for (int j = 0; j < 34; j++){
			int sum = 0;
			if (map[i][j] == 1){
				v[i][j] = -1;
				continue;
			}
			for (int k = 0; k < 4; k++){
				int X = j + x[k];
				int Y = i + y[k];
				if (map[Y][X] == 1)
					sum++;
			}
			v[i][j] = sum;
		}
	}
	for (int i = 0; i < 34; i++){
		for (int j = 0; j < 34; j++)
			std::cout << map[i][j];
		std::cout << std::endl;
	}

	//まわりを文字列化
	flag = 0;
	//再帰処理のための変数
	int vec[34][34] = { 0 };
	for (int YY = 1; YY <= 32; YY++){
		for (int XX = 1; XX <= 32; XX++)
			if (map[YY][XX] == 0){
				Y = YY - 1;
				X = XX;
				if (num == 0)
					search_first(Y, X, vec, 3);
				else{
					if (map[Y][X] < 2)
						continue;
					else if (map[Y + 1][X - 1] >= 2){
						Y++;
						X--;
						search(Y, X, vec, 4);
					}
				}
				flag = 1;
				break;
			}
		if (flag == 1)
			break;
	}
}

/*
 4
2□3
 1
*/
void Map::search_first(int y, int x, int vec[][34], int way){
	if (vec[y][x] == 1)
		return;
	std::cout << str.size() << std::endl;
	std::cout << y << " " << x << std::endl;
	vec[y][x] = 1;
	//下
	if (way == 3 || way == 4){
		if (y < 33 && map[y + 1][x] != 0)
			search(y + 1, x, vec, 1);
		else if (y < 33 && map[y + 1][x] == 0)
			str.push_back(1);
	}
	//右
	if (way != 1){
		if (x < 33 && map[y][x + 1] != 0)
			search(y, x + 1, vec, 3);
		else if (x < 33 && map[y][x + 1] == 0)
			str.push_back(3);
	}
	//上
	if (y > 0 && map[y - 1][x] != 0)
		search(y - 1, x, vec, 4);
	else if (y > 0 && map[y - 1][x] == 0)
		str.push_back(4);
	//左
	if (x > 0 && map[y][x - 1] != 0)
		search(y, x - 1, vec, 2);
	else if (x > 0 && map[y][x - 1] == 0)
		str.push_back(2);


	//左、下から来た場合の上の判定のタイミング
	if (way == 1 || way == 2){
		if (y < 33 && map[y + 1][x] != 0)
			search(y + 1, x, vec, 1);
		else if (y < 33 && map[y + 1][x] == 0)
			str.push_back(1);
	}
	if (way == 1){
		if (x < 33 && map[y][x + 1] != 0)
			search(y, x + 1, vec, 3);
		else if (x < 33 && map[y][x + 1] == 0)
			str.push_back(3);
	}
}


void Map::search(int y, int x, int vec[][34], int way){
	if (vec[y][x] == 1)
		return;
	std::cout << str.size() << std::endl;
	std::cout << y << " " << x << std::endl;
	vec[y][x] = 1;
	//下
	if (way == 3 || way == 4){
		if (y < 33 && map[y + 1][x] >= 2)
			search(y + 1, x, vec, 1);
		else if (y < 33 && map[y + 1][x] == 0)
			str.push_back(1);
	}
	//右
	if (way != 1){
		if (x < 33 && map[y][x + 1] >= 2)
			search(y, x + 1, vec, 3);
		else if (x < 33 && map[y][x + 1] == 0)
			str.push_back(3);
	}
	//上
	if (y > 0 && map[y - 1][x] >= 2)
		search(y - 1, x, vec, 4);
	else if (y > 0 && map[y - 1][x] == 0)
		str.push_back(4);
	//左
	if (x > 0 && map[y][x - 1] >= 2)
		search(y, x - 1, vec, 2);
	else if (x > 0 && map[y][x - 1] == 0)
		str.push_back(2);
	

	//左、下から来た場合の上の判定のタイミング
	if (way == 1 || way == 2){
		if (y < 33 && map[y + 1][x] >= 2)
			search(y + 1, x, vec, 1);
		else if (y < 33 && map[y + 1][x] == 0)
			str.push_back(1);
	}
	if (way == 1){
		if (x < 33 && map[y][x + 1] >= 2)
			search(y, x + 1, vec, 3);
		else if (x < 33 && map[y][x + 1] == 0)
			str.push_back(3);
	}
}