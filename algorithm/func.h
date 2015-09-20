#pragma once
#include <vector>
#include "map.h"
#include "piece.h"

//テキストからの入力
void input_sum(int&);
void input(Map&, std::vector<Piece>&, int);
//テキストに出力
void output(std::vector<std::vector<int> >);
//敷地の出力 0:空 / 1:あり
void screen(std::vector<std::vector<int> >);
//敷地の空の隣接辺を数を表示
void screen_v(std::vector<std::vector<int> >);