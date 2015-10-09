using namespace std;

typedef struct {
	int pos[16];
	int adj[32];
	int location[2];
	char side;
	int angle;
} MinoPos;

typedef struct {
	char name;
	int used;
	int formsize;
	int position;
	bool put;
	int angle;
	MinoPos form[8];
} Piece;

class algorithm{
private:
	//石の数
	int sum;
	//マップデータ
	int map[1122];
	//石データ
	Piece parts[256];

	//石データ　ブロック数
	int partscount[256];
	//石データ　ブロック総数
	int parts_sum;
	//石の外周の空マス数
	int spacescount[256];

	//得点
	int score;

	//マップデータ 右上 左下
	int map_left_up[2];
	int map_right_down[2];

	int counter, try_counter, width, height;

public:
	algorithm();
	//ファイルの入力
	void input_file();
	//ファイルへ出力
	void output_file();
	//アルゴリズム実行
	//総当り
	void try_piece(int);
	void try_first_piece(int);

	//コンソール出力
	void consolout();
};