#include<iostream>
#include<fstream>
#include<string>
#include<stack>
#include<math.h>
#include<vector>
#include<memory>
#include<algorithm>
#include"algorithm.h"
const int size = 32;
const int MAX = 1000;
const int BLOCK = 0;

algorithm::algorithm()
{
	map_left_up[0] = 32;
	map_left_up[1] = 32;
	map_right_down[0] = 0;
	map_right_down[1] = 0;
	partscount[0] = 0;
	parts_sum = 0;
	score = 100000;
	counter = 0;
	try_counter = 0;
}

void algorithm::try_piece(int level){
	int i, j, k, l, m, x, before, max, formsize, space, position;
	bool continue_flag = false;
	bool put_flag[1122];

	try_counter++;
	max = partscount[level];
	formsize = parts[level].formsize;
	for (l = 0; l < formsize; l++){
		for (i = 0; i < level; i++){
			if (!parts[i].put)
				continue;
			for (space = 0; space < spacescount[i]; space++){
				if (map[parts[i].form[parts[i].angle].adj[space] + parts[i].position] != 0)
					continue;
				x = parts[i].form[parts[i].angle].adj[space] + parts[i].position;
				for (k = 0; k < max; k++){
					for (m = 0; m < max; m++){
						if (map[x + parts[level].form[l].pos[m] - parts[level].form[l].pos[k]]){
							continue_flag = true;
							break;
						}
					}
					if (!continue_flag){
						if (put_flag[x + parts[level].form[l].pos[0] - parts[level].form[l].pos[k]] == false)
							continue_flag = true;
					}
					if (continue_flag){
						continue_flag = false;
						continue;
					}
					put_flag[x + parts[level].form[l].pos[0] - parts[level].form[l].pos[k]] = false;
					// ピースを置く
					for (m = 0; m < max; m++){ map[x + parts[level].form[l].pos[m] - parts[level].form[l].pos[k]] = level + 1; }
					parts[level].used = 1;
					parts[level].position = x + parts[level].form[l].pos[0] - parts[level].form[l].pos[k];
					parts[level].put = true;
					parts[level].angle = l;
					// すべてのピースを置ききったらTrueを返す
					if (level == sum - 1){
						score = parts_sum;
						counter++;
						output_file();
						// ピースを戻す
						for (m = 0; m < max; m++){ map[x + parts[level].form[l].pos[m] - parts[level].form[l].pos[k]] = 0; }
						parts[level].used = 0;
						parts[level].position = 0;
						parts[level].put = false;
						return;
					}
					try_piece(level + 1);
					// ピースを戻す
					for (m = 0; m < max; m++){ map[x + parts[level].form[l].pos[m] - parts[level].form[l].pos[k]] = 0; }
					parts[level].used = 0;
					parts[level].position = 0;
					parts[level].put = false;
					if (parts_sum == score)
						return;
				}
			}
		}
	}
	parts[level].used = 1;
	parts_sum += partscount[level];
	if (parts_sum >= score){
		parts_sum -= partscount[level];
		return;
	}
	if (level != sum - 1){
		try_piece(level + 1);
		parts_sum -= partscount[level];
		return;
	}
	// すべてのピースを置ききったらTrueを返す（recursiveコールの終了）
	score = parts_sum;
	counter++;
	output_file();
	parts_sum -= partscount[level];
	return;
}

void algorithm::try_first_piece(int level){
	int i, j, k, l, m, x, max, formsize;
	bool continue_flag = false;

	try_counter++;
	for (i = 0; i < sum; i++){
		max = partscount[i];
		formsize = parts[i].formsize;
		for (j = 0; j < formsize; j++){
			for (k = map_left_up[1]; k < map_right_down[1]; k++){
				for (l = map_left_up[0]; l < map_right_down[0]; l++){
					if (map[k * 33 + l] == 0){
						x = k * 33 + l;
						for (m = 1; m < max; m++){
							if (map[x + parts[i].form[j].pos[m]]){
								continue_flag = true;
								break;
							}
						}
						if (continue_flag){
							continue_flag = false;
							continue;
						}
						// ピースを置く
						for (m = 0; m < max; m++){ map[x + parts[i].form[j].pos[m]] = i + 1; }
						parts[i].used = 1;
						parts[i].position = x;
						parts[i].put = true;
						parts[i].angle = j;
						// 次のピースを試す
						try_piece(level + 1);
						// ピースを戻す
						for (m = 0; m < max; m++){ map[x + parts[i].form[j].pos[m]] = 0; }
						parts[i].used = 0;
						parts[i].position = 0;
						parts[i].put = false;
					}
				}
			}
		}
		parts[i].used = 1;
		parts[i].put = false;
		parts_sum += partscount[i];
	}
}

