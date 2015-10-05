#pragma once
#include <vector>
#include "map.h"
#include "piece.h"

//�e�L�X�g����̓���
void input_sum(int&);
void input(Map&, std::vector<Piece>&, int);
//�e�L�X�g�ɏo��
void output(std::vector<std::vector<int> >);
//�~�n�̏o�� 0:�� / 1:����
void screen(std::vector<std::vector<int> >);
//�~�n�̋�̗אڕӂ𐔂�\��
void screen_v(std::vector<std::vector<int> >);
//�~�n��������I�ȏꏊ��T��
void search_place(Map& map, Piece& piece, int&, int&);
//���ނ̃s�[�X�ɑ΂��Ă��낢�뎎��
void put(Piece& piece, Map& map);
//�~���l�߂��邩���肵�A�~���l�߂���ꍇ�͕~���l�߂Ă���
bool check(Map&, Piece&, int&, int&);