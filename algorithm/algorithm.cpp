#include<iostream>
#include<fstream>
#include<vector>
#include<string>
#include<math.h>
#include"algorithm.h"
using namespace std;

//sum_part, ranking, parts

algorithm::algorithm() {
	map.reserve(32);
	for (int i = 0; i < 32; i++)
		map[i].reserve(32);

	out.reserve(10);
	for (int i = 0; i < 10; i++)
		out[i].reserve(4);

	parts.reserve(10);
	for (int i = 0; i < 8; i++) {
		parts[i].reserve(8);
		for (int j = 0; j < 8; j++) {
			parts[i][j].reserve(8);
		}
	}
	sum_part = 0;
}

void algorithm::run(){
	//�D�揇�ʂ��i�[
	vector<int> ranking(sum_part);
	
	rank();
}

void algorithm::rank(){
}

void algorithm::input_file(){
  //�t�@�C�����̓X�g���[���̏�����
  ifstream ifs("input.txt");
  string line;
  int sum=0, t=0;
	//1�t�@�C���̒��g����s���ǉ�
  while (getline(ifs, line)){
	  if (line.size() == 32){
			for (int i = 0; i < 32; i++)
				map[t][i] = (int)line[i] - (int)'0';
			t++;
		}
		else if (line.size() == 8){
			if ((sum + 1) == (int)parts.size())
				parts.push_back(vector<vector<int> >(8, vector<int>(8)));
			for (int i = 0; i < 8; i++)
			if (t == 7){
				sum++;
			}
			t++;
		}
		else if (line.size() != 0){
			for (int i = 0; i < (int)line.size(); i++){
				sum_part += (int)pow(10, i) * ((int)line[line.size() - i - 1] - (int)'0');
			}
		}
		else
			t = 0;
  }
}

void algorithm::output_file(){
	ofstream ofs("output.txt");
	cout << out.size() << endl;
	for(int i=0;i<(int)out.size();i++){
		if(i!=0)	ofs << endl;
		
		if(out[i][3] == 0)//�\
			ofs << out[i][0] << " " << out[i][1] << " H " << out[i][4];
		else if(out[i][3] == 1)//��
			ofs << out[i][0] << " " << out[i][1] << " H " << out[i][4];
	}
}