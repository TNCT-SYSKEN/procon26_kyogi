#include <vector>
#include <iostream>
#include <cstdlib>
#include <fstream>
#include<string>
#include <windows.h>

#include "func.h"
#include "map"
#include "piece.h"

void put(Piece& piece){

}

void search_place(std::vector<std::vector<int> > v, std::vector<std::vector<std::vector<int> >>& place){
	for (int i = 0; i < 32; i++){
		for (int j = 0; j < 32; j++){
			if (v[i][j] == 3){

			}
				/*for (int k = i - 5; k < (i+5); k++){
					for (int s = j - 5; s < (j + 5); s++)
						if (k >= 0 && s >= 0 && k < 32 && s < 32)
							if (v[k][s] == -1)
								std::cout << "*";
							else
								std::cout << v[k][s];
						std::cout << std::endl;
				}*/
		}
		std::cout << std::endl;
	}
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
	for (int i = 0; i < 32; i++){
		for (int j = 0; j < 32; j++)
			if (v[i][j] == -1)
				std::cout << "*";
			else
				std::cout << v[i][j];
		std::cout << std::endl;
	}
}

void input_sum(int& sum){
	//ファイル入力ストリームの初期化
	std::ifstream ifs("Debug/quest9.txt");
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
	std::ifstream ifs("Debug/quest9.txt");
	std::string line;
	std::vector<std::vector<int> > map(32, std::vector<int>(32));
	piece.resize(sum);
	for (int i = 0; i < sum; i++) {
		piece[i].parts.resize(8);
		for (int j = 0; j < 8; j++) {
			piece[i].parts[j].resize(8);
		}
	}
	int N = 0, t = 0;
	//1ファイルの中身を一行ずつ追加
	while (getline(ifs, line)){
		if (line.size() == 32){
			for (int i = 0; i < 32; i++)
				map[t][i] = (int)line[i] - (int)'0';
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