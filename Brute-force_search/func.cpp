#include <vector>
#include <iostream>
#include <windows.h>

void screen(std::vector<std::vector<int> > map){
	system("cls");
	for (int i = 0; i < 34; i++)
		printf("_");
	printf("\n");
	for (int i = 0; i < 32; i++){
		printf("|");
		for (int j = 0; j < 32; j++)
			if (map[i][j] == 1)
				printf("X");
			else if (map[i][j] == 0)
				printf("-");
			else
				printf("O");
			printf("|");
			printf("\n");
	}
	for (int i = 0; i < 34; i++)
		printf("_");
	printf("\n");
}