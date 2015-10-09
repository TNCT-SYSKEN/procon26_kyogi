#include<iostream>
#include<fstream>
#include<string>
#include<stack>
#include<math.h>
#include<vector>
#include<algorithm>
#include<Windows.h>
#include"algorithm.h"
using namespace std;

int main(void){
	algorithm p;

	p.input_file();

	p.try_first_piece(0);

	//p.consolout();
	cout << "finish" << endl;
	Sleep(10000000);

	return 0;

}
