#include<iostream>
#include<fstream>
#include<vector>
#include<string>
#include<math.h>

#include"piece.h"
#include"func.h"
#include "map.h"
using namespace std;

int main(){
	//mapデータを管理
	Map map;
	int y, x;
	//ピース一つを管理
	std::vector<Piece> piece;
	//ピースの番号
	int num = 0;
	//ピースの総数
	int sum;
	//特徴的な場所を管理
	std::vector<std::vector<std::vector<int> >> place;

	input_sum(sum);
  input(map, piece, sum);

	//search_place(map.v, place);

	//最初に敷き始めるアドレスを適当に決める
	for (y = 0; y < 32; y++)
		for (x = 0; x < 32; x++)
			if (map.v[y][x] >= 0)
				break;
		

	//とりあえず、ピースを順番にはめれたらはめる、無理だったらパスを繰り返す
	while (num < sum){
		put(piece[num]);
		num++;
	}

	/*for (int k = 0; k < sum; k++){
		for (int i = 0; i < 8; i++){
			for (int j = 0; j < 8; j++)
				std::cout << piece[k].parts[i][j];
			std::cout << std::endl;
		}
	}*/

	screen_v(map.v);

	
	//output();

  //ドラッグ＆ドロップ用
  /*ifstream ifs(argv[1], 1);
  if (ifs.bad())return -1;
  string line;
  */
}
