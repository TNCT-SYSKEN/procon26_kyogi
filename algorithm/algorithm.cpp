#include<iostream>
#include<fstream>
#include<vector>
#include<string>
#include<cmath>
#include <cstdlib>
#include"algorithm.h"
#include"func.h"

algorithm::algorithm() {
	map.resize(32);
	for (int i = 0; i < 32; i++)
		map[i].resize(32);
}

void algorithm::run(){
	//�D�揇�ʂ��i�[
	std::vector<int> ranking(sum);
	screen(map);
}

void algorithm::rank(){
}

void algorithm::input_file(){
  //�t�@�C�����̓X�g���[���̏�����
	std::ifstream ifs("input.txt");
	std::string line;
	int N=0, t=0;
	//1�t�@�C���̒��g����s���ǉ�
	while (getline(ifs, line)){
		if (line.size() == 32){
			for (int i = 0; i < 32; i++)
				map[t][i] = (int)line[i] - (int)'0';
			t++;
		}
		else if (line.size() == 8){
			for (int i = 0; i < 8; i++)
				parts[N][t][i] = (int)(line[i] - '0');
			if (t == 7){
				N++;
			}
			t++;
		}
		else if (line.size() != 0){
			sum = atoi(line.c_str());
			//�΂Əo�͗p�̗v�f���Ƃ�
			parts.resize(sum);
			for (int i = 0; i < sum; i++) {
				parts[i].resize(8);
				for (int j = 0; j < 8; j++) {
					parts[i][j].resize(8);
				}
			}
			out.resize(sum);
			for (int i = 0; i < sum; i++)
				out[i].resize(4, 0);
		}
		else
			t = 0;
	}
}

void algorithm::output_file(){
	std::ofstream ofs("output.txt");
	std::cout << out.size() << std::endl;
	std::cout << parts.size() << std::endl;
	

	for(int i=0;i<(int)out.size();i++){
		if(i!=0)	ofs << std::endl;
		
		if(out[i][2] == 0)//�\
			ofs << out[i][0] << " " << out[i][1] << " H " << out[i][3];
		else if(out[i][2] == 1)//��
			ofs << out[i][0] << " " << out[i][1] << " H " << out[i][3];
	}
}