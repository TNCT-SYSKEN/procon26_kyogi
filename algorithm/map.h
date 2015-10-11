#pragma once;
#include <vector>
#include <map>

#include "piece.h"
#include "map.h"

class Map{
private:
	//すでに敷いてるピースの隣接している空マスのアドレスを格納
	std::vector<std::pair<int,int> > adress;
	//向き
	int way;
	//search脱出用のフラグ
	int flag;
	//何個目か
	int num;
public:
  //マップデータ
	std::vector<std::vector<int> > map;
	//周囲の文字列
	std::vector<int> str;
	//文字列の始点の座標
	int X, Y;
	//隣接している数を格納
	std::vector<std::vector<int> > v;

	//データをセットmapに
	void set(std::vector<std::vector<int> >);
	//隣接数の更新
	void updata_v();

	/*敷いていく用*/
	//敷くことができれば敷く
	bool lay(Piece, int, int, int);
	//すでにピースを敷いているか
	bool already();
};