#pragma once;
#include <vector>
#include <map>

#include "piece.h"
#include "map.h"

class Map{
private:
  //�}�b�v�f�[�^
	std::vector<std::vector<int> > map;
	//���łɕ~���Ă�s�[�X�̗אڂ��Ă����}�X�̃A�h���X���i�[
	std::vector<std::pair<int,int> > adress;
public:
	//�אڂ��Ă��鐔���i�[
	std::vector<std::vector<int> > v;
	//�f�[�^���Z�b�gmap��
	void set(std::vector<std::vector<int> >);
	//�אڐ��̍X�V
	void updata_v();
	//�~�����Ƃ��ł���Ε~��
	bool lay(Piece, int, int, int);
	//���łɃs�[�X��~���Ă��邩
	bool already();
};