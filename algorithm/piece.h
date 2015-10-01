#pragma once 
#include <iostream>
#include <vector>

class Piece{
private:
	//��]�f�[�^�ƕ\��
	int out[2];
	//�n���ꂽ����
	int way;
public:
	//������f�[�^
	std::vector<int> str;
  //�΃f�[�^
	std::vector<std::vector<int> > parts;

	//���̓f�[�^���i�[
	void set();
	//0~3�\�ŉ�], 4~7���ŉ�]
	void routen(int);
	//������̍X�V
	void piece_update();
	//�ċA(1�̏ꏊ��T��
	void search(int, int, int[][8], int);
};