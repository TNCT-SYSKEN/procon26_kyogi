#pragma once 
#include <iostream>
#include <vector>

class Piece{
private:
	//回転データと表裏
	int out[2];
public:
  //石データ
	std::vector<std::vector<int> > parts;
	//入力データを格納
	void set();
	//0~3表で回転, 4~7裏で回転
	void routen(int);
};