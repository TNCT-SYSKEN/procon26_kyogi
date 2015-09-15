#include<iostream>
#include<fstream>
#include<vector>
#include<string>
#include<math.h>
#include"algorithm.h"
#include"func.h"
using namespace std;

int main(){
	algorithm p;

  input(p);

	p.run();
	
	output(p.get_out());

  //ドラッグ＆ドロップ用
  /*ifstream ifs(argv[1], 1);
  if (ifs.bad())return -1;
  string line;
  */
}
