#include <iostream>
#include <vector>

#include "piece.h"


void Piece::routen(int pattern){
	if ((pattern > 3 && out[1] == 0) || (pattern <= 3 && out[1] == 1)){
		out[1]++;
		out[1] %= 2;
		for (int i = 0; i < 8; i++){
			for (int j = 0; j < 4; j++){
				int val = parts[i][j];
				parts[i][j] = parts[i][7 - j];
				parts[i][7 - j] = val;
			}
		}
	}
	int time = abs(pattern % 4 - out[0]);
	out[0] = pattern % 4;
	for (int s = 0; s < time; s++){
		std::vector<std::vector<int> > v(4, std::vector<int>(4));
		//4•ªŠ„‚µ‚Ä“ü‚ê‘Ö‚¦‚é
		for (int i = 0; i < 4; i++)
			for (int j = 0; j < 4; j++)
				v[i][j] = parts[i][j];
		for (int i = 0; i < 4; i++)
			for (int j = 0; j < 4; j++)
				parts[i][j] = parts[7 - j][i];
		for (int i = 0; i < 4; i++)
			for (int j = 0; j < 4; j++)
				parts[7 - j][i] = parts[7 - i][7 - j];
		for (int i = 0; i < 4; i++)
			for (int j = 0; j < 4; j++)
				parts[7 - i][7 - j] = parts[j][7 - i];
		for (int i = 0; i < 4; i++)
			for (int j = 0; j < 4; j++)
				parts[j][7 - i] = v[i][j];
	}
}

void Piece::set(){
	out[0] = 0;
	out[1] = 0;
	int x[4] = { -1, 0, 1, 0 };
	int y[4] = { 0, 1, 0, -1 };

	int val[8][8] = { 0 };
	for (int i = 0; i < 8; i++){
		for (int j = 0; j < 8; j++){
			if (parts[i][j] == 1){
				int sum = 0;
				for (int s = 0; s < 4; s++){
					int X = j + x[s];
					int Y = i + y[s];
					if (Y >= 0 && Y < 8 && X >= 0 && X < 8 && parts[Y][X] == 1)
						sum++;
				}
				val[i][j] = 4 - sum;
			}
		}
	}
	for (int i = 0; i < 8; i++){
		for (int j = 0; j < 8; j++){
			parts[i][j] = val[i][j];
		}
	}
}