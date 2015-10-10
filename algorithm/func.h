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
//敷地の場所を返す
void search_adress(Map& map, Piece& piece, std::vector<std::vector<int> > ,int i, std::vector<int>::iterator, int);
//一種類のピースに対していろいろ試す
void put(Piece& piece, Map& map);
//敷き詰められるか判定し、敷き詰められる場合は敷き詰めていく
bool check(Map&, Piece&, std::vector<std::vector<int> >);