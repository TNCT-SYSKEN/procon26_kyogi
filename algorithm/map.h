#pragma once;

class Map{
private:
  //�}�b�v�f�[�^
	std::vector<std::vector<int> > map;
public:
	//�אڂ��Ă��鐔���i�[
	std::vector<std::vector<int> > v;
	//�f�[�^���Z�b�gmap��
	void set(std::vector<std::vector<int> >);
	//�אڐ��̍X�V
	void updata_v();
};