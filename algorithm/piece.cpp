#include <iostream>
#include <vector>

#include "piece.h"


void Piece::set(){
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