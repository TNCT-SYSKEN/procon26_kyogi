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