#include <vector>
#include <iostream>
#include <cstdlib>
#include <fstream>
#include<string>
#include <windows.h>

#include "func.h"
#include "map"
#include "piece.h"

void put(Piece& piece, Map& map){
	//mapのy x、pieceのy x 一致数 routen
	std::vector<std::vector<int> > v(4, std::vector<int>(6));
	//敷く場所を探す
	int y, x;
	std::vector<int>::iterator ite;
	for (int k = 0; k < 8; k++){
		piece.routen(k);
		for (int i = 0; i < (int)piece.str.size(); i++){
			ite = find(map.str.begin(), map.str.end(), piece.str[i]);
			while (ite != map.str.end()){
				int t = 1;
				while (1){
					int map_ite = ite - map.str.begin() + t;
					int piece_ite = i + t;
					if (map_ite < map.str.size() && piece_ite > piece.str.size() && map.str[map_ite] == piece.str[piece_ite]){
						t++;
					}
					else{
						if (v[0][4] < t){
							for (int j = 0; j < 6; j++)
								v[1][i] = v[0][i];
							v[0][4] = t;
							v[0][5] = k;
							search_adress(map, piece, v, 0, ite, i);
						}
						else if (v[1][4] < t){
							v[0][4] = t;
							v[0][5] = k;
							search_adress(map, piece, v, 1, ite, i);
						}
						break;
					}
				}
				ite = find(map.str.begin(), map.str.end(), piece.str[i]);
			}
		}
	}

	//敷けるか判定
	for (int i = 0; i < (int)v.size(); i++)
		if (check(map, piece, v))
			break;
}

bool check(Map& map, Piece& piece, std::vector<std::vector<int> > v){
	int flag = 0;
	for (int k = 0; k < 2; k++){
		for (int i = 0; i < 8; i++){
			for (int j = 0; j < 8; j++){
				if (piece.parts[i][j] >= 1){
					int y = i + v[k][0] + v[k][2];
					int x = j + v[k][1] + v[k][3];
					if (y < 0 || x < 0 || y >= 8 || x >= 8 || map.map[y][x] == 1){
						flag += k + 1;
						if (flag == 3)
							return false;
						break;
					}
				}
			}
			if (flag == 1)
				break;
		}
	}
	for (int k = 0; k < 2; k++){
		for (int i = 0; i < 8; i++){
			for (int j = 0; j < 8; j++){
				if (piece.parts[i][j] >= 1){
					int y = i + v[k][0] + v[k][2];
					int x = j + v[k][1] + v[k][3];
					if (flag == 1 + k){
						map.map[i][j] = piece.parts[i][j];
					}
				}
			}
		}
	}
	return true;
}

void search_adress(Map& map, Piece& piece, std::vector<std::vector<int> > v, int rank, std::vector<int>::iterator ite, int piece_begin_match){
	int map_begin_match = ite - map.str.begin();
	int y = map.Y;
	int x = map.X;
	int old, now;
	for (int i = 1; i < map_begin_match; i++){
		old = map.str[i + map_begin_match - 1];
		now = map.str[i + map_begin_match];
		//一直線
		if (old == now){
			if (now == 1)
				x++;
			else if (now == 2)
				y--;
			else if (now == 3)
				y++;
			else
				x--;
		}//凸
		else if (old == 3 && now == 4){
			y--;
			x--;
		}
		else if (old == 4 && now == 2){
			y++;
			x--;
		}
		else if (old == 2 && now == 1){
			y++;
			x++;
		}
		else if (old == 1 && now == 3){
			y--;
			x++;
		}
		//凹は変わらないのでなし
	}
	v[rank][0] = y;
	v[rank][1] = x;

	//ピースのアドレスを取得
	for (int i = 0; i < 8; i++){
		for (int j = 0; j < 8; j++){
			if (piece.parts[i][j] != 0){
				x = j;
				y = i;
				break;
			}
		}
		if (piece.parts[y][x] != 0)
			break;
	}
	for (int i = 0; i < piece_begin_match; i++){
		//一直線
		if (old == now){
			if (now == 1)
				x++;
			else if (now == 2)
				y--;
			else if (now == 3)
				y++;
			else
				x--;
		}//凸
		else if (old == 2 && now == 1){
			y--;
			x--;
		}
		else if (old == 1 && now == 3){
			y++;
			x--;
		}
		else if (old == 3 && now == 4){
			y++;
			x++;
		}
		else if (old == 4 && now == 2){
			y--;
			x++;
		}
		//凹は変わらないのでなし
	}
	v[rank][3] = y;
	v[rank][4] = x;
}

void screen(std::vector<std::vector<int> > map){
	system("cls");
	for (int i = 0; i < 34; i++)
		printf("_");
	printf("\n");
	for (int i = 0; i < 32; i++){
		printf("|");
		for (int j = 0; j < 32; j++)
			if (map[i][j] == 1)
				printf("X");
			else if (map[i][j] == 0)
				printf("-");
			else
				printf("O");
			printf("|");
			printf("\n");
	}
	for (int i = 0; i < 34; i++)
		printf("_");
	printf("\n");
}

void screen_v(std::vector<std::vector<int> > v){
	for (int i = 0; i < 34; i++){
		for (int j = 0; j < 34; j++)
			if (v[i][j] == -1)
				std::cout << "*";
			else
				std::cout << v[i][j];
		std::cout << std::endl;
	}
}

void input_sum(int& sum){
	//ファイル入力ストリームの初期化
	std::ifstream ifs("Debug/quest2.txt");
	std::string line;
	//1ファイルの中身を一行ずつ追加
	while (getline(ifs, line)){
		if (line.size() > 0 && line.size() < 4){
			sum = atoi(line.c_str());
		}
	}
}

void input(Map& map1, std::vector<Piece>& piece, int sum){
	//ファイル入力ストリームの初期化
	std::ifstream ifs("Debug/quest2.txt");
	std::string line;
	std::vector<std::vector<int> > map(34, std::vector<int>(34));
	piece.resize(sum);
	for (int i = 0; i < sum; i++) {
		piece[i].parts.resize(8);
		for (int j = 0; j < 8; j++) {
			piece[i].parts[j].resize(8);
		}
	}
	for (int i = 0; i < 34; i++){
		map[0][i] = 1;
		map[33][i] = 1;
	}
	int N = 0, t = 1;
	//1ファイルの中身を一行ずつ追加
	while (getline(ifs, line)){
		if (line.size() == 32){
			for (int i = 0; i < 34; i++)
				if (i == 0 || i == 33)
					map[t][i] = 1;
				else
					map[t][i] = (int)line[i - 1] - (int)'0';
			t++;
		}
		else if (line.size() == 8){
			for (int i = 0; i < 8; i++)
				piece[N].parts[t][i] = (int)(line[i] - '0');
			if (t == 7){
				N++;
			}
			t++;
		}
		else if (line.size() != 0){
		}
		else
			t = 0;
	}
	map1.set(map);
	for (int i = 0; i < sum; i++)
		piece[i].set();
}

void output(std::vector<std::vector<int> > out){
	std::ofstream ofs("output.txt");
	std::cout << out.size() << std::endl;


	for (int i = 0; i < (int)out.size(); i++){
		if (i != 0)	ofs << std::endl;

		if (out[i][2] == 0)//表
			ofs << out[i][0] << " " << out[i][1] << " H " << out[i][3];
		else if (out[i][2] == 1)//裏
			ofs << out[i][0] << " " << out[i][1] << " H " << out[i][3];
	}
}