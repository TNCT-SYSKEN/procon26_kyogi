#pragma once
#include <vector>

class algorithm{
private:
	//�΂̑���
  int sum_part;
  //�}�b�v�f�[�^
	std::vector<std::vector<int> > map;
  //���̓f�[�^
	std::vector<std::vector<std::vector<int> > > parts;
  //�o�̓f�[�^
	std::vector<std::vector<int> > out;

public:
    algorithm();
	//�t�@�C���̓���
	void input_file();
	//�t�@�C���֏o��
	void output_file();
	//�A���S���Y�����s
	void run();
	//���ʕt��
	void rank();
};

//extern void algorithm(int sum_part, vector<vector<int> >&map(32),
	//vector<vector<vector<int> > >&parts(10, vector<vector<int>(8, vector<int>(8) >, vector<vector<int> >& out(10, vector<int>(4));
//void algorithm(algorithm &);