void algorithm::input_file(){
	//ファイル入力ストリームの初期化
	ifstream ifs("input.txt");
	string line;
	int N = 0, t = 1, count = 0, i, j = 0, angle, standard[] = { 8, 8 }, formsize = 1, all_parts_sum = 0, n_count, space_count;
	bool add = true, space_add = true;
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
				}
				else
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
			for (i = 0; i < 8; i++)
				vir_parts[t][i] = (bool)(line[i] - '0');
			for (i = 0; i < 8; i++){
				if (vir_parts[t][i] == true)
					partscount[N]++;
			}
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
				for (i--; i < 8; i++, j = 0){
					if (count != partscount[N]){
						for (; j < 8; j++){
							if (vir_parts[i][j])
							{
								parts[N].form[0].pos[count++] = j - standard[0] + (i - standard[1]) * 33;
							}
						}
					}
					else
						break;
				}

				for (i = 0; i < partscount[N]; i++){
					for (j = 0; j < partscount[N]; j++){
						if (parts[N].form[0].pos[j] == parts[N].form[0].pos[i] - 33){
							space_add = false;
							break;
						}
					}
					if (space_add)
						parts[N].form[0].adj[space_count++] = parts[N].form[0].pos[i] - 33;
					space_add = true;
				}
				for (i = 0; i < partscount[N]; i++){
					for (j = 0; j < partscount[N]; j++){
						if (parts[N].form[0].pos[j] == parts[N].form[0].pos[i] + 33){
							space_add = false;
							break;
						}
					}
					for (j = 0; j < space_count; j++){
						if (parts[N].form[0].adj[j] == parts[N].form[0].pos[i] + 33){
							space_add = false;
							break;
						}
					}
					if (space_add)
						parts[N].form[0].adj[space_count++] = parts[N].form[0].pos[i] + 33;
					space_add = true;
				}
				for (i = 0; i < partscount[N]; i++){
					for (j = 0; j < partscount[N]; j++){
						if (parts[N].form[0].pos[j] == parts[N].form[0].pos[i] - 1){
							space_add = false;
							break;
						}
					}
					for (j = 0; j < space_count; j++){
						if (parts[N].form[0].adj[j] == parts[N].form[0].pos[i] - 1){
							space_add = false;
							break;
						}
					}
					if (space_add)
						parts[N].form[0].adj[space_count++] = parts[N].form[0].pos[i] - 1;
					space_add = true;
				}
				for (i = 0; i < partscount[N]; i++){
					for (j = 0; j < partscount[N]; j++){
						if (parts[N].form[0].pos[j] == parts[N].form[0].pos[i] + 1){
							space_add = false;
							break;
						}
					}
					for (j = 0; j < space_count; j++){
						if (parts[N].form[0].adj[j] == parts[N].form[0].pos[i] + 1){
							space_add = false;
							break;
						}
					}
					if (space_add)
						parts[N].form[0].adj[space_count++] = parts[N].form[0].pos[i] + 1;
					space_add = true;
				}

				spacescount[N] = space_count;
				parts[N].form[0].location[0] = standard[0];
				parts[N].form[0].location[1] = standard[1];
				parts[N].form[0].side = 'H';
				parts[N].form[0].angle = 0;
				count = 0;
				standard[0] = 8;
				standard[1] = 8;
				space_count = 0;
				for (angle = 1; angle < 8; angle++){
					switch (angle){
					case 1:
						for (i = 0; i < 8; i++){
							for (j = 0; j < 8; j++){
								vir_parts[i][j] = temp[7 - j][i];
							}
						}
						parts[N].form[formsize].side = 'H';
						parts[N].form[formsize].angle = 90;
						break;
					case 2:
						for (i = 0; i < 8; i++){
							for (j = 0; j < 8; j++){
								vir_parts[i][j] = temp[7 - i][7 - j];
							}
						}
						parts[N].form[formsize].side = 'H';
						parts[N].form[formsize].angle = 180;
						break;
					case 3:
						for (i = 0; i < 8; i++){
							for (j = 0; j < 8; j++){
								vir_parts[i][j] = temp[j][7 - i];
							}
						}
						parts[N].form[formsize].side = 'H';
						parts[N].form[formsize].angle = 270;
						break;
					case 4:
						for (i = 0; i < 8; i++){
							for (j = 0; j < 8; j++){
								vir_parts[i][j] = temp[i][7 - j];
							}
						}
						parts[N].form[formsize].side = 'T';
						parts[N].form[formsize].angle = 0;
						break;
					case 5:
						for (i = 0; i < 8; i++){
							for (j = 0; j < 8; j++){
								vir_parts[i][j] = temp[7 - j][7 - i];
							}
						}
						parts[N].form[formsize].side = 'T';
						parts[N].form[formsize].angle = 90;
						break;
					case 6:
						for (i = 0; i < 8; i++){
							for (j = 0; j < 8; j++){
								vir_parts[i][j] = temp[7 - i][j];
							}
						}
						parts[N].form[formsize].side = 'T';
						parts[N].form[formsize].angle = 180;
						break;
					case 7:
						for (i = 0; i < 8; i++){
							for (j = 0; j < 8; j++){
								vir_parts[i][j] = temp[j][i];
							}
						}
						parts[N].form[formsize].side = 'T';
						parts[N].form[formsize].angle = 270;
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
									parts[N].form[formsize].pos[count++] = j - standard[0] + (i - standard[1]) * 33;
							}
						}
						else
							break;
					}
					for (i = 0; i < formsize; i++)
						if (0 == memcmp(parts[N].form[i].pos, parts[N].form[formsize].pos, sizeof(int) * partscount[N])){
							add = false;
							break;
						}

					parts[N].form[formsize].location[0] = standard[0];
					parts[N].form[formsize].location[1] = standard[1];

					if (add){

						for (i = 0; i < partscount[N]; i++){
							for (j = 0; j < partscount[N]; j++){
								if (parts[N].form[formsize].pos[j] == parts[N].form[formsize].pos[i] - 33){
									space_add = false;
									break;
								}
							}
							if (space_add)
								parts[N].form[formsize].adj[space_count++] = parts[N].form[formsize].pos[i] - 33;
							space_add = true;
						}
						for (i = 0; i < partscount[N]; i++){
							for (j = 0; j < partscount[N]; j++){
								if (parts[N].form[formsize].pos[j] == parts[N].form[formsize].pos[i] + 33){
									space_add = false;
									break;
								}
							}
							for (j = 0; j < space_count; j++){
								if (parts[N].form[formsize].adj[j] == parts[N].form[formsize].pos[i] + 33){
									space_add = false;
									break;
								}
							}
							if (space_add)
								parts[N].form[formsize].adj[space_count++] = parts[N].form[formsize].pos[i] + 33;
							space_add = true;
						}
						for (i = 0; i < partscount[N]; i++){
							for (j = 0; j < partscount[N]; j++){
								if (parts[N].form[formsize].pos[j] == parts[N].form[formsize].pos[i] - 1){
									space_add = false;
									break;
								}
							}
							for (j = 0; j < space_count; j++){
								if (parts[N].form[formsize].adj[j] == parts[N].form[formsize].pos[i] - 1){
									space_add = false;
									break;
								}
							}
							if (space_add)
								parts[N].form[formsize].adj[space_count++] = parts[N].form[formsize].pos[i] - 1;
							space_add = true;
						}
						for (i = 0; i < partscount[N]; i++){
							for (j = 0; j < partscount[N]; j++){
								if (parts[N].form[formsize].pos[j] == parts[N].form[formsize].pos[i] + 1){
									space_add = false;
									break;
								}
							}
							for (j = 0; j < space_count; j++){
								if (parts[N].form[formsize].adj[j] == parts[N].form[formsize].pos[i] + 1){
									space_add = false;
									break;
								}
							}
							if (space_add)
								parts[N].form[formsize].adj[space_count++] = parts[N].form[formsize].pos[i] + 1;
							space_add = true;
						}

						formsize++;
					}
					add = true;
					space_add = true;
					count = 0;
					standard[0] = 8;
					standard[1] = 8;
					space_count = 0;
				}
