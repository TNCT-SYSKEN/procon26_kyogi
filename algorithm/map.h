#pragma once;

class Map{
private:
  //マップデータ
	std::vector<std::vector<int> > map;
public:
	//隣接している数を格納
	std::vector<std::vector<int> > v;
	//データをセットmapに
	void set(std::vector<std::vector<int> >);
	//隣接数の更新
	void updata_v();
};