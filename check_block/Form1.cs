using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace BlockChecker
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public struct information
        {
            public int x;
            public int y;
            public int n;
        };

        const int interval = 25;
        const int pictureboxheight = 509;
        const int pictureboxwidth = 548;
        int mass = 29;
        int masscount = 4;
        int before_target1 = -1;
        int labelsum;
        int[,] leftup;
        int[,] rightdown;
        public int sum;
        public int mass_sum;
        public int space_sum;
        public int[] total;
        public int[, ,] parts;
        public information[,] mass_info;
        public information[] space_info;
        bool button2flag = false;
        bool button3flag = false;
        bool button4flag = false;
        bool button5flag = false;
        bool checkflag = false;
        bool fileopen = false;
        bool charflag = false;
        bool markflag = false;
        bool[, ,] spacemass;
        bool[,] vir_map = new bool[24, 24];
        public bool list1_add_flag = false;
        public bool form2_flag = false;
        public bool firstflag = true;
        string editFilePath = "";
        List<int> color = new List<int>();
        public List<int> list_n = new List<int>();
        public Brush[, ,] blockcolor;
        TextBox buffer;
        Panel panel2 = new Panel();
        PictureBox pictureBox1 = new PictureBox();
        Form2 f2;

        private void Form1_Load(object sender, EventArgs e)
        {
            panel2.Location = new Point(265, 6);
            panel2.Size = new Size(this.Size.Width-291, this.Size.Height-52);
            pictureBox1.BackColor = System.Drawing.Color.White;
            pictureBox1.Location = new Point(0, 0);
            pictureBox1.Size = new Size(this.Size.Width - 291, this.Size.Height);
            button1.BackColor = Color.Gray;
            int h = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height;
            int w = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width;
            this.Location = new Point(0 , 0);
            this.Height = h;
            this.Width = w;
            panel2.Width = this.Size.Width - 291;
            panel2.Height = this.Size.Height - 52;
            pictureBox1.Width = this.Size.Width - 291;
            pictureBox1.Height = this.Size.Height - 52;
            panel1.Controls.Add(list1);
            panel1.Controls.Add(list2);
            panel2.Controls.Add(pictureBox1);
            this.Controls.Add(panel2);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            panel2.AutoScroll = true;
            pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
        }
               
        private void button1_Click(object sender, EventArgs e)
        {
            if (e.ToString() == "System.Windows.Form.KeyEventArgs")
            {
                KeyEventArgs k = (KeyEventArgs)e;
                if (!(k.Alt && (k.KeyCode == Keys.O)))
                {
                    return;
                }
            }
            openFileDlg.ShowDialog(this);
        }

        private void button1_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void openFileDlg_FileOk(object sender, CancelEventArgs e)
        { 
            buffer = new TextBox();
            editFilePath = openFileDlg.FileName;
            const string MSGBOX_TITLE = "ファイルオープン";
            fileopen = true;
            this.Text += ":"+openFileDlg.SafeFileName;
            int space_count;
            
            try
            {
                buffer.Text = File.ReadAllText(editFilePath, Encoding.Default);
                algorithm();
                color.Clear();
                color.Add(0);
                button2.BackColor = SystemColors.Control;
                button2flag = false;
                button3.BackColor = SystemColors.Control;
                button3flag = false;
                button4.BackColor = SystemColors.Control;
                button4flag = false;
                button5.BackColor = SystemColors.Control;
                button5flag = false;
                charflag = false;
                button6.BackColor = SystemColors.Control;
                button7.BackColor = SystemColors.Control;
                markflag = false;
                list1.ClearSelected();
                list1.Items.Clear();
                list2.Items.Clear();
                list1.Visible = false;
                list2.Visible = false;
                label1.Visible = false;
                button10.Visible = false;
                firstflag = true;

                for (int n = 0; n < sum; n++)
                {
                    for (int i = 1; i < 9; i++)
                    {
                        for (int j = 1; j < 9; j++)
                        {
                            if (parts[n, i, j] == 0)
                            {
                                space_count = parts[n, i - 1, j] + parts[n, i, j - 1] + parts[n, i + 1, j] + parts[n, i, j + 1];
                                if (space_count != 0)
                                {
                                    space_sum++;
                                }
                            }
                        }
                    }
                }
                space_info = new information[space_sum];

                Draw();

            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, MSGBOX_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void algorithm()
        {
            int N = 0, t = 0, i, length;
            mass_sum = 0;
            for (int count = 0; count < buffer.Lines.Length; count++)
            {
                length = buffer.Lines[count].Length;
                if (length == 8)
                {
                    for (i = 0; i < 8; i++)
                    {
                        parts[N, t+1, i+1] = (int)(buffer.Lines[count][i] - '0');
                        if (parts[N, t + 1, i + 1] == 1)
                        {
                            blockcolor[N, t, i] = Brushes.Aqua;
                            mass_info[N, total[N]].x = t;
                            mass_info[N, total[N]].y = i;
                            total[N]++;
                            if (t + 1 < leftup[N, 0])
                                leftup[N, 0] = t  +1 ;
                            if (t + 1 > rightdown[N, 0])
                                rightdown[N, 0] = t  +1 ;
                            if (i + 1 < leftup[N, 1])
                                leftup[N, 1] = i + 1 ;
                            if (i + 1 > rightdown[N, 1])
                                rightdown[N, 1] = i + 1;
                        }
                        else
                            blockcolor[N, t, i] = Brushes.White;
                    }
                    if (t == 7)
                    {
                        mass_sum+=total[N];
                        N++;
                        leftup[N, 0] = 9;
                        leftup[N, 1] = 9;
                        
                    }
                    t++;
                }
                else if (length != 0 && length != 32)
                {
                    sum = int.Parse(buffer.Lines[count]);
                    parts = new int[sum, 10, 10];
                    blockcolor = new Brush[sum, 8, 8];
                    leftup = new int[sum+1, 2] ;
                    rightdown = new int[sum + 1, 2];
                    spacemass = new bool[sum, 10, 10];
                    total = new int[sum];
                    mass_info = new information[sum,16];
                    leftup[N, 0] = 9;
                    leftup[N, 1] = 9;
                }
                else
                    t = 0;
            }
        }
        
        public void Draw()
        {
            int stonesum = mass * 8;
            int charnumber = 1;
            int marknumber = 1;
            labelsum = 0;
            if (pictureBox1.Height < interval * (sum / masscount + 1) + (sum / masscount + 1) * stonesum)
            {
                pictureBox1.Height = interval * (sum / masscount + 1) + (sum / masscount + 1) * stonesum + interval;
                panel1.Height = pictureBox1.Height;
            }
            else if (interval * (sum / masscount + 1) + (sum / masscount + 1) * stonesum < pictureBox1.Height)
            {
                pictureBox1.Height = this.Size.Height - 52;
                panel1.Height = pictureBox1.Height;
            }
            panel2.AutoScrollMinSize = new Size(800, this.Size.Height);
            Bitmap canvas = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            Graphics g = Graphics.FromImage(canvas); 
            Font fnt = new Font("MS UI Gothic", 11);
            Font spacefnt = new Font("メイリオ", 11);
            for (int i = 0; i < sum; i++)
            {
                for (int j = 0; j <= 8; j++)
                    {
                        g.DrawLine(Pens.Black, interval * (i % masscount + 1) + i % masscount * stonesum, interval * (i / masscount + 1) + (i / masscount * stonesum) + j * mass, interval * (i % masscount + 1) + (i % masscount + 1) * stonesum, interval * (i / masscount + 1) + (i / masscount * stonesum) + j * mass);
                        g.DrawLine(Pens.Black, interval * (i % masscount + 1) + (i % masscount * stonesum) + j * mass, interval * (i / masscount + 1) + i / masscount * stonesum, interval * (i % masscount + 1) + (i % masscount * stonesum) + j * mass, interval * (i / masscount + 1) + (i / masscount + 1) * stonesum);
                    }
            }

            for (int n = 0; n < sum; n++)
            {
                for (int i = 0; i < 8; i++)
                    for (int j = 0; j < 8; j++)
                    {
                        g.FillRectangle(blockcolor[n, i, j], interval * (n % masscount + 1) + n % masscount * stonesum + i * mass + 1, interval * (n / masscount + 1) + n / masscount * stonesum + j * mass + 1, mass - 1, mass - 1);
                        if (charflag||checkflag)
                        {
                            if (parts[n, i + 1, j + 1] == 1)
                            {
                                if (charflag)
                                {
                                    g.DrawString(charnumber.ToString(), fnt, Brushes.Black, interval * (n % masscount + 1) + n % masscount * stonesum + i * mass + 1, interval * (n / masscount + 1) + n / masscount * stonesum + j * mass + 1);
                                }
                                if (checkflag && blockcolor[n, i, j] == Brushes.OrangeRed)
                                {
                                    list2.Items.Add(charnumber);
                                    labelsum++;
                                }
                                charnumber++;
                            }
                        }
                        if (markflag)
                        {
                            if ((blockcolor[n, i, j] == Brushes.LightSalmon || blockcolor[n, i, j] == Brushes.LimeGreen || blockcolor[n, i, j] == Brushes.Red || blockcolor[n, i, j] == Brushes.Gray) && parts[n, i + 1, j + 1] == 0)
                            {
                                g.DrawString(marknumber.ToString(), spacefnt, Brushes.Red, interval * (n % masscount + 1) + n % masscount * stonesum + i * mass + 1, interval * (n / masscount + 1) + n / masscount * stonesum + j * mass + 1);
                                space_info[marknumber - 1].x = i+1;
                                space_info[marknumber - 1].y = j+1;
                                space_info[marknumber - 1].n = n;
                                if (list1_add_flag && blockcolor[n, i, j] != Brushes.Gray && list_n.Contains(n))                               
                                    list1.Items.Add(marknumber.ToString());
                                marknumber++;
                            }

                        }
                    }
            }
            if(!form2_flag&&firstflag&&list1.Text!="")
            {
                Pen BlackPen = new Pen(Color.Black, 3);
                int i = int.Parse(list1.Text)-1;
                for (int j = 0; j <= 8; j++)
                {
                    g.DrawLine(BlackPen, (interval * (i % masscount + 1) + i % masscount * stonesum)-1, (interval * (i / masscount + 1) + (i / masscount * stonesum) + j * mass)-1, (interval * (i % masscount + 1) + (i % masscount + 1) * stonesum)+1, (interval * (i / masscount + 1) + (i / masscount * stonesum) + j * mass)-1);
                    g.DrawLine(BlackPen, (interval * (i % masscount + 1) + (i % masscount * stonesum) + j * mass)-1, (interval * (i / masscount + 1) + i / masscount * stonesum)-1, (interval * (i % masscount + 1) + (i % masscount * stonesum) + j * mass)-1, (interval * (i / masscount + 1) + (i / masscount + 1) * stonesum)+1);
                }
            }
            fnt.Dispose();
            g.Dispose();
            pictureBox1.Image = canvas;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            int stonesum = mass * 8;
            int count = 0;
            int flagcount = 0;
            for (; pictureBox1.Width > interval * (count + 2) + (count + 1) * stonesum; count++) { }
            Bitmap canvas = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            Graphics g = Graphics.FromImage(canvas);
            if (button2flag)
            {
                color.Remove(2);
                paint();
                Draw();
                button2.BackColor = SystemColors.Control;
                button2flag = false;
            }
            else
            {
                for (int n = 0; n < sum; n++)
                {
                    for (int i = 1; i < 9; i++)
                    {
                        for (int j = 1; j < 9; j++)
                        {
                            flagcount = 0;
                            if (parts[n, i, j] == 1)
                            {
                                flagcount = parts[n, i - 1, j] + parts[n, i, j - 1] + parts[n, i + 1, j] + parts[n, i, j + 1];
                                if (flagcount == 1||flagcount == 0)
                                    blockcolor[n, i - 1, j - 1] = Brushes.DeepPink;                                
                            }
                        }
                    }
                }
                color.Add(2);
                Draw();
                button2.BackColor = Color.DeepPink;
                button2flag = true;
            }
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            panel2.Width=this.Size.Width - 291;
            panel2.Height = this.Size.Height - 52;
            pictureBox1.Width = this.Size.Width - 291;
            pictureBox1.Height = this.Size.Height - 52;
            if (fileopen)
                Draw();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int stonesum = mass * 8;
            int count = 0;
            int flagcount = 0;
            for (; pictureBox1.Width > interval * (count + 2) + (count + 1) * stonesum; count++) { }
            Bitmap canvas = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            Graphics g = Graphics.FromImage(canvas);
            if (button3flag)
            {                
                for (int n = 0; n < sum; n++)
                    for (int i =1; i < 9; i++)
                        for (int j = 1; j <9; j++)
                        {
                            flagcount = 0;
                            if (parts[n, i, j] == 0)
                            {
                                flagcount = parts[n, i - 1, j] + parts[n, i, j - 1] + parts[n, i + 1, j] + parts[n, i, j + 1];
                                if (flagcount == 3)
                                    if (markflag)
                                    {
                                        blockcolor[n, i-1, j-1] = Brushes.LightSalmon;
                                    }
                                    else
                                    {
                                        blockcolor[n, i-1, j-1] = Brushes.White;
                                    }
                            }
                        }
                Draw();
                button3.BackColor = SystemColors.Control;
                button3flag = false;
            }
            else
            {
                for (int n = 0; n < sum; n++)
                {
                    for (int i = 1; i < 9; i++)
                    {
                        for (int j = 1; j < 9; j++)
                        {
                            flagcount = 0;
                            if (parts[n, i, j] == 0)
                            {
                                flagcount = parts[n, i - 1, j] + parts[n, i, j - 1] + parts[n, i + 1, j] + parts[n, i, j + 1];
                                if (flagcount == 3)
                                {
                                    blockcolor[n, i - 1, j - 1] = Brushes.LimeGreen;
                                }
                            }
                        }
                    }
                }
                Draw();
                button3.BackColor = Color.LimeGreen;
                button3flag = true;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int stonesum = mass * 8;
            int count = 0;
            for (; pictureBox1.Width > interval * (count + 2) + (count + 1) * stonesum; count++) { }
            Bitmap canvas = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            Graphics g = Graphics.FromImage(canvas);
            if (button4flag)
            {
                color.Remove(4);
                paint();
                Draw();
                button4.BackColor = SystemColors.Control;
                button4flag = false;
            }
            else
            {
                for (int n = 0; n < sum; n++)
                {
                    for (int i = 1; i < 9; i++)
                    {
                        for (int j = 1; j < 9; j++)
                        {
                            if (parts[n, i, j] == 1)
                            {
                                if (i == leftup[n, 0] && j == leftup[n, 1] || i == leftup[n, 0] && j == rightdown[n, 1] || i == rightdown[n, 0] && j == rightdown[n, 1] || i == rightdown[n, 0] && j == leftup[n, 1])
                                {
                                    blockcolor[n, i-1, j-1] = Brushes.DarkGoldenrod;
                                }
                            }
                        }
                    }
                }
                color.Add(4);
                Draw();
                button4.BackColor = Color.DarkGoldenrod;
                button4flag = true;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            int stonesum = mass * 8;
            int count = 0;
            for (; pictureBox1.Width > interval * (count + 2) + (count + 1) * stonesum; count++) { }
            Bitmap canvas = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            Graphics g = Graphics.FromImage(canvas);
            if (button5flag)
            {
                color.Remove(5);
                paint();
                Draw();
                button5.BackColor = SystemColors.Control;
                button5flag = false;
            }
            else
            {
                for (int n = 0; n < sum; n++)
                {
                    for (int i = 1; i < 9; i++)
                    {
                        for (int j = 1; j < 9; j++)
                        {
                            if (parts[n, i, j] == 1)
                            {
                                if (leftup[n,0] == i || leftup[n,1] == j || rightdown[n,0] == i || rightdown[n,1] == j)
                                    blockcolor[n, i - 1, j - 1] = Brushes.Brown;
                            }
                        }
                    }
                }
                color.Add(5);
                Draw();
                button5.BackColor = Color.Brown;
                button5flag = true;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (charflag)
            {
                charflag = false;
                button6.BackColor = SystemColors.Control;
            }
            else
            {
                charflag = true;
                button6.BackColor = Color.Gray;
            }
            Draw();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            int flagcount = 0;
            if (markflag)
            {
                paint();
                for (int n = 0; n < sum; n++)
                {
                    for (int i = 1; i <9; i++)
                    {
                        for (int j = 1; j < 9; j++)
                        {
                            flagcount = 0;
                            if (parts[n, i, j] == 0)
                            {
                                flagcount = parts[n, i - 1, j] + parts[n, i, j - 1] + parts[n, i + 1, j] + parts[n, i, j + 1];
                                if (flagcount != 0)
                                {
                                    blockcolor[n, i - 1, j - 1] = Brushes.White;
                                }
                            }
                        }
                    }
                }
                markflag = false;
                charflag = false;
                list1.ClearSelected();
                list1.Items.Clear(); 
                list2.Items.Clear(); 
                list1.Visible = false;
                list2.Visible = false;
                label1.Visible = false;
                button10.Visible = false;
                button11.Visible = false;
                Draw();
                if (form2_flag)
                {
                    f2.list1_clear_flag = false;
                    f2.Close();
                    paint2();
                    list1.Items.Clear(); 
                    form2_flag = false;
                }
                button6.BackColor = SystemColors.Control;
                button7.BackColor = SystemColors.Control;
            }
            else
            {
                for (int n = 0; n < sum; n++)
                {
                    for (int i = 1; i < 9; i++)
                    {
                        for (int j = 1; j < 9; j++)
                        {
                            flagcount = 0;
                            if (parts[n, i, j] == 0)
                            {
                                flagcount = parts[n, i - 1, j] + parts[n, i, j - 1] + parts[n, i + 1, j] + parts[n, i, j + 1];
                                if (flagcount != 0)
                                {
                                    blockcolor[n, i - 1, j - 1] = Brushes.LightSalmon;
                                }
                            }
                        }
                    }
                }
                markflag = true;
                //list1_add_flag = true;
                charflag = true;
                Draw();
                for (int i = 1; i <= sum;i++ )
                    list1.Items.Add(i.ToString());
                //list1_add_flag = false;
                button6.BackColor = Color.Gray;
                button7.BackColor = Color.Gray;
                list1.Show();
                button10.Show();
                button11.Show();
            }
        }

        private void list1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (list1.SelectedIndex == -1)
                return;
            list2.Items.Clear(); 
            for (int n = 0; n < sum; n++)
                for (int i = 0; i < 8; i++)
                    for (int j = 0; j < 8; j++)
                    {
                        if (blockcolor[n, i, j] == Brushes.OrangeRed)
                            blockcolor[n, i, j] = Brushes.Aqua;
                    }
            checkflag = true;
            int target = int.Parse(list1.Text)-1;
            for (int i = 1; i < 9; i++)
                for (int j = 1; j < 9; j++)
                {
                    vir_map[i + 7, j + 7] = false;
                    if (parts[space_info[target].n, i, j] == 1)
                    {
                        vir_map[i + 7, j + 7] = true;
                    }
                }
            if (before_target1 != -1)
                blockcolor[space_info[before_target1].n, space_info[before_target1].x - 1, space_info[before_target1].y - 1] = Brushes.LightSalmon;
            if (form2_flag)
            {
                blockcolor[space_info[target].n, space_info[target].x - 1, space_info[target].y - 1] = Brushes.Red;
                f2.blockcolor[f2.space_info[target].x,f2.space_info[target].y] = Brushes.Red;
                f2.massflag = true;
                f2.Draw();
                if (list_n.Count != 0)
                    f2.paint(list_n[list_n.Count - 1], list_n.Count - 1);
                f2.massflag = false;
                check(target);
                Draw();
            }
            else
            {
                Draw();
            }
            label1.Text = "Total:" + labelsum.ToString();
            checkflag = false;
            before_target1 = target;
        }

        private void check(int target)
        {
            int version = 0;
            bool loop = true;
            bool success;
            while (loop)
            {
                switch (version)
                {
                    case 0:
                        for (int N = 0; N < sum; N++)
                        {
                            if (list_n.Contains(N))
                            {
                                continue;
                            }
                            for (int i = 0; i < total[N]; i++)
                            {
                                success = true;
                                for (int j = 0; j < total[N]; j++)
                                {
                                    if (f2.vir_map[f2.space_info[target].x + (mass_info[N, j].x - mass_info[N, i].x), f2.space_info[target].y + (mass_info[N, j].y - mass_info[N, i].y)])
                                    {
                                        success = false;
                                    }
                                }
                                if (success && blockcolor[N, mass_info[N, i].x, mass_info[N, i].y] != Brushes.Gray)
                                {
                                    blockcolor[N, mass_info[N, i].x, mass_info[N, i].y] = Brushes.OrangeRed;
                                }
                            }
                        }
                        break;
                    case 1:
                        for (int N = 0; N < sum; N++)
                        {
                            if (list_n.Contains(N))
                            {
                                continue;
                            }
                            for (int i = 0; i < total[N]; i++)
                            {
                                success = true;
                                for (int j = 0; j < total[N]; j++)
                                {
                                    if (f2.vir_map[f2.space_info[target].x + (mass_info[N, j].y - mass_info[N, i].y), f2.space_info[target].y - (mass_info[N, j].x - mass_info[N, i].x)])
                                    {
                                        success = false;
                                    }
                                }
                                if (success && blockcolor[N, mass_info[N, i].x, mass_info[N, i].y] != Brushes.Gray)
                                {
                                    blockcolor[N, mass_info[N, i].x, mass_info[N, i].y] = Brushes.OrangeRed;
                                }
                            }
                        }
                        break;
                    case 2:
                        for (int N = 0; N < sum; N++)
                        {
                            if (list_n.Contains(N))
                            {
                                continue;
                            }
                            for (int i = 0; i < total[N]; i++)
                            {
                                success = true;
                                for (int j = 0; j < total[N]; j++)
                                {
                                    if (f2.vir_map[f2.space_info[target].x - (mass_info[N, j].x - mass_info[N, i].x), f2.space_info[target].y - (mass_info[N, j].y - mass_info[N, i].y)])
                                    {
                                        success = false;
                                    }
                                }
                                if (success && blockcolor[N, mass_info[N, i].x, mass_info[N, i].y] != Brushes.Gray)
                                {
                                    blockcolor[N, mass_info[N, i].x, mass_info[N, i].y] = Brushes.OrangeRed;
                                }
                            }
                        }
                        break;
                    case 3:
                        for (int N = 0; N < sum; N++)
                        {
                            if (list_n.Contains(N))
                            {
                                continue;
                            }
                            for (int i = 0; i < total[N]; i++)
                            {
                                success = true;
                                for (int j = 0; j < total[N]; j++)
                                {
                                    if (f2.vir_map[f2.space_info[target].x - (mass_info[N, j].y - mass_info[N, i].y), f2.space_info[target].y + (mass_info[N, j].x - mass_info[N, i].x)])
                                    {
                                        success = false;
                                    }
                                }
                                if (success && blockcolor[N, mass_info[N, i].x, mass_info[N, i].y] != Brushes.Gray)
                                {
                                    blockcolor[N, mass_info[N, i].x, mass_info[N, i].y] = Brushes.OrangeRed;
                                }
                            }
                        }
                        break;
                    case 4:
                        for (int N = 0; N < sum; N++)
                        {
                            if (list_n.Contains(N))
                            {
                                continue;
                            }
                            for (int i = 0; i < total[N]; i++)
                            {
                                success = true;
                                for (int j = 0; j < total[N]; j++)
                                {
                                    if (f2.vir_map[f2.space_info[target].x - (mass_info[N, j].x - mass_info[N, i].x), f2.space_info[target].y + (mass_info[N, j].y - mass_info[N, i].y)])
                                    {
                                        success = false;
                                    }
                                }
                                if (success && blockcolor[N, mass_info[N, i].x, mass_info[N, i].y] != Brushes.Gray)
                                {
                                    blockcolor[N, mass_info[N, i].x, mass_info[N, i].y] = Brushes.OrangeRed;
                                }
                            }
                        }
                        break;
                    case 5:
                        for (int N = 0; N < sum; N++)
                        {
                            if (list_n.Contains(N))
                            {
                                continue;
                            }
                            for (int i = 0; i < total[N]; i++)
                            {
                                success = true;
                                for (int j = 0; j < total[N]; j++)
                                {
                                    if (f2.vir_map[f2.space_info[target].x + (mass_info[N, j].y - mass_info[N, i].y), f2.space_info[target].y + (mass_info[N, j].x - mass_info[N, i].x)])
                                    {
                                        success = false;
                                    }
                                }
                                if (success && blockcolor[N, mass_info[N, i].x, mass_info[N, i].y] != Brushes.Gray)
                                {
                                    blockcolor[N, mass_info[N, i].x, mass_info[N, i].y] = Brushes.OrangeRed;
                                }
                            }
                        }
                        break;
                    case 6:
                        for (int N = 0; N < sum; N++)
                        {
                            if (list_n.Contains(N))
                            {
                                continue;
                            }
                            for (int i = 0; i < total[N]; i++)
                            {
                                success = true;
                                for (int j = 0; j < total[N]; j++)
                                {
                                    if (f2.vir_map[f2.space_info[target].x + (mass_info[N, j].x - mass_info[N, i].x), f2.space_info[target].y  - (mass_info[N, j].y - mass_info[N, i].y)])
                                    {
                                        success = false;
                                    }
                                }
                                if (success && blockcolor[N, mass_info[N, i].x, mass_info[N, i].y] != Brushes.Gray)
                                {
                                    blockcolor[N, mass_info[N, i].x, mass_info[N, i].y] = Brushes.OrangeRed;
                                }
                            }
                        }
                        break;
                    case 7:
                        for (int N = 0; N < sum; N++)
                        {
                            if (list_n.Contains(N))
                            {
                                continue;
                            }
                            for (int i = 0; i < total[N]; i++)
                            {
                                success = true;
                                for (int j = 0; j < total[N]; j++)
                                {
                                    if (f2.vir_map[f2.space_info[target].x - (mass_info[N, j].y - mass_info[N, i].y), f2.space_info[target].y - (mass_info[N, j].x - mass_info[N, i].x)])
                                    {
                                        success = false;
                                    }
                                }
                                if (success && blockcolor[N, mass_info[N, i].x, mass_info[N, i].y] != Brushes.Gray)
                                {
                                    blockcolor[N, mass_info[N, i].x, mass_info[N, i].y] = Brushes.OrangeRed;
                                }
                            }
                        }
                        break;
                    default:
                        loop = false;
                        break;
                }
                version++;
            }

        }

        private void paint()
        {
            int flagcount = 0; 
            for (int n = 0; n < sum; n++)
                for (int i = 1; i < 9; i++)
                {
                    for (int j = 1; j < 9; j++)
                    {
                        flagcount = 0;
                        if (parts[n, i, j] == 1)
                        {
                            for (int k = 0; k < color.Count; k++)
                            {
                                flagcount = parts[n, i - 1, j] + parts[n, i, j - 1] + parts[n, i + 1, j] + parts[n, i, j + 1];
                                if (color[k] == 2)
                                {
                                    if (flagcount == 1 || flagcount == 0)
                                    {
                                        blockcolor[n, i - 1, j - 1] = Brushes.DeepPink;
                                    }
                                }
                                else if (color[k] == 4)
                                {
                                    if (i == leftup[n, 0] && j == leftup[n, 1] || i == leftup[n, 0] && j == rightdown[n, 1] || i == rightdown[n, 0] && j == rightdown[n, 1] || i == rightdown[n, 0] && j == leftup[n, 1])
                                    {
                                        blockcolor[n, i - 1, j - 1] = Brushes.DarkGoldenrod;
                                    }
                                }
                                else if (color[k] == 5)
                                {
                                    if (leftup[n, 0] == i || leftup[n, 1] == j || rightdown[n, 0] == i || rightdown[n, 1] == j)
                                    {
                                        blockcolor[n, i - 1, j - 1] = Brushes.Brown;
                                    }
                                }
                                else
                                {
                                    blockcolor[n, i - 1, j - 1] = Brushes.Aqua;
                                }
                            }
                        }
                    }
                }
            if (form2_flag)
                if (list_n.Count != 0)
                {
                    paint3(list_n[list_n.Count - 1], list_n.Count - 1);
                    //paint4();
                }
        }

        public void paint2()
        {
            int flagcount = 0;
            for(int n=0;n<sum;n++)
                for(int i=1;i<9;i++)
                    for(int j=1;j<9;j++)
                    {
                        flagcount = 0;
                        if (parts[n, i, j] == 0)
                        {
                            flagcount = parts[n, i - 1, j] + parts[n, i, j - 1] + parts[n, i + 1, j] + parts[n, i, j + 1];
                            if (flagcount != 0)
                            {
                                blockcolor[n, i - 1, j - 1] = Brushes.LightSalmon;
                            }
                        }
                        else
                            blockcolor[n, i - 1, j - 1] = Brushes.Aqua;
                    }
            Draw();
        }

        private void paint3(int n, int location)
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                { 
                    if(parts[n, i + 1, j + 1] == 1)
                    blockcolor[n, i, j] = Brushes.Gray;
                }
            }
            if (location != 0)
                paint3(list_n[location-1],location-1);
        }
        
        private void list2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (list2.Text == "")
                return;
            int i;
            int sum_total = 0;
            int target2 = int.Parse(list2.Text)-1;
            for(int n = 0 ; n < sum ; n++)
                for(i = 0 ; i < 8 ; i++)
                    for(int j = 0 ; j < 8 ; j++)
                    {
                        if (parts[n, i + 1, j + 1] == 1&&blockcolor[n, i, j]!=Brushes.Gray)
                        {
                            blockcolor[n, i, j] = Brushes.Aqua;
                        }
                    }
            for(i = 0 ; sum_total+total[i] <= target2 ; i++)
                sum_total += total[i];
            checkflag = true;
            blockcolor[i, mass_info[i, target2 - sum_total].x , mass_info[i, target2 - sum_total].y ] = Brushes.Red;
            Draw();
            checkflag = false;
            blockcolor[i, mass_info[i, target2 - sum_total].x , mass_info[i, target2 - sum_total].y ] = Brushes.Aqua;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (masscount == 1)
                return;
            masscount--;
            int i;
            int buffer = interval * (masscount + 1) + masscount * (mass + 1) * 8;
            for (i = mass; buffer < pictureBox1.Width; i++)
            {
                buffer = interval * (masscount + 1) + masscount * (i + 2) * 8;
            }
            mass = i;
            Draw();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (masscount == 8)
                return;
            masscount++;
            int i;
            int buffer = interval * (masscount + 1) + masscount * (mass - 1) * 8;
            for (i = mass; buffer > pictureBox1.Width; i--)
            {
                buffer = interval * (masscount + 1) + masscount * (i - 1) * 8;
            }
            mass = i;
            Draw();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if(list1.Text == ""&& form2_flag == false)
            {
                return;
            }
            if (list1.Text != "" && list2.Text == "" && form2_flag)
            {
                return;
            }
            if(list1.Text == "")
            {
                if (f2.put_target.Count != 0)
                {
                    //f2.output(list1.Text);
                    int temp;
                    int target = f2.put_target.Pop();
                    int target2 = f2.put_againtarget2.Pop();
                    int temp_target2 = temp = f2.put_target2.Pop();
                    f2.put_target2.Push(temp);
                    f2.check(target, target2, temp_target2);
                    f2.Draw2();
                }
                else
                {
                    return;
                }
            }
            else if (list2.Text != "")
            {
                    if (f2 == null)
                        return;
                    int target = int.Parse(list1.Text) - 1;
                    int target2 = int.Parse(list2.Text) - 1;
                    f2.min = 0;
                    f2.max = total[0];
                    f2.n = 0;
                    for (int i = 1; f2.max < target2 + 1; i++)
                    {
                        f2.min = f2.max;
                        f2.max += total[i];
                        f2.n = i;
                    }
                    for (int i = 0; i < f2.max - f2.min; i++)
                    {
                        blockcolor[f2.n, mass_info[f2.n, i].x, mass_info[f2.n, i].y] = Brushes.Gray;
                    }
                    target2 -= f2.min;
                    f2.check(target, target2, int.Parse(list2.Text) - 1);
                    f2.Draw2();
                    //f2.output(f2.put_partsnum.Count.ToString());
                
            }
            else
            {
                int target = 0;
                if (f2 != null)
                {
                    f2.list1_clear_flag = false;
                    //int tempx = f2.Location.X;
                    //int tempy = f2.Location.Y;
                    f2.Close();
                    //f2 = new Form2();
                    //form2_flag = true;
                    paint2();
                    //list2.Show();
                    /*f2.Location = new Point(tempx, tempy);*/
                }
                f2 = new Form2();
                form2_flag = true;
                /* f2.Location = new Point((System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width) / 2 - (interval * 2 + 3 * (mass - 15) * 8 + 25) / 2, (System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height) / 2 - (interval * 2 + 3 * (mass - 15) * 8 + 35) / 2);*/
                int n = int.Parse(list1.Text) - 1;
                list_n.Add(n);
                f2.TopMost = true;
                f2.Size = new Size(interval * 2 + 3 * (mass - 15) * 8 + 25, interval * 2 + 3 * (mass - 15) * 8 + 35);
                f2.FormBorderStyle = FormBorderStyle.FixedSingle;
                f2.set(mass-15);
                f2.Show();
                f2.Draw(this);
                for (int i = 0; i < 24; i++)
                    for (int j = 0; j < 24; j++)
                        f2.vir_map[i, j] = false;
                for (int i = 0; space_info[i].n != n; i++)
                {
                    target = i + 1;
                }
                /*
                blockcolor[space_info[target].n, space_info[target].x - 1, space_info[target].y - 1] = Brushes.Red;
                check(target);
                */
                for (int i = 0; i < total[space_info[target].n]; i++)
                {
                    f2.vir_map[mass_info[space_info[target].n, i].x + 8, mass_info[space_info[target].n, i].y + 8] = true;
                    blockcolor[space_info[target].n, mass_info[space_info[target].n, i].x, mass_info[space_info[target].n, i].y] = Brushes.Gray;
                    //f2.blockcolor[mass_info[space_info[target].n, i].x + 8, mass_info[space_info[target].n, i].y + 8] = Brushes.Red;
                    f2.blockcolor[mass_info[space_info[target].n, i].x + 8, mass_info[space_info[target].n, i].y + 8] = Brushes.Aqua;
                }
                f2.Draw2();
                //f2.output(f2.put_partsnum.Count.ToString());
                for (int i = 0; space_info[i].n <= space_info[target].n&&i<space_sum-1; i++)
                {
                    if (space_info[i].n == space_info[target].n)
                    {
                        f2.space_info[i].x = space_info[i].x + 7;
                        f2.space_info[i].y = space_info[i].y + 7;
                        //MessageBox.Show((f2.space_info[i].x).ToString()+" "+(f2.space_info[i].y).ToString());
                    }
                }
                list2.Show();
                label1.Show();
                firstflag = false;
            }
            Draw();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (f2 == null)
                return;
            else
            {
                if (f2.put_partsnum.Count == 0)
                    return;
                int target = f2.put_target2.Pop();
                f2.back(target);
                f2.Draw2();
                Draw();
                //f2.output(f2.put_partsnum.Count.ToString());
            }
        }

    }
}