#pragma endregion
				parts[N].formsize = formsize;
				parts[N].used = 0;
				parts[N].position = 0;
				parts[N].put = false;
				parts[N].angle = 0;
				all_parts_sum += formsize;
				N++;
				partscount[N] = 0;
				count = 0;
				formsize = 1;
				add = true;
				space_add = true;
				standard[0] = 8;
				standard[1] = 8;
				n_count = 0;
				space_count = 0;
			}
			t++;
			n_count = 0;
		}
		else if (line.size() != 0)
			sum = atoi(line.c_str());
		else{
			t = 0;
			j = 0;
			n_count = 0;
			space_count = 0;
		}
	}
	for (i = 0; i < 33; i++)
		map[i] = -1;
	for (i = 1089; i < 1122; i++)
		map[i] = -1;
	width = (map_right_down[0] - map_left_up[0] + 2);
	height = (map_right_down[1] - map_left_up[1] + 2);
}

void algorithm::output_file(){
	ofstream ofs("output.txt");

	for (int i = 0; i<sum; i++){
		if (!parts[i].put)	ofs << endl;

		ofs << parts[i].position % 33 - parts[i].form[parts[i].angle].location[0] << " " << parts[i].position / 33 - 1 - parts[i].form[parts[i].angle].location[1] << " " << parts[i].form[parts[i].angle].side << " " << parts[i].form[parts[i].angle].angle << endl;
	}
}

void algorithm::consolout()
{
	for (int i = 0; i < height; i++){
		for (int j = 0; j < width; j++){
			std::cout << map[i * 33 + j] << " ";
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