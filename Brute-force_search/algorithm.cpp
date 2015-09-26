#include<iostream>
#include<fstream>
#include<string>
#include<stack>
#include<math.h>
#include<vector>
#include<time.h>
#include<memory>
#include<algorithm>
#include"algorithm.h"
#include"func.h"
const int size = 32;
const int MAX = 1000;
const int BLOCK = 0;

algorithm::algorithm()
{
	map_left_up[0] = 32;
	map_left_up[1] = 32;
	map_right_down[0] = 0;
	map_right_down[1] = 0;
}

void algorithm::init_board()
{
	int i, j;

	for (i = 0; i<height; i++){
		for (j = 0; j < width; j++){
			map[33 * i + j] = 0;
			if (((j + 1) % width) == 0 || i > height - 2){
				map[33 * i + j] = -1;
			}
		}
	}
	start = clock();
}

int algorithm::board_index(int find_num)
{
	int i, j;

	for (i = 0; i < height; i++){
		for (j = 0; j < width; j++){
			if (map[i*33+j] == find_num){
				return i * 33 + j;
			}
		}
	}
	return 0;
}

bool algorithm::try_once_piece(int level){
	int i, j, k, l, x, max, formsize;
	bool continue_flag = false;
	bool next_flag = false;

	try_counter++;
	x = board_index(0);
	for (i = 0; i<sum; i++){
		if (parts[i].used == 1){ continue; }
		max = partscount[i];
		formsize = parts[i].formsize;
		for (j = 0; j<formsize; j++){
			for (l = 0; l < max; l++){
				if (map[x + parts[i].form[j].pos[l]]){
					continue_flag = true;
					break;
				}
			}
			if (continue_flag){
				continue_flag = false;
				continue;
			}
			// ピースを置く
			for (k = 0; k<max; k++){ map[x + parts[i].form[j].pos[k]] = i + 1; }
			parts[i].used = 1;
			// すべてのピースを置ききったらTrueを返す（recursiveコールの終了）
			if (level == sum - 1){
				counter++;
				// ピースを戻す
				for (k = 0; k<max; k++){ map[x + parts[i].form[j].pos[k]] = 0; }
				parts[i].used = 0;
				return true;
			}
			// 次のピースを試す
			next_flag = try_once_piece(level + 1);
			if (next_flag)
				return true;
			// ピースを戻す
			for (k = 0; k<max; k++){ map[x + parts[i].form[j].pos[k]] = 0; }
			parts[i].used = 0;
		}
	}
	return false;
}

void algorithm::try_piece(int level){
	int i, j, k, l, x, max, formsize;
	bool continue_flag = false;

	try_counter++;
	x = board_index(0);
	for (i = 0; i<sum; i++){
		if (parts[i].used == 1){ continue; }
		max = partscount[i];
		formsize = parts[i].formsize;
		for (j = 0; j<formsize; j++){
			for (l = 0; l < max; l++){
				if (map[x + parts[i].form[j].pos[l]]){
					continue_flag = true;
					break;
				}
			}
			if (continue_flag){
				continue_flag = false;
				continue;
			}
			// ピースを置く
			for (k = 0; k<max; k++){ map[x + parts[i].form[j].pos[k]] = i + 1; }
			parts[i].used = 1;
			// すべてのピースを置ききったらTrueを返す（recursiveコールの終了）
			if (level == sum - 1){
				counter++;
				// ピースを戻す
				for (k = 0; k<max; k++){ map[x + parts[i].form[j].pos[k]] = 0; }
				parts[i].used = 0;
				return;
			}
			// 次のピースを試す
			try_piece(level + 1);
			// ピースを戻す
			for (k = 0; k<max; k++){ map[x + parts[i].form[j].pos[k]] = 0; }
			parts[i].used = 0;
		}
	}
}

