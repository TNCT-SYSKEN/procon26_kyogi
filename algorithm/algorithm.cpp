#include<iostream>
#include<fstream>
#include<vector>
#include<string>
#include<math.h>
#include"algorithm.h"
using namespace std;

algorithm::algorithm() {
	map.resize(32);
	for (int i = 0; i < 32; i++)
		map[i].resize(32);

	out.resize(10);
	for (int i = 0; i < 10; i++)
		out[i].resize(4, 0);

	parts.resize(10);
	for (int i = 0; i < 8; i++) {
		parts[i].resize(8);
		for (int j = 0; j < 8; j++) {
			parts[i][j].resize(8);
		}
	}
	
	sum_part = 0;
}

void algorithm::run(){
	//優先順位を格納
	vector<int> ranking(sum_part);
	
	rank();
}

void algorithm::rank(){
}

void algorithm::input_file(){
  //ファイル入力ストリームの初期化
  ifstream ifs("input.txt");
  string line;
  int sum=0, t=0;
	//1ファイルの中身を一行ずつ追加
  while (getline(ifs, line)){

	  if (line.size() == 32){
			for (int i = 0; i < 32; i++)
				map[t][i] = (int)line[i] - (int)'0';
			t++;
		}
		else if (line.size() == 8){
			if (sum == (int)parts.size()){
				cout << parts.size();
				//parts.push_back(vector<vector<int> >(8, vector<int>(8)));
				cout << parts.size();
				out.push_back(vector<int>(4));
			}
			for (int i = 0; i < 8; i++){
				//parts[sum][t][i] = (int)(line[i] - '0');
				cout << line[i] << " ";
			}
			cout << endl;
			if (t == 7){
				sum++;
				cout <<  endl;
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
	cout << parts.size() << endl;
	

	for(int i=0;i<(int)out.size();i++){
		if(i!=0)	ofs << endl;
		
		if(out[i][2] == 0)//表
			ofs << out[i][0] << " " << out[i][1] << " H " << out[i][3];
		else if(out[i][2] == 1)//裏
			ofs << out[i][0] << " " << out[i][1] << " H " << out[i][3];
	}
}