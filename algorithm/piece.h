#pragma once 
#include <iostream>
#include <vector>

class Piece{
private:
	//��]�f�[�^�ƕ\��
	int out[2];
public:
  //�΃f�[�^
	std::vector<std::vector<int> > parts;
	//���̓f�[�^���i�[
	void set();
	//0~3�\�ŉ�], 4~7���ŉ�]
	void routen(int);
};