void algorithm::input_file(){
	//ファイル入力ストリームの初期化
	ifstream ifs("input.txt");
	string line;
	int N = 0, t = 0, count = 0, i, j = 0, angle, standard[] = { 8, 8 }, formsize = 1, all_parts_sum = 0, n_count;
	bool add = true;
	bool vir_parts[8][8];
	//1ファイルの中身を一行ずつ追加
	while (getline(ifs, line)){
		if (line.size() == 32){
			for (j = 0; j < 32; j++){
				if (0 == (int)(line[j] - '0')){
					map[t * 33 + j] = 0;
					if (j < map_left_up[0]){
						if (map_left_up[1] == 32)
							map_left_up[1] = t;
						map_left_up[0] = j;
					}
					map_right_down[1] = t;
					break;
				}else
					map[t * 33 + j] = -1;
			}
			for (; j < 32; j++){
				if (0 == (bool)(line[j] - '0')){
					map[t * 33 + j] = 0;
					if (j > map_right_down[0])
						map_right_down[0] = j;
				}
				else{
					map[t * 33 + j] = -1;
				}
			}
			map[t * 33 + j] = -1;
			t++;
		}
		else if (line.size() == 8){
			for ( i = 0; i < 8; i++)
				vir_parts[t][i] = (bool)(line[i] - '0');
			for (i = 0; i < 8; i++)
				if (vir_parts[t][i] == true)
					n_count++;
			partscount[N] += n_count;
			if (t == 7){
				bool temp[8][8] = { 0 };
				//回転・反転の座標を保存
				#pragma region Rotation/Reversal
				for (i = 0; i<8; i++){
					for (j = 0; j<8; j++){
						temp[i][j] = vir_parts[i][j];
					}
				}
				for (i = 0; i < 8; i++){
					if (standard[0] == 8)
					{
						for (j = 0; j < 8; j++){
							if (vir_parts[i][j])
							{
								standard[0] = j++;
								standard[1] = i;
								parts[N].form[0].pos[count++] = 0;
								break;
							}
						}
					}
					else
						break;
				}
				for (i--; i < 8; i++, j=0){
					if (count != partscount[N]){
						for (; j < 8; j++){
							if (vir_parts[i][j])
							{
								parts[N].form[0].pos[count++] = j - standard[0] + (i - standard[1]) * 33;
							}
						}
					}
					else
					{
						break;
					}
				}
				count = 0;
				standard[0] = 8;
				standard[1] = 8;
				for (angle = 1; angle < 8; angle++){
					switch (angle){
					case 1:
						for (i = 0; i < 8; i++){
							for (j = 0; j < 8; j++){
								vir_parts[i][j] = temp[7 - j][i];
							}
						}
						break;
					case 2:
						for (i = 0; i < 8; i++){
							for (j = 0; j < 8; j++){
								vir_parts[i][j] = temp[7 - i][7 - j];
							}
						}
						break;
					case 3:
						for (i = 0; i < 8; i++){
							for (j = 0; j < 8; j++){
								vir_parts[i][j] = temp[j][7 - i];
							}
						}
						break;
					case 4:
						for (i = 0; i < 8; i++){
							for (j = 0; j < 8; j++){
								vir_parts[i][j] = temp[i][7 - j];
							}
						}
						break;
					case 5:
						for (i = 0; i < 8; i++){
							for (j = 0; j < 8; j++){
								vir_parts[i][j] = temp[7 - j][7 - i];
							}
						}
						break;
					case 6:
						for (i = 0; i < 8; i++){
							for (j = 0; j < 8; j++){
								vir_parts[i][j] = temp[7 - i][j];
							}
						}
						break;
					case 7:
						for (i = 0; i < 8; i++){
							for (j = 0; j < 8; j++){
								vir_parts[i][j] = temp[j][i];
							}
						}
						break;
					}
					for (i = 0; i < 8; i++){
						if (standard[0] == 8)
						{
							for (j = 0; j < 8; j++){
								if (vir_parts[i][j])
								{
									standard[0] = j++;
									standard[1] = i;
									parts[N].form[formsize].pos[count++] = 0;
									break;
								}
							}
						}
						else
							break;
					}
					for (i--; i < 8; i++, j = 0){
						if (count != partscount[N]){
							for (; j < 8; j++){
								if (vir_parts[i][j])
								{
									parts[N].form[formsize].pos[count++] = j - standard[0] + (i - standard[1]) * 33;
								}
							}
						}
						else
						{
							break;
						}
					}
					for (i = 0; i < formsize; i++)
						if (0 == memcmp(parts[N].form[i].pos, parts[N].form[formsize].pos, sizeof(int) * partscount[N])){
							add = false;
							break;
						}
					if (add){
						formsize++;
					}
					add = true;
					count = 0;
					standard[0] = 8;
					standard[1] = 8;
				}
				#pragma endregion
				parts[N].formsize = formsize; 
				all_parts_sum += formsize;
				N++;
				count = 0;
				formsize = 1;
				add = true;
				standard[0] = 8;
				standard[1] = 8;
				n_count = 0;
			}
			t++;
			n_count = 0;
		}
		else if (line.size() != 0){
			sum = atoi(line.c_str());
			//石と出力用の要素をとる
			out.resize(sum);
			for (int i = 0; i < sum; i++)
				out[i].resize(4, 0);
		}
		else{
			t = 0;
			j = 0; 
			n_count = 0;
		}
	}
	width = (map_right_down[0] - map_left_up[0] + 2);
	height = (map_right_down[1] - map_left_up[1] + 2);
}

void algorithm::output_file(){
	ofstream ofs("output.txt");
	std::cout << out.size() << std::endl;
	//cout << parts.size() << endl;


	for (int i = 0; i<(int)out.size(); i++){
		if (i != 0)	ofs << endl;

		if (out[i][2] == 0)//表
			ofs << out[i][0] << " " << out[i][1] << " H " << out[i][3];
		else if (out[i][2] == 1)//裏
			ofs << out[i][0] << " " << out[i][1] << " H " << out[i][3];
	}
}

void algorithm::consolout()
{
	for (int i = 0; i < height; i++){
		for (int j = 0; j < width; j++){
			std::cout << map[i*33+j] << " ";
		}
		std::cout << std::endl;
	}
	std::cout << std::endl;
	std::cout << sum << std::endl;
	for (int i = 0; i<sum; i++){
		for (int k = 0; k < partscount[i]; k++)
			std::cout << parts[i].form[0].pos[k] << " ";
		std::cout << std::endl;
		std::cout << partscount[i] << std::endl;
		std::cout << std::endl;
	}
	std::cout << "解合計: " << counter << std::endl;
	//操作数をできるだけ減らす必要がある
	std::cout << "操作数: " << try_counter << std::endl;
}

void algorithm::clock_out(){
	std::cout << (double)(clock() - start) / CLOCKS_PER_SEC << "秒" << std::endl;
}