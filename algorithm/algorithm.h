#pragma once
#include <vector>

class algorithm{
private:
	//�΂̑���
  int sum_part;
  //�}�b�v�f�[�^
	std::vector<std::vector<int> > map(32, std::vector<int>(32));
  //���̓f�[�^
	std::vector<std::vector<std::vector<int> > > parts(10, std::vector<std::vector<int> >(8, std::vector<int>(8)));
  //�o�̓f�[�^
	std::vector<std::vector<int> > out(10, std::vector<int>(4));

public:
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