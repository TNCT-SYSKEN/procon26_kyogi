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
	for (int k = 0; k < (int)parts.size(); k++)
		for (int i = 0; i < 8; i++)
			for (int j = 0; j < 8; j++)
				parts[k][i][j] = Parts[k][i][j];

	out.resize(sum);
	for (int i = 0; i < sum; i++)
		out[i].resize(4, 0);

	int x[4] = { -1, 0, 1, 0 };
	int y[4] = { 0, 1, 0, -1 };

	for (int k = 0; k < sum; k++){
		int val[8][8] = { 0 };
		for (int i = 0; i < 8; i++){
			for (int j = 0; j < 8; j++){
				if (parts[k][i][j] == 1){
					int sum = 0;
					for (int s = 0; s < 4; s++){
						int X = j + x[s];
						int Y = i + y[s];
						if (Y >= 0 && Y < 8 && X >= 0 && X < 8 && parts[k][Y][X] == 1)
							sum++;
					}
					val[i][j] = 4 - sum;
				}
			}
		}
		for (int i = 0; i < 8; i++){
			for (int j = 0; j < 8; j++){
				parts[k][i][j] = val[i][j];
			}
		}
		for (int i = 0; i < 8; i++)
			for (int j = 0; j < 8; j++)
				parts[k][i][j] = val[i][j];

	}
}

std::vector<std::vector<int> > Piece::get_out(){
	return out;
}

