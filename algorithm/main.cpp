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
	Map map;
	std::vector<Piece> piece;
	int sum;

	input_sum(sum);
  input(map, piece, sum);

	/*for (int k = 0; k < sum; k++){
		for (int i = 0; i < 8; i++){
			for (int j = 0; j < 8; j++)
				std::cout << piece[k].parts[i][j];
			std::cout << std::endl;
		}
	}*/

	//screen_v(map.v);

	
	//output();

  //ドラッグ＆ドロップ用
  /*ifstream ifs(argv[1], 1);
  if (ifs.bad())return -1;
  string line;
  */
}
