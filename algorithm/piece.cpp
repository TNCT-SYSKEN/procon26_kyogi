#include <iostream>
#include <vector>

#include "piece.h"

void Piece::piece_update(){
	str.clear();
	int y, x;
	for (int i = 0; i < 8; i++){
		int j;
		for (j = 0; j < 8; j++){
			if (parts[i][j] != 0){
				y = i;
				x = j;
				break;
			}
		}
		if (j != 8)
			break;
	}
	int v[8][8] = { 0 };
	search(y, x, v, 3);
}

/*
	1
 3□2
	4
*/
void Piece::search(int y, int x, int v[][8], int way){
	if (v[y][x] == 1)
		return;
	v[y][x] = 1;
	//wayの向き:下から来た場合は1、つまり前の動作は下向きのとき
	//上
	if (way == 3 || way == 1){
		if (y > 0 && parts[y - 1][x] != 0)
			search(y - 1, x, v, 4);
		else
			str.push_back(1);
	}
	//右
	if (way != 4){
		if (x < 7 && parts[y][x + 1] != 0)
			search(y, x + 1, v, 3);
		else
			str.push_back(2);
	}
	//下
	if (y < 7 && parts[y + 1][x] != 0)
		search(y + 1, x, v, 1);
	else
		str.push_back(4);
	//左
	if (x > 0 && parts[y][x - 1] != 0)
		search(y, x - 1, v, 2);
	else
		str.push_back(3);

	//左、下から来た場合の上の判定のタイミング
	if (way == 4 || way == 2){
		if (y > 0 && parts[y - 1][x] != 0)
			search(y - 1, x, v, 4);
		else
			str.push_back(1);
	}
	if (way == 4){
		if (x < 7 && parts[y][x + 1] != 0)
			search(y, x + 1, v, 3);
		else
			str.push_back(2);
	}
}

void Piece::routen(int pattern){
	if ((pattern > 3 && out[1] == 0) || (pattern <= 3 && out[1] == 1)){
		out[1]++;
		out[1] %= 2;
		for (int i = 0; i < 8; i++){
			for (int j = 0; j < 4; j++){
				int val = parts[i][j];
				parts[i][j] = parts[i][7 - j];
				parts[i][7 - j] = val;
			}
		}
	}
	int time = abs(pattern % 4 - out[0]);
	out[0] = pattern % 4;
	for (int s = 0; s < time; s++){
		std::vector<std::vector<int> > v(4, std::vector<int>(4));
		//4分割して入れ替える
		for (int i = 0; i < 4; i++)
			for (int j = 0; j < 4; j++)
				v[i][j] = parts[i][j];
		for (int i = 0; i < 4; i++)
			for (int j = 0; j < 4; j++)
				parts[i][j] = parts[7 - j][i];
		for (int i = 0; i < 4; i++)
			for (int j = 0; j < 4; j++)
				parts[7 - j][i] = parts[7 - i][7 - j];
		for (int i = 0; i < 4; i++)
			for (int j = 0; j < 4; j++)
				parts[7 - i][7 - j] = parts[j][7 - i];
		for (int i = 0; i < 4; i++)
			for (int j = 0; j < 4; j++)
				parts[j][7 - i] = v[i][j];
	}
	piece_update();
}

void Piece::set(){
	out[0] = 0;
	out[1] = 0;
	int x[4] = { -1, 0, 1, 0 };
	int y[4] = { 0, 1, 0, -1 };

	int val[8][8] = { 0 };
	for (int i = 0; i < 8; i++){
		for (int j = 0; j < 8; j++){
			if (parts[i][j] == 1){
				int sum = 0;
				for (int s = 0; s < 4; s++){
					int X = j + x[s];
					int Y = i + y[s];
					if (Y >= 0 && Y < 8 && X >= 0 && X < 8 && parts[Y][X] == 1)
						sum++;
				}
				val[i][j] = 4 - sum;
				if (sum == 4)
					val[i][j] = 4;
			}
		}
	}
	for (int i = 0; i < 8; i++){
		for (int j = 0; j < 8; j++){
			parts[i][j] = val[i][j];
		}
	}
	piece_update();
}