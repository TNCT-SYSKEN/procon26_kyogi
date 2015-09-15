#include <iostream>
#include <vector>

#include "piece.h"


void Piece::set(std::vector<std::vector<std::vector<int> >>Parts, int Sum){
	sum = Sum;
	parts.resize(Parts.size());
	for (int i = 0; i < (int)Parts.size(); i++){
		parts[i].resize(8);
		for (int j = 0; j < 8; j++)
			parts[i][j].resize(8);
	}

	for (int i = 0; i < (int)parts.size(); i++)
		for (int j = 0; j < 8; j++)
			for (int k = 0; k < 8; k++)
				parts[i][j][k] = Parts[i][j][k];
	out.resize(sum);
	for (int i = 0; i < sum; i++)
		out[i].resize(4, 0);
}

std::vector<std::vector<int> > Piece::get_out(){
	return out;
}

