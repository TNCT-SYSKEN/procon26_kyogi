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

    const int MAP = 32, width = 17, length = 17;
    int[,] map = new int[32, 32];


    //初期化やファイル読み込み
    private void Form1_Load(object sender, EventArgs e)
    {
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


    }

    private void tabPage1_Paint(object sender, PaintEventArgs e)
    {

    }

    private void tabPage2_Paint(object sender, PaintEventArgs e)
    {
      //*************枠の作成*****************
      //描画先とするImageオブジェクトを作成する
      Bitmap canvas = new Bitmap(pictureBox1.Width, pictureBox1.Height);
      //ImageオブジェクトのGraphicsオブジェクトを作成する
      Graphics g = Graphics.FromImage(canvas);
      for (int i = 0; i <= MAP; i++)
      {
        //(x, y)-(x, y)に、幅1の黒い線を引く
        g.DrawLine(Pens.Black, (300), i * length, (300 + 32 * width), i * length);
        g.DrawLine(Pens.Black, (300 + i * width), 0, (300 + i * width), 32 * length);
      }
      for (int i = 0; i < MAP; i++)
      {
        for (int j = 0; j < MAP; j++)
        {
          //黒色の表示
          if(map[i, j] == 1)
            g.FillRectangle(Brushes.Black, (300 + j * width), i * length, width, length);
        }
      }
      //リソースを解放する
      g.Dispose();
      //PictureBox1に表示する
      pictureBox1.Image = canvas;
      //**************************************


    }


    
  }
}
