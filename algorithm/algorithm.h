#pragma once
#include <vector>

class algorithm{
private:
	//石の総数
  int sum;
  //マップデータ
	std::vector<std::vector<int> > map;
  //石データ
	std::vector<std::vector<std::vector<int> > > parts;
  //出力データ
	std::vector<std::vector<int> > out;

public:
    algorithm();
	//ファイルの入力
	void set(std::vector<std::vector<int> >, std::vector<std::vector<std::vector<int> >>, int);
	//ファイルへ出力
	std::vector<std::vector<int> > get_out();
	//アルゴリズム実行
	void run();
	//順位付け
	void rank();
};

//extern void algorithm(int sum_part, vector<vector<int> >&map(32),
	//vector<vector<vector<int> > >&parts(10, vector<vector<int>(8, vector<int>(8) >, vector<vector<int> >& out(10, vector<int>(4));
//void algorithm(algorithm &);