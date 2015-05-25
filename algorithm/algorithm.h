#pragma once
#include <vector>

class algorithm{
private:
	//石の総数
  int sum_part;
  //マップデータ
	std::vector<std::vector<int> > map(32, std::vector<int>(32));
  //入力データ
	std::vector<std::vector<std::vector<int> > > parts(10, std::vector<std::vector<int> >(8, std::vector<int>(8)));
  //出力データ
	std::vector<std::vector<int> > out(10, std::vector<int>(4));

public:
	//ファイルの入力
	void input_file();
	//ファイルへ出力
	void output_file();
	//アルゴリズム実行
	void run();
	//順位付け
	void rank();
};

//extern void algorithm(int sum_part, vector<vector<int> >&map(32),
	//vector<vector<vector<int> > >&parts(10, vector<vector<int>(8, vector<int>(8) >, vector<vector<int> >& out(10, vector<int>(4));
//void algorithm(algorithm &);