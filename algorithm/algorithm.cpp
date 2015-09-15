#include<iostream>
#include<fstream>
#include<vector>
#include<cmath>
#include"algorithm.h"
#include"func.h"

algorithm::algorithm() {

}

void algorithm::run(){
	//—Dæ‡ˆÊ‚ğŠi”[
	std::vector<int> ranking(sum);
	screen(map);
}

void algorithm::rank(){
}

void algorithm::set(std::vector<std::vector<int> > Map, std::vector<std::vector<std::vector<int> >>Parts, int Sum){
	sum = Sum;
	map.resize(32);
	for (int i = 0; i < 32; i++)
		map[i].resize(32);
	parts.resize(Parts.size());
	for (int i = 0; i < (int)Parts.size(); i++){
		parts[i].resize(8);
		for (int j = 0; j < 8; j++)
			parts[i][j].resize(8);
	}
	for (int i = 0; i < 32; i++)
		for (int j = 0; j < 32; j++)
			map[i][j] = Map[i][j];
	for (int i = 0; i < (int)parts.size(); i++)
		for (int j = 0; j < 8; j++)
			for (int k = 0; k < 8; k++)
				parts[i][j][k] = Parts[i][j][k];
	out.resize(sum);
	for (int i = 0; i < sum; i++)
		out[i].resize(4, 0);
}

std::vector<std::vector<int> > algorithm::get_out(){
	return out;
}