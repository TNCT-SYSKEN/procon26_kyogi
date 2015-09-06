using System;
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

    //現在選択中のピース
    int[,] item_test = new int[8, 8];
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
    //ピースの提出データ
    /*0:y 1:x 2:(0:H/1:T) 3:angle 4:何個目のピースか*/
    int[,] data = new int[256, 5];
    //ピース
    int[, ,] item = new int[256, 8, 8];

    //条件付きのリスト
    ArrayList v = new ArrayList();


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
          else if(map[i, j] >= 2)
            g.FillRectangle(Brushes.Aqua, (400 + j * width) + 1, 0 + i * length + 1,  width, length);
        }
      }
      for (int i = 0; i <= MAP; i++)
      {
        //(x, y)-(x, y)に、幅1の黒い線を引く
        g.DrawLine(Pens.Black, (400), 0 + i * length , (400 + 32 * width ), 0 + i * length );
        g.DrawLine(Pens.Black, (400 + i * width ), 0, (400 + i * width ), 0 + 32 * length );
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
                if (item_test[j, k] == 1)
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
            if (item_test[i, j] == 1)
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
          {
            for (int j = 0; j < 8; j++)
            {
              if (item_test[i, j] == 1 && (i + y) >= 0 && (j + x) >= 0 && (i + y) < 32 && (j + x) < 32)
              {
                map[i + y, j + x] = count + 2;
              }
            }
          }
          data[sum, 4] = count;
          sum++;
          if (count < pieces)
          {
            count++;
            for (int i = 0; i < 8; i++)
              for (int j = 0; j < 8; j++)
                if (count < pieces)
                  item_test[i, j] = item[count, i, j];
                else
                  item_test[i, j] = 0;
          }
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
        for (int i = 0; i < 8; i++)
          for (int j = 0; j < 8; j++)
            item_test[i, j] = item[count, i, j];
        if (sum > 0 && count == data[sum - 1, 4])
        {
          data[sum - 1, 4] = -1;
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
        for (int i = 0; i < 8; i++)
          for (int j = 0; j < 8; j++)
            if (count < pieces)
              item_test[i, j] = item[count, i, j];
            else
              item_test[i, j] = 0;
      }
    }

    //反時計回り
    private void button3_Click(object sender, EventArgs e)
    {
      int[,] aaa = new int[4, 4];
      //4分割して入れ替える
      for (int i = 0; i < 4; i++)
        for (int j = 0; j < 4; j++)
          aaa[i, j] = item_test[i, j];
      for (int i = 0; i < 4; i++)
        for (int j = 0; j < 4; j++)
          item_test[i, j] = item_test[j, 7 - i];
      for (int i = 0; i < 4; i++)
        for (int j = 0; j < 4; j++)
          item_test[j, 7 - i] = item_test[7 - i, 7 - j];
      for (int i = 0; i < 4; i++)
        for (int j = 0; j < 4; j++)
          item_test[7 - i, 7 - j] = item_test[7 - j, i];
      for (int i = 0; i < 4; i++)
        for (int j = 0; j < 4; j++)
          item_test[7 - j, i] = aaa[i, j];

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
          aaa[i, j] = item_test[i, j];
      for (int i = 0; i < 4; i++)
        for (int j = 0; j < 4; j++)
          item_test[i, j] = item_test[7 - j, i];
      for (int i = 0; i < 4; i++)
        for (int j = 0; j < 4; j++)
          item_test[7 - j, i] = item_test[7 - i, 7 - j];
      for (int i = 0; i < 4; i++)
        for (int j = 0; j < 4; j++)
          item_test[7 - i, 7 - j] = item_test[j, 7 - i];
      for (int i = 0; i < 4; i++)
        for (int j = 0; j < 4; j++)
          item_test[j, 7 - i] = aaa[i, j];

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
          aaa[i, j] = item_test[i, j];
      for (int i = 0; i < 8; i++)
        for (int j = 0; j < 4; j++)
          item_test[i, j] = item_test[i, 7 - j];
      for (int i = 0; i < 8; i++)
        for (int j = 0; j < 4; j++)
          item_test[i, 7 - j] = aaa[i, j];

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
      System.Text.Encoding.GetEncoding("us-ascii");
      textBox3.Text = text;

      //TextBox3に入力されている文字列から一行ずつ読み込む
      //文字列(TextBox3に入力された文字列)からStringReaderインスタンスを作成
      System.IO.StringReader rs = new System.IO.StringReader(textBox3.Text);

      int mapx = 0, mapy = 0; //マップのx,y座標
      int num = 0, stnx = 0, stny = 0; //石の番号,x,y座標
      int next = 0; //stnnumを次の数値に移行させるための変数
      //読み込みできる文字がなくなるまで繰り返す
      while (rs.Peek() > -1)
      {
        Console.WriteLine(rs.ReadLine());
        if (rs.Peek() == 32)     //マップ情報の読み取り
        {
          for (mapx = 0; mapx < 32; mapx++)
          {
            map[mapx, mapy] = int.Parse(text.Substring(mapx, 1));
          }
          mapy++;
        }
        else if (rs.Peek() == 8)  //石の読み取り
        {
          for (stnx = 0; stnx < 8; stnx++)
          {
            item[num, stnx, stny] = int.Parse(text.Substring(stnx, 1));
          }
          next++;
          if (next == 7)
          {
            num++;           //次の石の読み取りに移る
            next = 0;
          }
        }
        else if (3 >= rs.Peek() && rs.Peek() >= 1)  //石の総数の読み取り
        {
          pieces = int.Parse(text.Substring(1, rs.Peek()));
        }
        //else if (iLength != 0)    //読み取り可能な文字がなくなったら
        //{
        //stns = stnnum;
        //}
        //
        //string text = sr.ReadLine();
        //一文字分の整数を取る
        //int a = int.Parse(text.Substring(10, 1));
        sr.Close();
        fs.Close();

      }
      rs.Close();
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
      for (int i = 0; i < 8; i++)
        for (int j = 0; j < 8; j++)
          item_test[i, j] = item[count, i, j];

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
      //条件で選別
      /*for (int k = 0; k < pieces; k++)
      {
        int sum = 0;
        for (int i = 0; i < 8; i++)
          for (int j = 0; j < 8; j++)
            if (item[k, i, j] == 1)
              sum++;
        if(sum  >= 5)
      }*/
    }


    /*
     int mapx=0,mapy=0,stnx=0,stny=0,s=0,num=0,pieces=0;    //pieces:石の数を記憶,num:各石に番号をふる
     
     while(一行とる){
        if(32){                                             //マップ情報の読み取り
          for(32回繰り返す){
            map[mapx][mapy]=一文字分のデータ
          }
          mapx++;                                           //次の行の読み取りに移る
          mapy=0;
        }else if(8){                                        //石の読み取り
          for(8回繰り返す){
            map[num][stnx][stny]=一文字分のデータ
          }
          s++;
          if(s==7){
            num++;                                          //次の石の読み取りに移る
            s=0;
          }
        }else if(!0){
          pieces=石の総数
        }
     }
     */

    /*
       1.三種類の入力パターン(マス、ピース、ピースの数)
       2.一列のサイズを把握(32,8,1~3)
       3.while文で繰り返す(配列)
       4.一つずつのデータをint型に変換して配列に代入
    */

    //文字コードはASCIIコード、改行コードはCR+LF
  }
}