#pragma once 
#include <iostream>
#include <vector>

class Piece{
private:
public:
  //石データ
	std::vector<std::vector<int> > parts;
	//入力データを格納
	void set();
};