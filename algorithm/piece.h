#pragma once 
#include <iostream>
#include <vector>

class Piece{
private:
	//�΂̑���
  int sum;
  //�΃f�[�^
	std::vector<std::vector<std::vector<int> > > parts;
  //�o�̓f�[�^
	std::vector<std::vector<int> > out;
public:
	void set(std::vector<std::vector<std::vector<int> >>, int);
	//�t�@�C���֏o��
	std::vector<std::vector<int> > get_out();
};