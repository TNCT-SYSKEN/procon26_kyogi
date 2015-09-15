#pragma once 
#include <iostream>
#include <vector>

class Piece{
private:
	//石の総数
  int sum;
  //石データ
	std::vector<std::vector<std::vector<int> > > parts;
  //出力データ
	std::vector<std::vector<int> > out;
public:
	void set(std::vector<std::vector<std::vector<int> >>, int);
	//ファイルへ出力
	std::vector<std::vector<int> > get_out();
};