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
	//�΂̐�
	int sum;
	//�}�b�v�f�[�^
	int map[1089];
	//�΃f�[�^
	Piece parts[256];
	//�o�̓f�[�^
	vector < vector < int > > out;

	//�΃f�[�^�@�傫��(1�̐�)
	int partscount[256];

	//�}�b�v�f�[�^ �E�� ����
	int map_left_up[2];
	int map_right_down[2];

	int counter, try_counter, width, height;
	clock_t start;

public:
	algorithm();
	//�t�@�C���̓���
	void input_file();
	//�t�@�C���֏o��
	void output_file();
	//�A���S���Y�����s
	//������@
	//�S��T��
	void try_piece(int);
	//�������
	bool try_once_piece(int);

	//�ǉ�
	void init_board();
	int board_index(int);

	//�R���\�[���o��
	void consolout();
	void clock_out();
};