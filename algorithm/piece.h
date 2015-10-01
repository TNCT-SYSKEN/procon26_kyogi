#pragma once 
#include <iostream>
#include <vector>

class Piece{
private:
	//回転データと表裏
	int out[2];
	//渡された向き
	int way;
public:
	//文字列データ
	std::vector<int> str;
  //石データ
	std::vector<std::vector<int> > parts;

	//入力データを格納
	void set();
	//0~3表で回転, 4~7裏で回転
	void routen(int);
	//文字列の更新
	void piece_update();
	//再帰(1の場所を探索
	void search(int, int, int[][8], int);
};