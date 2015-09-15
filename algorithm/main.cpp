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
	Piece piece;

  input(map, piece);

	output(piece.get_out());

  //ドラッグ＆ドロップ用
  /*ifstream ifs(argv[1], 1);
  if (ifs.bad())return -1;
  string line;
  */
}
