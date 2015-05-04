using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace procon26_kyogi
{
  public partial class Form1 : Form
  {
    public Form1()
    {
      InitializeComponent();
    }
    //定数
    const int MAP = 32, width = 17, length = 17;

    int pieces = 10;
    int count = 0;
    //Fontを作成
    Font fnt = new Font("ＭＳ ゴシック", 12);
    int[,] map = new int[32, 32];
    int[, ,] item = new int[10, 8, 8] { { { 0,0,0,0,0,0,0,0},
                                        { 0,1,1,1,1,1,1,0},
                                        { 0,1,0,0,0,0,0,0},
                                        { 0,1,0,0,0,0,0,0}, 
                                        { 0,1,0,0,0,0,0,0},
                                        { 0,0,0,0,0,0,0,0}, 
                                        { 0,0,0,0,0,0,0,0}, 
                                        { 0,0,0,0,0,0,0,0} },
    
                                        { { 0,1,0,0,0,0,0,0},
                                        { 0,1,0,0,0,0,0,0},
                                        { 0,1,0,0,0,0,0,0},
                                        { 0,1,0,0,0,0,0,0}, 
                                        { 0,1,0,0,0,0,0,0},
                                        { 0,1,0,0,0,0,0,0}, 
                                        { 0,1,0,0,0,0,0,0}, 
                                        { 0,1,0,0,0,0,0,0} },
    
                                        { { 0,0,0,0,0,0,0,0},
                                        { 0,0,0,0,0,0,0,0},
                                        { 0,0,0,0,1,0,0,0},
                                        { 0,0,0,0,1,0,0,0}, 
                                        { 0,0,0,0,1,1,1,0},
                                        { 0,0,0,0,0,0,1,0}, 
                                        { 0,0,0,0,0,0,1,0}, 
                                        { 0,0,0,0,0,0,0,0} },
    
                                        { { 0,0,0,0,0,0,0,0},
                                        { 0,0,0,0,0,0,0,0},
                                        { 0,0,0,0,0,0,0,0},
                                        { 0,0,0,0,0,0,0,0}, 
                                        { 0,0,0,1,1,1,1,0},
                                        { 0,0,0,1,1,1,1,0}, 
                                        { 0,0,0,1,1,1,1,0}, 
                                        { 0,0,0,0,0,0,0,0} },

                                        { { 0,0,0,0,0,0,0,1},
                                        { 0,0,0,0,0,0,1,1},
                                        { 0,0,0,0,0,1,1,0},
                                        { 0,0,0,0,1,1,0,0}, 
                                        { 0,0,0,1,1,0,0,0},
                                        { 0,0,1,1,0,0,0,0}, 
                                        { 0,1,1,0,0,0,0,0}, 
                                        { 1,1,0,0,0,0,0,0} },
    
                                        { { 0,0,0,0,0,0,0,0},
                                        { 1,1,0,0,0,0,0,0},
                                        { 1,0,0,0,0,0,0,0},
                                        { 1,0,0,0,0,0,0,0}, 
                                        { 1,1,1,1,1,1,1,1},
                                        { 0,0,0,0,0,0,0,1}, 
                                        { 0,0,0,0,0,0,0,1}, 
                                        { 0,0,0,0,0,0,0,1} },
    
                                        { { 0,0,0,0,0,0,0,0},
                                        { 0,0,0,0,0,0,0,0},
                                        { 0,0,0,1,0,1,0,0},
                                        { 0,0,1,1,1,1,1,0}, 
                                        { 0,0,1,0,0,0,0,0},
                                        { 0,0,1,1,1,0,0,0}, 
                                        { 0,0,0,0,0,0,0,0}, 
                                        { 0,0,0,0,0,0,0,0} },
    
                                        { { 0,0,0,0,0,0,0,0},
                                        { 0,0,1,0,0,0,0,0},
                                        { 0,0,1,0,0,0,0,0},
                                        { 0,0,1,0,0,0,0,0}, 
                                        { 0,0,1,1,1,1,0,0},
                                        { 0,0,1,0,0,0,0,0}, 
                                        { 0,0,1,0,0,0,0,0}, 
                                        { 0,0,0,0,0,0,0,0} },
    
                                        { { 0,0,0,0,1,0,0,0},
                                        { 0,1,1,1,1,1,0,0},
                                        { 0,0,0,0,0,1,0,0},
                                        { 0,0,1,0,0,1,0,0}, 
                                        { 0,0,1,0,0,1,0,0},
                                        { 0,0,1,1,1,1,0,0}, 
                                        { 0,0,0,0,0,0,0,0}, 
                                        { 0,0,0,0,0,0,0,0} },
    
                                        { { 0,0,0,0,0,0,0,0},
                                        { 0,0,0,0,0,0,0,0},
                                        { 0,0,0,0,0,0,0,0},
                                        { 0,0,0,0,0,0,0,0}, 
                                        { 0,0,0,0,0,0,0,0},
                                        { 0,0,0,0,0,0,0,0}, 
                                        { 0,0,1,1,1,1,0,0}, 
                                        { 0,0,1,1,1,1,1,0} },};


    //初期化やファイル読み込み
    private void Form1_Load(object sender, EventArgs e)
    {
      //テスト用のフィールド
      for (int i = 0; i < MAP; i++)
        for (int j = 0; j < MAP; j++)
          map[i, j] = 0;
      for (int i = 1; i < 6; i++)
      for (int j = 0; j < MAP; j++)
      {
        map[MAP - i, j] = 1;
        map[j, MAP - i] = 1;
      }
      map[10, 10] = 1;

      //テスト用にパーツの作成


    }

    private void tabPage1_Paint(object sender, PaintEventArgs e)
    {
      //*************枠の作成*****************
      //描画先とするImageオブジェクトを作成する
      Bitmap canvas = new Bitmap(pictureBox2.Width, pictureBox1.Height);
      //ImageオブジェクトのGraphicsオブジェクトを作成する
      Graphics g = Graphics.FromImage(canvas);
      for (int i = 0; i <= MAP; i++)
      {
        //(x, y)-(x, y)に、幅1の黒い線を引く
        g.DrawLine(Pens.Black, (800), 250 + i * length / 2, (800 + 32 * width / 2), 250 + i * length / 2);
        g.DrawLine(Pens.Black, (800 + i * width / 2), 250, (800 + i * width / 2), 250 + 32 * length / 2);
      }
      for (int i = 0; i < MAP; i++)
      {
        for (int j = 0; j < MAP; j++)
        {
          //黒色の表示
          if (map[i, j] == 1)
            g.FillRectangle(Brushes.Black, (800 + j * width / 2), 250 + i * length / 2, 1 + width / 2, 1 + length / 2);
        }
      }
      //リソースを解放する
      g.Dispose();
      //PictureBox1に表示する
      pictureBox1.Image = canvas;
      //**************************************
    
    }

    private void tabPage2_Paint(object sender, PaintEventArgs e)
    {
      //*************枠の作成*****************
      //描画先とするImageオブジェクトを作成する
      Bitmap canvas = new Bitmap(pictureBox2.Width, pictureBox2.Height);
      //ImageオブジェクトのGraphicsオブジェクトを作成する
      Graphics g = Graphics.FromImage(canvas);
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
          if(map[i, j] == 1)
            g.FillRectangle(Brushes.Black, (275 + j * width), i * length, width, length);
        }
      }

      //右ブロックの表示
      for (int i = 0; i < 4; i++)
      for (int j = 0; j <= 8; j++)
      {
        //(x, y)-(x, y)に、幅1の黒い線を引く
        g.DrawLine(Pens.Black, (850), i * 160 + j * length, (850 + 8 * width), i * 160 + j * length);
        g.DrawLine(Pens.Black, (850 + j * width), i * 160, (850 + j * width), i * 160 + 8 * length);
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
          if (item[count + i, j, k] == 1)
            g.FillRectangle(Brushes.Aqua, (850 + k * width + 1), i * 160 + j * length + 1, width-1, length-1);
      }
      for (int i = 0; i < 6; i++)
      {
        if(pieces > count + 4 + i)
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
      for (int i = 0; i < 10; i++)
      {
        if ((count - 10 + i >= 0) && (pieces > count + i - 10))
        for (int j = 0; j < 8; j++)
          for (int k = 0; k < 8; k++)
            if (item[count - 10 + i, j, k] == 1)
              g.FillRectangle(Brushes.Aqua, (15 + i / 5 * 125 +  k * 13 + 1), i % 5 * 125 + j * 13 + 1, 12, 12);
      }

        //リソースを解放する
        g.Dispose();
      //PictureBox1に表示する
      pictureBox2.Image = canvas;
      //**************************************


      /*ネタ*/
      /*
      t++;
      t %= 1000;
      if(t < 500){
      //フォーム上の座標でマウスポインタの位置を取得する
      //画面座標でマウスポインタの位置を取得する
      System.Drawing.Point sp = System.Windows.Forms.Cursor.Position;
      //画面座標をクライアント座標に変換する

      System.Drawing.Point cp = this.PointToClient(sp);
      //X座標を取得する
      int x = cp.X;
      //Y座標を取得する
      int y = cp.Y;
      float angle = (float)(0.02 * Math.PI * (t % 1000));
      int a = (int)(250 + 200 * Math.Sin(angle));

      //フォーム上の座標 (10, 20) にマウスポインタを移動する
      //クライアント座標を画面座標に変換する
      System.Drawing.Point mp = this.PointToScreen(
          new System.Drawing.Point((t % 200) * 5, a));
      //マウスポインタの位置を設定する
      System.Windows.Forms.Cursor.Position = mp;
      }*/

    }

    //戻るボタン
    private void button1_Click(object sender, EventArgs e)
    {
      if(count > 0)
      count--;
    }

    //スキップボタン
    private void button2_Click(object sender, EventArgs e)
    {
      if(count < (pieces + 10))
      count++;
    }

    
    
  }
}
