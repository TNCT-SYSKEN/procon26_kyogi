#include<iostream>
#include<fstream>
#include<string>
#include<stack>
#include<math.h>
#include<vector>
#include<algorithm>
#include<Windows.h>
#include<time.h>
#include"algorithm.h"
using namespace std;

int main(void){
	algorithm p;

	p.input_file();

	p.init_board();
	//�S��
	p.try_piece(0);
	//���
	//p.try_once_piece(0);

	p.consolout();
	p.clock_out();
	Sleep(10000000);

	return 0;

}
