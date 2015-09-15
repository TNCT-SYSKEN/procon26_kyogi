#pragma once;

class Map{
private:
	//石の総数
  int sum;
  //マップデータ
	std::vector<std::vector<int> > map;
public:
	void set(std::vector<std::vector<int> >);
};