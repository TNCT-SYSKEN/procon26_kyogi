#include<vector>
using namespace std;

typedef struct {
	int pos[16];
} MinoPos;

typedef struct {
	char name;
	int used;
	int formsize;
	MinoPos form[8];
} Piece;

class algorithm{
private:
	//石の数
	int sum;
	//マップデータ
	int map[1089];
	//石データ
	Piece parts[256];
	//出力データ
	vector < vector < int > > out;

	//石データ　大きさ(1の数)
	int partscount[256];

	//マップデータ 右上 左下
	int map_left_up[2];
	int map_right_down[2];

	int counter, try_counter, width, height;
	clock_t start;

public:
	algorithm();
	//ファイルの入力
	void input_file();
	//ファイルへ出力
	void output_file();
	//アルゴリズム実行
	//総当り　
	//全解探索
	void try_piece(int);
	//一つ見つける
	bool try_once_piece(int);

	//追加
	void init_board();
	int board_index(int);

	//コンソール出力
	void consolout();
	void clock_out();
};