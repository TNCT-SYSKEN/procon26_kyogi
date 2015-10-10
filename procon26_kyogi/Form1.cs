﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace procon26_kyogi
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        //定数
        const int MAP = 32, width = 17, length = 17, mass_begin = 203, mass_end = 882;

        //クリックフラグ
        int click_down_flag = 0, click_up_flag = 0;
        //ピースの総数
        int pieces;
        int count = 0;


        //Fontを作成
        Font fnt = new Font("ＭＳ ゴシック", 12);
        int[,] map = new int[32, 32];
        //現在配置されているピースの総数
        int sum = 0;
        //使われたピース番号
        int[] used = new int[256];

        //ピースの提出データ
        /*0:y 1:x 2:(0:H/1:T) 3:angle 4:何個目のピースか*/
        int[,] data = new int[256, 5];
        //ピース
        int[, ,] item = new int[256, 8, 8];


        //make_pair
        public class Obj
        {
            public int num;
            public int x;
            public int y;
            public int routen;
            public int sur;
            public Obj(int Num, int X, int Y, int Routen, int Sur)
            {
                num = Num;
                x = X;
                y = Y;
                routen = Routen;
                sur = Sur;
            }
        }
        //条件付きのリスト
        List<Obj> fdata = new List<Obj>();


        //初期化やファイル読み込み
        private void Form1_Load(object sender, EventArgs e)
        {
            //テスト用にパーツの作成

            for (int i = 0; i < 256; i++)
                data[i, 4] = -1;
        }

        private void tabPage1_Paint(object sender, PaintEventArgs e)
        {
            //*************枠の作成*****************
            //描画先とするImageオブジェクトを作成する
            Bitmap canvas = new Bitmap(pictureBox2.Width, pictureBox1.Height);
            //ImageオブジェクトのGraphicsオブジェクトを作成する
            Graphics g = Graphics.FromImage(canvas);
            for (int i = 0; i < MAP; i++)
            {
                for (int j = 0; j < MAP; j++)
                {
                    //block
                    if (map[i, j] == 1)
                        g.FillRectangle(Brushes.Black, (400 + j * width), 0 + i * length, 1 + width, 1 + length);
                    else if (map[i, j] >= 2)
                        g.FillRectangle(Brushes.Aqua, (400 + j * width) + 1, 0 + i * length + 1, width, length);
                }
            }
            for (int i = 0; i <= MAP; i++)
            {
                //(x, y)-(x, y)に、幅1の黒い線を引く
                g.DrawLine(Pens.Black, (400), 0 + i * length, (400 + 32 * width), 0 + i * length);
                g.DrawLine(Pens.Black, (400 + i * width), 0, (400 + i * width), 0 + 32 * length);
            }
            //リソースを解放する
            g.Dispose();
            //PictureBox1に表示する
            pictureBox1.Image = canvas;
            //**************************************

        }

        private void tabPage2_Paint(object sender, PaintEventArgs e)
        {

            //描画先とするImageオブジェクトを作成する
            Bitmap canvas = new Bitmap(pictureBox2.Width, pictureBox2.Height);
            //ImageオブジェクトのGraphicsオブジェクトを作成する
            Graphics g = Graphics.FromImage(canvas);

            //中央のマスを作成
            for (int i = 0; i <= MAP; i++)
            {
                //(x, y)-(x, y)に、幅1の黒い線を引く
                g.DrawLine(Pens.Black, (275), i * length, (275 + 32 * width), i * length);
                g.DrawLine(Pens.Black, (275 + i * width), 0, (275 + i * width), 32 * length);
            }
            for (int i = 0; i < MAP; i++)
            {
                for (int j = 0; j < MAP; j++)
                {
                    //黒色の表示
                    if (map[i, j] == 1)
                        g.FillRectangle(Brushes.Black, (275 + j * width), i * length, width, length);
                    else if (map[i, j] > 1)
                    {
                        if (map[i, j] == map[i, j + 1])
                            g.FillRectangle(Brushes.Aqua, (275 + j * width + 1), i * length + 1, 17, 16);
                        if (map[i, j] == map[i + 1, j])
                            g.FillRectangle(Brushes.Aqua, (275 + j * width + 1), i * length + 1, 16, 17);
                        if (map[i, j] != map[i, j + 1] && map[i, j] != map[i + 1, j])
                            g.FillRectangle(Brushes.Aqua, (275 + j * width + 1), i * length + 1, 16, 16);
                    }
                }
            }

            //右ブロックの表示
            for (int i = 0; i < 4; i++)
                for (int j = 0; j <= 8; j++)
                {
                    //(x, y)-(x, y)に、幅1の黒い線を引く
                    if (i == 0)
                    {
                        g.DrawLine(Pens.Black, (850), i * 160 + j * length, (850 + 8 * width), i * 160 + j * length);
                        g.DrawLine(Pens.Black, (850 + j * width), i * 160, (850 + j * width), i * 160 + 8 * length);
                    }
                    else
                    {
                        g.DrawLine(Pens.Black, (850), 125 + i * 125 + j * 13, (850 + 8 * 13), 125 + i * 125 + j * 13);
                        g.DrawLine(Pens.Black, (850 + j * 13), 125 + i * 125, (850 + j * 13), 125 + i * 125 + 8 * 13);
                    }
                }
            for (int i = 0; i < 6; i++)
                for (int j = 0; j <= 8; j++)
                {
                    //(x, y)-(x, y)に、幅1の黒い線を引く
                    g.DrawLine(Pens.Black, (1025), i * 100 + j * 10, (1025 + 8 * 10), i * 100 + j * 10);
                    g.DrawLine(Pens.Black, (1025 + j * 10), i * 100, (1025 + j * 10), i * 100 + 8 * 10);
                }
            for (int i = 0; i < 4; i++)
            {
                if (pieces > count + i)
                    for (int j = 0; j < 8; j++)
                        for (int k = 0; k < 8; k++)
                            if (i == 0)
                            {
                                if (item[count, j, k] == 1)
                                    g.FillRectangle(Brushes.Aqua, (850 + k * width + 1), i * 160 + j * length + 1, width - 1, length - 1);
                            }
                            else if (item[count + i, j, k] == 1)
                            {
                                g.FillRectangle(Brushes.Aqua, (850 + k * 13 + 1), 125 + i * 125 + j * 13 + 1, 12, 12);
                            }
            }
            for (int i = 0; i < 6; i++)
            {
                if (pieces > count + 4 + i)
                    for (int j = 0; j < 8; j++)
                        for (int k = 0; k < 8; k++)
                            if (item[count + 4 + i, j, k] == 1)
                                g.FillRectangle(Brushes.Aqua, (1025 + k * 10 + 1), i * 100 + j * 10 + 1, 9, 9);
            }

            //左ブロックの表示
            for (int i = 0; i < 10; i++)
                for (int j = 0; j <= 8; j++)
                {
                    //(x, y)-(x, y)に、幅1の黒い線を引く
                    g.DrawLine(Pens.Black, (15 + i / 5 * 125), i % 5 * 125 + j * 13, (15 + i / 5 * 125 + 8 * 13), i % 5 * 125 + j * 13);
                    g.DrawLine(Pens.Black, (15 + i / 5 * 125 + j * 13), i % 5 * 125, (15 + i / 5 * 125 + j * 13), i % 5 * 125 + 8 * 13);
                }
            int a = 0;
            for (int i = 0; i < 10; i++)
            {
                if ((count - 10 + i >= 0) && (pieces > count + i - 10))
                    for (int j = 0; j < 8; j++)
                        for (int k = 0; k < 8; k++)
                            if (item[count - 10 + i, j, k] == 1)
                                if (data[a, 4] == count - 10 + i)
                                    g.FillRectangle(Brushes.PaleGreen, (15 + i / 5 * 125 + k * 13 + 1), i % 5 * 125 + j * 13 + 1, 12, 12);
                                else
                                    g.FillRectangle(Brushes.Aqua, (15 + i / 5 * 125 + k * 13 + 1), i % 5 * 125 + j * 13 + 1, 12, 12);

                if (data[a, 4] == count - 10 + i) a++;
            }

            //フォーム上の座標でマウスポインタの位置を取得する
            //画面座標でマウスポインタの位置を取得する
            System.Drawing.Point sp = System.Windows.Forms.Cursor.Position;
            //画面座標をクライアント座標に変換する
            System.Drawing.Point cp = this.PointToClient(sp);

            int x;
            int y = (cp.Y - 60) / length;
            if (cp.X > (60 + mass_begin + 69))
                x = (cp.X - (60 + mass_begin + 69)) / width;
            else
                x = (cp.X - (60 + mass_begin + 68)) / width - 1;
            //ブロックを置くことができるか判定
            int block_check = 0;

            //ブロックの当たり判定
            if (click_down_flag == 1)
            {
                data[sum, 0] = y;
                data[sum, 1] = x;
                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        if (item[count, i, j] == 1)
                        {
                            if (cp.X >= mass_begin && cp.X <= mass_end)
                            {
                                if ((y + i) < 0 || (y + i) >= 32 || (x + j) < 0 || (x + j) >= 32)
                                {
                                    g.FillRectangle(Brushes.Pink, (cp.X - 60) / width * width + j * 17 + 4, y * length + i * 17 + 1, 16, 16);
                                    block_check++;
                                }
                                else if (map[y + i, x + j] >= 1)
                                {
                                    g.FillRectangle(Brushes.DeepPink, (cp.X - 60) / width * width + j * 17 + 4, y * length + i * 17 + 1, 16, 16);
                                    block_check++;
                                }
                                else
                                {
                                    g.FillRectangle(Brushes.Aqua, (cp.X - 60) / width * width + j * 17 + 4, y * length + i * 17 + 1, 16, 16);
                                }
                            }
                            else
                            {
                                g.FillRectangle(Brushes.Aqua, cp.X - 60 + j * 17, cp.Y - 60 + i * 17, 16, 16);
                            }
                        }
                    }
                }
            }

            if (click_up_flag == 1)
            {
                click_up_flag = 0;
                if (click_down_flag == 1 && block_check == 0 && mass_begin <= cp.X && mass_end >= cp.X)
                {
                    for (int i = 0; i < 8; i++)
                        for (int j = 0; j < 8; j++)
                            if (item[count, i, j] == 1 && (i + y) >= 0 && (j + x) >= 0 && (i + y) < 32 && (j + x) < 32)
                                map[i + y, j + x] = count + 2;
                    data[sum, 4] = count;
                    used[sum] = count;
                    sum++;
                    if (count < pieces)
                        count++;
                }
                click_down_flag = 0;
            }

            g.DrawString(data[sum, 0].ToString("D"), fnt, Brushes.Blue, 850, 210);
            g.DrawString(data[sum, 1].ToString("D"), fnt, Brushes.Blue, 870, 210);
            g.DrawString(data[sum, 3].ToString("D"), fnt, Brushes.Blue, 910, 210);
            if (data[sum, 2] == 0)
                g.DrawString("H", fnt, Brushes.Blue, 890, 210);
            else
                g.DrawString("T", fnt, Brushes.Blue, 890, 210);

            //リソースを解放する
            g.Dispose();
            //PictureBox1に表示する
            pictureBox2.Image = canvas;

        }

        private void tabPage3_Paint(object sender, PaintEventArgs e)
        {
            //描画先とするImageオブジェクトを作成する
            Bitmap canvas = new Bitmap(pictureBox3.Width, pictureBox3.Height);
            //ImageオブジェクトのGraphicsオブジェクトを作成する
            Graphics g = Graphics.FromImage(canvas);
            for (int k = 0; k < fdata.Count; k++)
            {
                for (int i = 0; i < 8; i++)
                    for (int j = 0; j < 8; j++)
                        if (item[(int)fdata[k].num, i, j] == 1)
                            g.FillRectangle(Brushes.Aqua, (int)fdata[k].x + j * 7, (int)fdata[k].y + i * 7, 7, 7);
            }
            //リソースを解放する
            g.Dispose();
            //PictureBox3に表示する
            pictureBox3.Image = canvas;
        }

        //クリックダウン
        private void pictureBox2_MouseDown(object sender, MouseEventArgs e)
        {
            //フォーム上の座標でマウスポインタの位置を取得する
            //画面座標でマウスポインタの位置を取得する
            System.Drawing.Point sp = System.Windows.Forms.Cursor.Position;
            //画面座標をクライアント座標に変換する
            System.Drawing.Point cp = this.PointToClient(sp);
            if (cp.X >= 850 && cp.X < 1010 && cp.Y >= 0 && cp.Y < 165)
            {
                click_down_flag = 1;
            }
        }

        //クリックアップ
        private void pictureBox2_MouseUp(object sender, MouseEventArgs e)
        {
            click_up_flag = 1;
        }

        //戻るボタン
        private void button1_Click(object sender, EventArgs e)
        {
            if (count > 0)
            {
                for (int i = 0; i < 32; i++)
                    for (int j = 0; j < 32; j++)
                        if (map[i, j] == count + 1)
                            map[i, j] = 0;
                count--;
                if (sum > 0 && count == data[sum - 1, 4])
                {
                    data[sum - 1, 4] = -1;
                    used[sum - 1] = -1;
                    sum--;
                }
            }
        }

        //スキップボタン
        private void button2_Click(object sender, EventArgs e)
        {
            if (count < pieces)
            {
                count++;
            }
        }

        //反時計回り
        private void button3_Click(object sender, EventArgs e)
        {
            int[,] aaa = new int[4, 4];
            //4分割して入れ替える
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    aaa[i, j] = item[count, i, j];
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    item[count, i, j] = item[count, j, 7 - i];
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    item[count, j, 7 - i] = item[count, 7 - i, 7 - j];
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    item[count, 7 - i, 7 - j] = item[count, 7 - j, i];
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    item[count, 7 - j, i] = aaa[i, j];

            data[sum, 3] += 270;
            data[sum, 3] %= 360;
        }

        //時計回り
        private void button4_Click(object sender, EventArgs e)
        {
            int[,] aaa = new int[4, 4];
            //4分割して入れ替える
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    aaa[i, j] = item[count, i, j];
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    item[count, i, j] = item[count, 7 - j, i];
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    item[count, 7 - j, i] = item[count, 7 - i, 7 - j];
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    item[count, 7 - i, 7 - j] = item[count, j, 7 - i];
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    item[count, j, 7 - i] = aaa[i, j];

            data[sum, 3] += 90;
            data[sum, 3] %= 360;
        }

        //反転
        private void button5_Click(object sender, EventArgs e)
        {
            int[,] aaa = new int[8, 4];
            //左半分と右半分を入れ替える
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 4; j++)
                    aaa[i, j] = item[count, i, j];
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 4; j++)
                    item[count, i, j] = item[count, i, 7 - j];
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 4; j++)
                    item[count, i, 7 - j] = aaa[i, j];

            data[sum, 2]++;
            data[sum, 2] %= 2;
        }


        //*************************************
        //入力ファイルを開く
        //
        //入力ファイル選択Buttonでファイル選択
        private void button11_Click(object sender, EventArgs e)
        {
            DialogResult dr = openFileDialog1.ShowDialog();
            if (dr == System.Windows.Forms.DialogResult.OK)
            {
                //ファイル名をtextBox1に出力
                textBox1.Text = openFileDialog1.FileName;
            }

            //テキスト内容をtextBox3に出力
            FileStream fs = new FileStream(openFileDialog1.FileName, FileMode.Open);
            StreamReader sr = new StreamReader(fs);

            string text = sr.ReadToEnd();
            textBox3.Text = text;
            System.Text.Encoding.GetEncoding("us-ascii");
            System.IO.StringReader rs = new System.IO.StringReader(text);

            int mapx = 0, mapy = 0; //マップのx,y座標
            int num = 0, stnx = 0, stny = 0; //石の番号,x,y座標
            //読み込みできる文字がなくなるまで繰り返す
            while (rs.Peek() > -1)
            {
                string t = rs.ReadLine();
                if (t.Length == 32)     //マップ情報の読み取り
                {
                    for (mapx = 0; mapx < 32; mapx++)
                        map[mapy, mapx] = int.Parse(t.Substring(mapx, 1));
                    mapy++;
                }
                else if (t.Length == 8)  //石の読み取り
                {
                    for (stnx = 0; stnx < 8; stnx++)
                        item[num, stny, stnx] = int.Parse(t.Substring(stnx, 1));
                    stny++;
                    if (stny == 8)
                    {
                        num++;           //次の石の読み取りに移る
                        stny = 0;
                    }
                }
                else if (3 >= t.Length && t.Length >= 1)  //石の総数の読み取り
                {
                    pieces = int.Parse(t.Substring(0, t.Length));
                }
            }
            sr.Close();
            fs.Close();
            rs.Close();


            count = 0;
            for (int i = 0; i < pieces; i++)
            {
                for (int j = 0; j < 4; j++)
                    data[i, j] = 0;
            }
        }

        //---------------------------Drag&Dropでファイル選択--------------------------
        private void textBox1_DragDrop(object sender, DragEventArgs e)
        {
            //ドロップされたファイルの一覧を取得
            string[] fileName = (string[])e.Data.GetData(DataFormats.FileDrop, false);

            if (fileName.Length <= 0)
            {
                return;
            }

            //ドロップ先がTextBoxであるかチェック
            TextBox txtTarget = sender as TextBox;
            if (txtTarget == null)
            {
                return;
            }
            //テキスト内容をtextBox3に出力
            FileStream fs = new FileStream(fileName[0], FileMode.Open);
            StreamReader sr = new StreamReader(fs);

            string text = sr.ReadToEnd();
            textBox3.Text = text;
            System.Text.Encoding.GetEncoding("us-ascii");
            System.IO.StringReader rs = new System.IO.StringReader(text);

            int mapx = 0, mapy = 0; //マップのx,y座標
            int num = 0, stnx = 0, stny = 0; //石の番号,x,y座標
            //読み込みできる文字がなくなるまで繰り返す
            while (rs.Peek() > -1)
            {
                string t = rs.ReadLine();
                if (t.Length == 32)     //マップ情報の読み取り
                {
                    for (mapx = 0; mapx < 32; mapx++)
                        map[mapy, mapx] = int.Parse(t.Substring(mapx, 1));
                    mapy++;
                }
                else if (t.Length == 8)  //石の読み取り
                {
                    for (stnx = 0; stnx < 8; stnx++)
                        item[num, stny, stnx] = int.Parse(t.Substring(stnx, 1));
                    stny++;
                    if (stny == 8)
                    {
                        num++;           //次の石の読み取りに移る
                        stny = 0;
                    }
                }
                else if (3 >= t.Length && t.Length >= 1)  //石の総数の読み取り
                {
                    pieces = int.Parse(t.Substring(0, t.Length));
                }
            }
            sr.Close();
            fs.Close();
            rs.Close();
            //TextBoxの内容をファイル名に変更
            txtTarget.Text = fileName[0];

            count = 0;
            for (int i = 0; i < pieces; i++)
            {
                for (int j = 0; j < 4; j++)
                    data[i, j] = 0;
            }
        }

        private void textBox1_DragEnter(object sender, DragEventArgs e)
        {
            //ファイルがドラッグされている場合、カーソルを変更する
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
        }


        //*************************************

        //*************************************
        //計算結果を出力したファイルを生成する
        //
        //Buttonでファイル書き込み
        private void button10_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                FileStream fs = new FileStream(saveFileDialog1.FileName, FileMode.Create);
                StreamWriter sw = new StreamWriter(fs);

                int datastorage = 0;

                while (0 < data[0, 4]) //冒頭のスキップの処理が後述のwhile文だと上手くいかなかったのでこの文を追加
                {
                    textBox4.Text += ("\r\n");
                    data[0, 4]--;
                    datastorage++;
                }
                data[0, 4] += datastorage;

                for (int ij = 0; ij < pieces; ij++)
                {
                    datastorage = 0;
                    if (data[ij, 4] != -1)  //ピースが当てはめられていたら
                    {
                        if (0 < ij)
                        {
                            while (data[ij - 1, 4] + 1 < data[ij, 4])  //石番号でスキップ回数を判断
                            {
                                textBox4.Text += ("\r\n");
                                data[ij, 4]--;
                                datastorage++;
                            }
                            data[ij, 4] += datastorage;
                        }
                        for (int j = 0; j < 4; j++)   //テスト用に石番号も表示にはj<5、本来はj<4
                        {
                            if (j != 2)
                            {
                                textBox4.Text += data[ij, j];
                                if (j != 3)//j!=3
                                {
                                    textBox4.Text += string.Format(" ");
                                }
                                else if (j == 3)//j==3
                                {
                                    textBox4.Text += ("\r\n");
                                }
                            }
                            else if (j == 2)
                            {
                                if (data[ij, 2] == 0)
                                {
                                    textBox4.Text += ("H");
                                    textBox4.Text += string.Format(" ");
                                }
                                else if (data[ij, 2] == 1)
                                {
                                    textBox4.Text += ("T");
                                    textBox4.Text += string.Format(" ");
                                }
                            }

                        }
                    }
                }

                sw.WriteLine(textBox4.Text);
                sw.Close();
                fs.Close();
            }
        }

        //*************************************


        //パターン1~4のボタン
        private void button9_Click(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {

        }

        private void tabPage3_Click(object sender, EventArgs e)
        {
        }
        
        //出力ファイルの読み込み
        private void button12_Click(object sender, EventArgs e)
        {
            DialogResult dr = openFileDialog1.ShowDialog();
            if (dr == System.Windows.Forms.DialogResult.OK)
            {
                //ファイル名をtextBox1に出力
                textBox1.Text = openFileDialog1.FileName;
            }

            //テキスト内容をtextBox3に出力
            FileStream fs = new FileStream(openFileDialog1.FileName, FileMode.Open);
            StreamReader sr = new StreamReader(fs);

            string text = sr.ReadToEnd();
            textBox3.Text = text;
            System.Text.Encoding.GetEncoding("us-ascii");
            System.IO.StringReader rs = new System.IO.StringReader(text);

            int mapx = 0, mapy = 0; //マップのx,y座標
            int num = 0, stnx = 0, stny = 0; //石の番号,x,y座標
            //読み込みできる文字がなくなるまで繰り返す
            int i = 0;
            while (rs.Peek() > -1)
            {
                /*0:y 1:x 2:(0:H/1:T) 3:angle 4:何個目のピースか*/
                string t = rs.ReadLine();
                if (t.Length == 0)     //改行
                {
                  data[i, 4] = -1;
                }
                else if (t.Length == 8)  //書き込み
                {

                }
                i++;
            }
            sr.Close();
            fs.Close();
            rs.Close();

            for (num = 0; num < pieces; num++)
            {
                if (data[num, 4] == num)
                {
                    //データ格納
                    //回転
                    if (data[num, 2] == 1)
                    {
                        int[,] aaa = new int[8, 4];
                        //左半分と右半分を入れ替える
                        for (i = 0; i < 8; i++)
                            for (int j = 0; j < 4; j++)
                                aaa[i, j] = item[num, i, j];
                        for (i = 0; i < 8; i++)
                            for (int j = 0; j < 4; j++)
                                item[num, i, j] = item[num, i, 7 - j];
                        for (i = 0; i < 8; i++)
                            for (int j = 0; j < 4; j++)
                                item[num, i, 7 - j] = aaa[i, j];
                    }
                }
                if (data[num, 3] != 0)
                {
                    int kaiten = data[num, 3] / 90;
                    for (int k = 0; k < kaiten; k++)
                    {
                        int[,] aaa = new int[4, 4];
                        //4分割して入れ替える
                        for (i = 0; i < 4; i++)
                            for (int j = 0; j < 4; j++)
                                aaa[i, j] = item[num, i, j];
                        for (i = 0; i < 4; i++)
                            for (int j = 0; j < 4; j++)
                                item[num, i, j] = item[num, 7 - j, i];
                        for (i = 0; i < 4; i++)
                            for (int j = 0; j < 4; j++)
                                item[num, 7 - j, i] = item[num, 7 - i, 7 - j];
                        for (i = 0; i < 4; i++)
                            for (int j = 0; j < 4; j++)
                                item[num, 7 - i, 7 - j] = item[num, j, 7 - i];
                        for (i = 0; i < 4; i++)
                            for (int j = 0; j < 4; j++)
                                item[num, j, 7 - i] = aaa[i, j];
                    }
                }
                for (i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        map[data[num, 0] + i, data[num, 1] + j] = item[num, i, j];
                    }
                }
            }
        }

        private void make_pair_button_Click(object sender, EventArgs e)
        {
            //make_pair button
            System.Text.Encoding.GetEncoding("us-ascii");
            string t = textBox5.Text;
            int a = 0;
            int min = 100;
            int max = 0;
            while (t.Length > a)
            {
                if (t[a] != ' ')
                {
                    if (t.Length > (a + 1))
                    {
                        if (t[a + 1] != ' ')
                        {
                            if (min == 100)
                                min = Convert.ToInt32(t[a] - '0') * 10 + Convert.ToInt32(t[a + 1] - '0');
                            else
                                max = Convert.ToInt32(t[a] - '0') * 10 + Convert.ToInt32(t[a + 1] - '0');
                            a++;
                        }
                        else
                        {
                            if (min == 100)
                                min = Convert.ToInt32(t[a] - '0');
                            else
                                max = Convert.ToInt32(t[a] - '0');
                        }
                    }
                    else
                    {
                        if (min == 100)
                            min = Convert.ToInt32(t[a] - '0');
                        else
                            max = Convert.ToInt32(t[a] - '0');
                    }
                }
                a++;
            }
            //条件で選別
            fdata.Clear();
            for (int k = 0; k < pieces; k++)
            {
                int sum = 0;
                for (int i = 0; i < 8; i++)
                    for (int j = 0; j < 8; j++)
                        if (item[k, i, j] == 1)
                            sum++;
                if (max == 0 && min <= sum)
                    fdata.Add(new Obj(k, 0, 0, 0, 0));
                else if (max >= sum && min <= sum)
                    fdata.Add(new Obj(k, 0, 0, 0, 0));
            }
            textBox5.Text = Convert.ToString(fdata.Count);
            for (int i = 0; i < fdata.Count; i++)
            {
                fdata[i].x = 56 * (int)(i / 10) + 10;
                fdata[i].y = 56 * (int)(i % 10) + 60;
            }
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            /*if (!(char.IsNumber(e.KeyChar)) && !(char.IsWhiteSpace(e.KeyChar)) && !(e.KeyCode == Keys.Back))
            {
              //押されたキーが 0～9でない場合は、イベントをキャンセルする
              e.Handled = true;
            }*/
        }

        int move_flag = 0;
        int move_piece = 0;
        private void pictureBox3_MouseDown(object sender, MouseEventArgs e)
        {
            //フォーム上の座標でマウスポインタの位置を取得する
            //画面座標でマウスポインタの位置を取得する
            System.Drawing.Point sp = System.Windows.Forms.Cursor.Position;
            //画面座標をクライアント座標に変換する
            System.Drawing.Point cp = this.PointToClient(sp);
            int flag = 0;
            for (int k = 0; k < fdata.Count; k++)
            {
                int a = cp.X - 12;
                int b = cp.Y - 27;
                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        int X = a - (fdata[k].x + j * 7);
                        int Y = b - (fdata[k].y + i * 7);
                        if (item[(int)fdata[k].num, i, j] == 1 && X <= 7 && X > 0 && Y <= 7 && Y > 0)
                        {
                            if ((Control.ModifierKeys & Keys.Shift) == Keys.Shift)
                            {
                                int[,] aaa = new int[8, 4];
                                //左半分と右半分を入れ替える
                                for (int y = 0; y < 8; y++)
                                    for (int x = 0; x < 4; x++)
                                        aaa[y, x] = item[fdata[k].num, y, x];
                                for (int y = 0; y < 8; y++)
                                    for (int x = 0; x < 4; x++)
                                        item[fdata[k].num, y, x] = item[fdata[k].num, y, 7 - x];
                                for (int y = 0; y < 8; y++)
                                    for (int x = 0; x < 4; x++)
                                        item[fdata[k].num, y, 7 - x] = aaa[y, x];

                                data[fdata[k].num, 2]++;
                                data[fdata[k].num, 2] %= 2;
                            }
                            else if ((Control.ModifierKeys & Keys.Control) == Keys.Control)
                            {
                                int[,] aaa = new int[4, 4];
                                //4分割して入れ替える
                                for (int y = 0; y < 4; y++)
                                    for (int x = 0; x < 4; x++)
                                        aaa[y, x] = item[fdata[k].num, y, x];
                                for (int y = 0; y < 4; y++)
                                    for (int x = 0; x < 4; x++)
                                        item[fdata[k].num, y, x] = item[fdata[k].num, 7 - x, y];
                                for (int y = 0; y < 4; y++)
                                    for (int x = 0; x < 4; x++)
                                        item[fdata[k].num, 7 - x, y] = item[fdata[k].num, 7 - y, 7 - x];
                                for (int y = 0; y < 4; y++)
                                    for (int x = 0; x < 4; x++)
                                        item[fdata[k].num, 7 - y, 7 - x] = item[fdata[k].num, x, 7 - y];
                                for (int y = 0; y < 4; y++)
                                    for (int x = 0; x < 4; x++)
                                        item[fdata[k].num, x, 7 - y] = aaa[y, x];

                                data[fdata[k].num, 3] += 90;
                                data[fdata[k].num, 3] %= 360;
                            }
                            else
                            {
                                move_flag = 1;
                                move_piece = k;
                            }
                            flag = 1;
                            break;
                        }
                        if (flag == 1)
                            break;
                    }
                    if (flag == 1)
                        break;
                }
                if (flag == 1)
                    break;
            }
        }


        private void pictureBox3_MouseUp(object sender, MouseEventArgs e)
        {
            move_flag = 0;
        }

        private void pictureBox3_MouseMove(object sender, MouseEventArgs e)
        {
            //フォーム上の座標でマウスポインタの位置を取得する
            //画面座標でマウスポインタの位置を取得する
            System.Drawing.Point sp = System.Windows.Forms.Cursor.Position;
            //画面座標をクライアント座標に変換する
            System.Drawing.Point cp = this.PointToClient(sp);
            if (move_flag == 1)
            {
                fdata[move_piece].x = cp.X - 12;
                fdata[move_piece].y = cp.Y - 27;
            }
        }

        //文字コードはASCIIコード、改行コードはCR+LF

    }
}