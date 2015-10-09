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
	//�΂̐�
	int sum;
	//�}�b�v�f�[�^
	int map[1122];
	//�΃f�[�^
	Piece parts[256];

	//�΃f�[�^�@�u���b�N��
	int partscount[256];
	//�΃f�[�^�@�u���b�N����
	int parts_sum;
	//�΂̊O���̋�}�X��
	int spacescount[256];

	//���_
	int score;

	//�}�b�v�f�[�^ �E�� ����
	int map_left_up[2];
	int map_right_down[2];

	int counter, try_counter, width, height;

public:
	algorithm();
	//�t�@�C���̓���
	void input_file();
	//�t�@�C���֏o��
	void output_file();
	//�A���S���Y�����s
	//������
	void try_piece(int);
	void try_first_piece(int);

	//�R���\�[���o��
	void consolout();
};