#pragma once;
#include <vector>
#include <map>

#include "piece.h"
#include "map.h"

class Map{
private:
	//���łɕ~���Ă�s�[�X�̗אڂ��Ă����}�X�̃A�h���X���i�[
	std::vector<std::pair<int,int> > adress;
	//����
	int way;
	//search�E�o�p�̃t���O
	int flag;
	//���ڂ�
	int num;
public:
  //�}�b�v�f�[�^
	std::vector<std::vector<int> > map;
	//���͂̕�����
	std::vector<int> str;
	//������̎n�_�̍��W
	int X, Y;
	//�אڂ��Ă��鐔���i�[
	std::vector<std::vector<int> > v;

	//�f�[�^���Z�b�gmap��
	void set(std::vector<std::vector<int> >);
	//�אڐ��̍X�V
	void updata_v();

	/*�~���Ă����p*/
	//�~�����Ƃ��ł���Ε~��
	bool lay(Piece, int, int, int);
	//���łɃs�[�X��~���Ă��邩
	bool already();
};