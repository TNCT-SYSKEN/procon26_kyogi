using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BlockChecker
{
    public partial class Form2 : Form
    {
        public Form2()
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
        int mass;
        PictureBox pictureBox1 = new PictureBox();
        Panel panel2 = new Panel();

        public bool[,] vir_map = new bool[24, 24];
        public int min, max;
        public int n;
        public Stack<int> put_target= new Stack<int>();
        public Stack<int> put_target2 = new Stack<int>();
        public Stack<int> put_partsnum = new Stack<int>();
        public Stack<int> put_againtarget2 = new Stack<int>();
        public Stack<int> stackmax = new Stack<int>();
        public Stack<int> stackmin = new Stack<int>();
        public Stack<int> stack_N = new Stack<int>();
        public Brush[,] blockcolor = new Brush[24, 24];
        public bool list1_clear_flag = true;

        public information[] mass_info;
        public information[] space_info;

        Form1 f1;

        public bool massflag = false;

        private void Form2_Load(object sender, EventArgs e)
        {
            panel2.Location = new Point(0, 0);
            panel2.Size = new Size(this.Size.Width - 291, this.Size.Height - 52);
            panel2.Size = new Size(this.Size.Width, this.Size.Height);
            pictureBox1.BackColor = System.Drawing.Color.White;
            pictureBox1.Location = new Point(0, 0);
            pictureBox1.Size = new Size(this.Size.Width, this.Size.Height);
            panel2.Controls.Add(pictureBox1);
            this.Controls.Add(panel2);
            for (int i = 0; i < 24; i++)
                for (int j = 0; j < 24; j++)
                    blockcolor[i, j] = Brushes.White;
        }

        public void Draw()
        {
            Bitmap canvas = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            Graphics g = Graphics.FromImage(canvas);
            for (int i = 0; i <= 24; i++)
            {
                g.DrawLine(Pens.Black, interval, interval + i * mass, interval + 24 * mass, interval + i * mass);
                g.DrawLine(Pens.Black, interval + i * mass, interval, interval + i * mass, interval + 24 * mass);

            }
            if (massflag)
            {
                /*for(int i = 0; i<f1.space_sum;i++)
                    {
                        if(space_info[i].x!=0&&space_info[i].y!=0)
                        g.FillRectangle(Brushes.Red, interval + space_info[i].x * mass + 1, interval + space_info[i].y * mass + 1, mass - 1, mass - 1);
                    }*/

                for (int i = 0; i < 24; i++)
                {
                    for (int j = 0; j < 24; j++)
                    {
                        if(blockcolor[i,j]==Brushes.DeepPink||blockcolor[i,j]==Brushes.DarkGoldenrod||blockcolor[i,j]==Brushes.Brown)
                        {
                            g.FillRectangle(blockcolor[i, j], interval + i * mass + 1, interval + j * mass + 1, mass - 1, mass - 1);
                            blockcolor[i, j] = Brushes.Aqua;
                        }
                        else if(blockcolor[i,j]==Brushes.Red)
                        {
                            g.FillRectangle(blockcolor[i, j], interval + i * mass + 1, interval + j * mass + 1, mass - 1, mass - 1);
                            blockcolor[i, j] = Brushes.White;
                        }
                        else if (vir_map[i, j])
                        {
                            g.FillRectangle(blockcolor[i,j], interval + i * mass + 1, interval + j * mass + 1, mass - 1, mass - 1);
                            blockcolor[i, j] = Brushes.Aqua;
                        }
                    }
                }
            }
            g.Dispose();
            pictureBox1.Image = canvas;
        }

        public void Draw2()
        {
            paint2();
            f1.list1.Items.Clear();
            f1.list2.Items.Clear();
            f1.list1_add_flag = true;
            massflag = true;
            Draw();
            massflag = false;
            if (f1.list_n.Count != 0)
                paint(f1.list_n[f1.list_n.Count - 1], f1.list_n.Count - 1);
            f1.Draw();
            f1.list1_add_flag = false;
        }

        public void Draw(Form1 form)
        {
            f1 = form;
            mass_info = new information[f1.mass_sum];
            space_info = new information[f1.space_sum];
            Draw();
        }

        public void paint(int n, int location)
        {
            for (int i = 0; i < f1.space_sum; i++)
            {
                if (f1.space_info[i].n > n)
                    break;
                if (space_info[i].x == 0 && space_info[i].y == 0 || f1.space_info[i].n!=n)
                    continue;
                if (vir_map[space_info[i].x, space_info[i].y])
                    f1.blockcolor[f1.space_info[i].n, f1.space_info[i].x - 1, f1.space_info[i].y - 1] = Brushes.Gray;
            }
            if (location != 0)
                paint(f1.list_n[location - 1], location - 1);
            /*int n = stack_N.Pop();
            int max = stackmax.Pop();
            int min = stackmin.Pop();
                for(int i = 0; i<f1.space_sum;i++)
                {
                    if (space_info[i].x == 0 && space_info[i].y == 0)
                        continue;
                    if (f1.space_info[i].n > n)
                        break;
                    if (vir_map[space_info[i].x,space_info[i].y])
                        f1.blockcolor[f1.space_info[i].n, f1.space_info[i].x - 1, f1.space_info[i].y - 1] = Brushes.Gray;
                }
            if (stack_N.Count != 0)
                paint();
            stack_N.Push(n);
            stackmax.Push(max);
            stackmin.Push(min);*/
        }

        private void paint2()
        {
            int flagcount;
            for (int n = 0; n < f1.sum; n++)
            {
                for (int i = 1; i < 9; i++)
                {
                    for (int j = 1; j < 9; j++)
                    {
                        flagcount = 0;
                        if (f1.parts[n, i, j] == 0)
                        {
                            flagcount = f1.parts[n, i - 1, j] + f1.parts[n, i, j - 1] + f1.parts[n, i + 1, j] + f1.parts[n, i, j + 1];
                            if (flagcount != 0)
                            {
                                f1.blockcolor[n, i - 1, j - 1] = Brushes.LightSalmon;
                            }
                        }
                    }
                }
            }
            return;
        }

        public void set (int a)
        {
            mass = a;
        }

        public void back (int target)
        {
            int  version = put_partsnum.Pop() - 1;
            int temp = put_againtarget2.Pop();
            int temp2 = put_target.Pop();
            //output("put_target2.Count" + put_target2.Count.ToString());
            max = stackmax.Pop();
            min = stackmin.Pop();
            n = stack_N.Pop();
            //output(version.ToString());
            //output(n.ToString());
            //output(temp.ToString());
            switch (version)
            {
                case 0:
                        for (int j = 0; j < max - min; j++)
                        {
                            vir_map[space_info[temp2].x + (f1.mass_info[n, j].x - f1.mass_info[n, temp].x), space_info[temp2].y + (f1.mass_info[n, j].y - f1.mass_info[n, temp].y)] = false;
                            blockcolor[space_info[temp2].x + (f1.mass_info[n, j].x - f1.mass_info[n, temp].x), space_info[temp2].y + (f1.mass_info[n, j].y - f1.mass_info[n, temp].y)] = Brushes.White;
                            f1.blockcolor[n, f1.mass_info[n, j].x, f1.mass_info[n, j].y] = Brushes.Aqua;
                            //output(n.ToString()+":n "+(f1.mass_info[n,j].x).ToString()+":mass_info[n,j].x "+(f1.mass_info[n,j].y).ToString()+":f1.mass_info[n,j].y");
                            //output((f1.space_info[temp2].x + 7 + f1.mass_info[n, j].x - f1.mass_info[n, temp].x).ToString()+" "+ (f1.space_info[temp2].y + 7 + f1.mass_info[n, j].y - f1.mass_info[n, temp].y).ToString());
                            //output((f1.space_info[temp2].x).ToString()+" "+ (f1.mass_info[n, j].x).ToString()+" "+(f1.mass_info[n, temp].x).ToString()+" "+( f1.space_info[temp2].y).ToString() +" "+ ( f1.mass_info[n, j].y).ToString()+" "+ (f1.mass_info[n, temp].y).ToString()+Environment.NewLine+vir_map[f1.space_info[temp2].x + 7 + f1.mass_info[n, j].x - f1.mass_info[n, temp].x, f1.space_info[temp2].y + 7 + f1.mass_info[n, j].y - f1.mass_info[n, temp].y].ToString() );
                            //output(temp2.ToString()+":temp2 "+temp.ToString()+":temp "+n.ToString()+":n "+j+":j ");
                        }
                    break;
                case 1:
                        for (int j = 0; j < max - min; j++)
                        {
                            vir_map[space_info[temp2].x + (f1.mass_info[n, j].y - f1.mass_info[n, temp].y), space_info[temp2].y - (f1.mass_info[n, j].x - f1.mass_info[n, temp].x)] = false;
                            blockcolor[space_info[temp2].x + (f1.mass_info[n, j].y - f1.mass_info[n, temp].y), space_info[temp2].y - (f1.mass_info[n, j].x - f1.mass_info[n, temp].x)] = Brushes.White;
                            f1.blockcolor[n, f1.mass_info[n, j].x, f1.mass_info[n, j].y] = Brushes.Aqua;
                        }
                    break;
                case 2:
                        for (int j = 0; j < max - min; j++)
                        {
                            vir_map[space_info[temp2].x - (f1.mass_info[n, j].x - f1.mass_info[n, temp].x), space_info[temp2].y - (f1.mass_info[n, j].y - f1.mass_info[n, temp].y)] = false;
                            blockcolor[space_info[temp2].x - (f1.mass_info[n, j].x - f1.mass_info[n, temp].x), space_info[temp2].y - (f1.mass_info[n, j].y - f1.mass_info[n, temp].y)] = Brushes.White;
                            f1.blockcolor[n, f1.mass_info[n, j].x, f1.mass_info[n, j].y] = Brushes.Aqua;
                        }
                    break;
                case 3:
                        for (int j = 0; j < max - min; j++)
                        {
                            vir_map[space_info[temp2].x - (f1.mass_info[n, j].y - f1.mass_info[n, temp].y), space_info[temp2].y + (f1.mass_info[n, j].x - f1.mass_info[n, temp].x)] = false;
                            blockcolor[space_info[temp2].x - (f1.mass_info[n, j].y - f1.mass_info[n, temp].y), space_info[temp2].y + (f1.mass_info[n, j].x - f1.mass_info[n, temp].x)] = Brushes.White;
                            f1.blockcolor[n, f1.mass_info[n, j].x, f1.mass_info[n, j].y] = Brushes.Aqua;

                        }
                    break;
                case 4:
                        for (int j = 0; j < max - min; j++)
                        {
                            vir_map[space_info[temp2].x - (f1.mass_info[n, j].x - f1.mass_info[n, temp].x), space_info[temp2].y + (f1.mass_info[n, j].y - f1.mass_info[n, temp].y)] = false;
                            blockcolor[space_info[temp2].x - (f1.mass_info[n, j].x - f1.mass_info[n, temp].x), space_info[temp2].y + (f1.mass_info[n, j].y - f1.mass_info[n, temp].y)] = Brushes.White;
                            f1.blockcolor[n, f1.mass_info[n, j].x, f1.mass_info[n, j].y] = Brushes.Aqua;
                        }
                    break;
                case 5:
                        for (int j = 0; j < max - min; j++)
                        {
                            vir_map[space_info[temp2].x + (f1.mass_info[n, j].y - f1.mass_info[n, temp].y), space_info[temp2].y + (f1.mass_info[n, j].x - f1.mass_info[n, temp].x)] = false;
                            blockcolor[space_info[temp2].x + (f1.mass_info[n, j].y - f1.mass_info[n, temp].y), space_info[temp2].y + (f1.mass_info[n, j].x - f1.mass_info[n, temp].x)] = Brushes.White;
                            f1.blockcolor[n, f1.mass_info[n, j].x, f1.mass_info[n, j].y] = Brushes.Aqua;
                        }
                    break;
                case 6:
                        for (int j = 0; j < max - min; j++)
                        {
                            vir_map[space_info[temp2].x + (f1.mass_info[n, j].x - f1.mass_info[n, temp].x), space_info[temp2].y - (f1.mass_info[n, j].y - f1.mass_info[n, temp].y)] = false;
                            blockcolor[space_info[temp2].x + (f1.mass_info[n, j].x - f1.mass_info[n, temp].x), space_info[temp2].y - (f1.mass_info[n, j].y - f1.mass_info[n, temp].y)] = Brushes.White;
                            f1.blockcolor[n, f1.mass_info[n, j].x, f1.mass_info[n, j].y] = Brushes.Aqua;
                            //output((f1.space_info[temp2].x + 7 + (f1.mass_info[n, j].x - f1.mass_info[n, temp].x)).ToString()+" "+( f1.space_info[temp2].y + 7 - (f1.mass_info[n, j].y - f1.mass_info[n, temp].y)).ToString());
                            //output((f1.space_info[temp2].x).ToString() + " " + (f1.mass_info[n, j].x).ToString() + " " + (f1.mass_info[n, temp].x).ToString() + " " + (f1.space_info[temp2].y).ToString() + " " + (f1.mass_info[n, j].y).ToString() + " " + (f1.mass_info[n, temp].y).ToString());
                            //output(temp2.ToString() + ":temp2 " + temp.ToString() + ":temp " + n.ToString() + ":n " + j + ":j ");
                        }
                    break;
                case 7:
                        for (int j = 0; j < max - min; j++)
                        {
                            vir_map[space_info[temp2].x - (f1.mass_info[n, j].y - f1.mass_info[n, temp].y), space_info[temp2].y - (f1.mass_info[n, j].x - f1.mass_info[n, temp].x)] = false;
                            blockcolor[space_info[temp2].x - (f1.mass_info[n, j].y - f1.mass_info[n, temp].y), space_info[temp2].y - (f1.mass_info[n, j].x - f1.mass_info[n, temp].x)] = Brushes.White;
                            f1.blockcolor[n, f1.mass_info[n, j].x, f1.mass_info[n, j].y] = Brushes.Aqua;
                        }
                    break;
            }
            for (int i = 0; i < f1.space_sum; i++)
            {
                if (f1.space_info[i].n > n)
                    break;
                if (f1.space_info[i].n == n)
                {
                    space_info[i].x = 0;
                    space_info[i].y = 0;
                }
            }
            back2(target);
                paint2();
            f1.list1.Items.Clear();
            f1.list2.Items.Clear();
            f1.list_n.Remove(f1.list_n[f1.list_n.Count-1]);
            f1.list1_add_flag = true;
            f1.Draw();
            f1.list1_add_flag = false;
        }

        public void back2(int target)
        {
            if (put_partsnum.Count == 0)
                return;
            int version = put_partsnum.Pop() - 1;
            int temp = put_againtarget2.Pop();
            int temp2 = put_target.Pop();
            //output("put_target2.Count" + put_target2.Count.ToString());
            max = stackmax.Pop();
            min = stackmin.Pop();
            n = stack_N.Pop();
            //output(n.ToString());
            //output(temp.ToString());
            switch (version)
            {
                case 0:
                    for (int j = 0; j < max - min; j++)
                    {
                        blockcolor[space_info[temp2].x + (f1.mass_info[n, j].x - f1.mass_info[n, temp].x), space_info[temp2].y + (f1.mass_info[n, j].y - f1.mass_info[n, temp].y)] = Brushes.LimeGreen;
                        //output((f1.space_info[temp2].x + 7 + f1.mass_info[n, j].x - f1.mass_info[n, temp].x).ToString()+" "+ (f1.space_info[temp2].y + 7 + f1.mass_info[n, j].y - f1.mass_info[n, temp].y).ToString());
                        //output((f1.space_info[temp2].x).ToString()+" "+ (f1.mass_info[n, j].x).ToString()+" "+(f1.mass_info[n, temp].x).ToString()+" "+( f1.space_info[temp2].y).ToString() +" "+ ( f1.mass_info[n, j].y).ToString()+" "+ (f1.mass_info[n, temp].y).ToString()+Environment.NewLine+vir_map[f1.space_info[temp2].x + 7 + f1.mass_info[n, j].x - f1.mass_info[n, temp].x, f1.space_info[temp2].y + 7 + f1.mass_info[n, j].y - f1.mass_info[n, temp].y].ToString() );
                        //output(temp2.ToString()+":temp2 "+temp.ToString()+":temp "+n.ToString()+":n "+j+":j ");
                    }
                    break;
                case 1:
                    for (int j = 0; j < max - min; j++)
                    {
                        blockcolor[space_info[temp2].x + (f1.mass_info[n, j].y - f1.mass_info[n, temp].y), space_info[temp2].y - (f1.mass_info[n, j].x - f1.mass_info[n, temp].x)] = Brushes.LimeGreen;
                    }
                    break;
                case 2:
                    for (int j = 0; j < max - min; j++)
                    {
                        blockcolor[space_info[temp2].x - (f1.mass_info[n, j].x - f1.mass_info[n, temp].x), space_info[temp2].y - (f1.mass_info[n, j].y - f1.mass_info[n, temp].y)] = Brushes.LimeGreen;
                    }
                    break;
                case 3:
                    for (int j = 0; j < max - min; j++)
                    {
                        blockcolor[space_info[temp2].x - (f1.mass_info[n, j].y - f1.mass_info[n, temp].y), space_info[temp2].y + (f1.mass_info[n, j].x - f1.mass_info[n, temp].x)] = Brushes.LimeGreen;

                    }
                    break;
                case 4:
                    for (int j = 0; j < max - min; j++)
                    {
                        blockcolor[space_info[temp2].x - (f1.mass_info[n, j].x - f1.mass_info[n, temp].x), space_info[temp2].y + (f1.mass_info[n, j].y - f1.mass_info[n, temp].y)] = Brushes.LimeGreen;
                    }
                    break;
                case 5:
                    for (int j = 0; j < max - min; j++)
                    {
                        blockcolor[space_info[temp2].x + (f1.mass_info[n, j].y - f1.mass_info[n, temp].y), space_info[temp2].y + (f1.mass_info[n, j].x - f1.mass_info[n, temp].x)] = Brushes.LimeGreen;
                    }
                    break;
                case 6:
                    for (int j = 0; j < max - min; j++)
                    {
                        blockcolor[space_info[temp2].x + (f1.mass_info[n, j].x - f1.mass_info[n, temp].x), space_info[temp2].y - (f1.mass_info[n, j].y - f1.mass_info[n, temp].y)] = Brushes.LimeGreen;
                        //output((f1.space_info[temp2].x + 7 + (f1.mass_info[n, j].x - f1.mass_info[n, temp].x)).ToString()+" "+( f1.space_info[temp2].y + 7 - (f1.mass_info[n, j].y - f1.mass_info[n, temp].y)).ToString());
                        //output((f1.space_info[temp2].x).ToString() + " " + (f1.mass_info[n, j].x).ToString() + " " + (f1.mass_info[n, temp].x).ToString() + " " + (f1.space_info[temp2].y).ToString() + " " + (f1.mass_info[n, j].y).ToString() + " " + (f1.mass_info[n, temp].y).ToString());
                        //output(temp2.ToString() + ":temp2 " + temp.ToString() + ":temp " + n.ToString() + ":n " + j + ":j ");
                    }
                    break;
                case 7:
                    for (int j = 0; j < max - min; j++)
                    {
                        blockcolor[space_info[temp2].x - (f1.mass_info[n, j].y - f1.mass_info[n, temp].y), space_info[temp2].y - (f1.mass_info[n, j].x - f1.mass_info[n, temp].x)] = Brushes.LimeGreen;
                    }
                    break;
            }
            put_partsnum.Push(version + 1);
            put_againtarget2.Push(temp);
            put_target.Push(temp2);
            //output("put_target2.Count" + put_target2.Count.ToString());
            stackmax.Push(max);
            stackmin.Push(min);
            stack_N.Push(n);
        }
        
        public void check(int target, int target2, int stacktarget2)
        {
            int version;
            bool again_flag = false;
            bool target_change = false;
            bool push_flag = true;
            bool first_flag = true;
            int temp = 0;
            int temp2 = 0;
            int check_version;
            int nextmax = max;
            int nextmin = min;
            int next_n = n;
            if (put_target2.Count == 0)
            {
                //output("A");
                //output(put_againtarget2.Count.ToString() + " " + put_target2.Count.ToString());
                version = 0;
            }
            else if ((temp = put_target2.Pop()) == stacktarget2)
            {
                max = stackmax.Pop();
                min = stackmin.Pop();
                n = stack_N.Pop();
                f1.list_n.Remove(f1.list_n.Count - 1);
                //output("B");
                //output(put_againtarget2.Count.ToString() + " " + put_target2.Count.ToString());
                version = put_partsnum.Pop() - 1;
                again_flag = true;
                temp = target2;
                temp2 = target; 
            }
            else
            {
                if (!vir_map[space_info[target].x, space_info[target].y])
                {
                    put_target2.Push(temp);
                    //output("C");
                    //output(put_againtarget2.Count.ToString() + " " + put_target2.Count.ToString());
                    version = 0;
                }
                else
                {
                    max = stackmax.Pop();
                    min = stackmin.Pop();
                    n = stack_N.Pop();
                    f1.list_n.Remove(f1.list_n.Count-1);
                    //output("D");
                    //output(put_againtarget2.Count.ToString() + " " + put_target2.Count.ToString());
                    version = put_partsnum.Pop() - 1;
                    again_flag = true;
                    target_change = true;
                    min = 0;
                    max = f1.total[0];
                    n = 0;
                    for (int i = 1; max < temp + 1; i++)
                    {
                        min = max;
                        max += f1.total[i];
                        n = i;
                    }
                    temp = put_againtarget2.Pop();
                    //output(n.ToString());
                }
            }
            check_version = version;
            put_againtarget2.Push(target2);
            //output(put_againtarget2.Count.ToString());
            put_target.Push(target);
            put_target2.Push(stacktarget2);
            stackmax.Push(nextmax);
            stackmin.Push(nextmin);
            stack_N.Push(n);
            f1.list_n.Add(n);
            bool success = true;
            for(;;)
            {
                //output(version.ToString());
                //output(target2.ToString());
                switch (version)
                {
                    case 0:
                        success = true;
                        if (again_flag)
                        {
                            for (int j = 0; j < max - min; j++)
                            {
                                vir_map[space_info[temp2].x + (f1.mass_info[n, j].x - f1.mass_info[n, temp].x), space_info[temp2].y  + (f1.mass_info[n, j].y - f1.mass_info[n, temp].y)] = false;
                                blockcolor[space_info[temp2].x + (f1.mass_info[n, j].x - f1.mass_info[n, temp].x), space_info[temp2].y  + (f1.mass_info[n, j].y - f1.mass_info[n, temp].y)] = Brushes.White;
                                //output((f1.space_info[target].x).ToString()+" "+ (f1.mass_info[n, j].x).ToString()+" "+(f1.mass_info[n, temp].x).ToString()+" "+( f1.space_info[target].y).ToString() +" "+ ( f1.mass_info[n, j].y).ToString()+" "+ (f1.mass_info[n, temp].y).ToString()+Environment.NewLine+vir_map[f1.space_info[target].x + 7 + f1.mass_info[n, j].x - f1.mass_info[n, temp].x, f1.space_info[target].y + 7 + f1.mass_info[n, j].y - f1.mass_info[n, temp].y].ToString() );
                            }
                            success = false;
                            again_flag = false;
                        }
                        else
                        {
                            for (int j = 0; j < nextmax - nextmin; j++)
                            {
                                if (vir_map[space_info[target].x + (f1.mass_info[next_n, j].x - f1.mass_info[next_n, target2].x) , space_info[target].y + (f1.mass_info[next_n, j].y - f1.mass_info[next_n, target2].y)])
                                {
                                    success = false;
                                }
                            }
                            if (success)
                            {
                                for (int j = 0; j < nextmax - nextmin; j++)
                                {
                                    vir_map[space_info[target].x + (f1.mass_info[next_n, j].x - f1.mass_info[next_n, target2].x), space_info[target].y + (f1.mass_info[next_n, j].y - f1.mass_info[next_n, target2].y)] = true;
                                    blockcolor[space_info[target].x + (f1.mass_info[next_n, j].x - f1.mass_info[next_n, target2].x), space_info[target].y + (f1.mass_info[next_n, j].y - f1.mass_info[next_n, target2].y)] = Brushes.LimeGreen;
                                    //output((f1.space_info[target].x + 7 + f1.mass_info[next_n, j].x - f1.mass_info[next_n, target2].x).ToString() + " " + (f1.space_info[target].y + 7 + f1.mass_info[next_n, j].y - f1.mass_info[next_n, target2].y).ToString());
                                    //output((f1.space_info[target].x).ToString() + " " + (f1.mass_info[next_n, j].x).ToString() + " " + (f1.mass_info[next_n, target2].x).ToString() + " " + (f1.space_info[target].y).ToString() + " " + (f1.mass_info[next_n, j].y).ToString() + " " + (f1.mass_info[next_n, target2].y).ToString() + Environment.NewLine + vir_map[f1.space_info[target].x + 7 + f1.mass_info[next_n, j].x - f1.mass_info[next_n, temp].x, f1.space_info[target].y + 7 + f1.mass_info[next_n, j].y - f1.mass_info[next_n, temp].y].ToString());
                                    //output(target.ToString() + ":target " + target2.ToString() + ":target " + next_n.ToString() + ":next_n " + j + ":j ");
                                }
                                for (int i = 0; i < f1.space_sum; i++)
                                {
                                    if (f1.space_info[i].n > n)
                                        break;
                                    //MessageBox.Show(f1.space_info[i].n.ToString());
                                    if (f1.space_info[i].n == n)
                                    {
                                        space_info[i].x = space_info[target].x + ((f1.space_info[i].x - 1) - f1.mass_info[n, target2].x);
                                        //MessageBox.Show((space_info[target].x).ToString() + " " + (f1.space_info[i].x - 1).ToString() + "  -" + (f1.mass_info[n, target2].x).ToString());
                                        space_info[i].y = space_info[target].y + ((f1.space_info[i].y - 1) - f1.mass_info[n, target2].y);
                                        //MessageBox.Show((space_info[target].y).ToString() + " " + (f1.space_info[i].y - 1).ToString() + "  -" + (f1.mass_info[n, target2].y).ToString());
                                        //MessageBox.Show((space_info[i].x).ToString() + " " + (space_info[i].y).ToString());
                                    }
                                }
                            }
                        }
                        version++;
                        break;
                    case 1:
                        success = true;
                        if (again_flag)
                        {
                            for (int j = 0; j < max - min; j++)
                            {
                                vir_map[space_info[temp2].x  + (f1.mass_info[n, j].y - f1.mass_info[n, temp].y), space_info[temp2].y  - (f1.mass_info[n, j].x - f1.mass_info[n, temp].x)] = false;
                                blockcolor[space_info[temp2].x  + (f1.mass_info[n, j].y - f1.mass_info[n, temp].y), space_info[temp2].y - (f1.mass_info[n, j].x - f1.mass_info[n, temp].x)] = Brushes.White;
                                //output((f1.space_info[temp2].x).ToString() + " " + (f1.mass_info[n, j].y).ToString() + " " + (f1.mass_info[n, temp].y).ToString() + " " + (f1.space_info[temp2].y).ToString() + " " + (f1.mass_info[n, j].x).ToString() + (f1.mass_info[n, temp].x).ToString());
                            }
                            success = false;
                            again_flag = false;
                        }
                        else
                        {
                            for (int j = 0; j < nextmax - nextmin; j++)
                            {
                                if (vir_map[space_info[target].x  + (f1.mass_info[next_n, j].y - f1.mass_info[next_n, target2].y), space_info[target].y - (f1.mass_info[next_n, j].x - f1.mass_info[next_n, target2].x)])
                                {
                                    success = false;
                                }
                                //output((f1.space_info[target].x).ToString() + " " + (f1.mass_info[next_n, j].y).ToString() + " " + (f1.mass_info[next_n, temp].y).ToString() + " " + (f1.space_info[target].y).ToString() + " " + (f1.mass_info[next_n, j].x).ToString() + (f1.mass_info[next_n, temp].x).ToString());
                            }
                            if (success)
                            {
                                for (int j = 0; j < nextmax - nextmin; j++)
                                {
                                    vir_map[space_info[target].x  + (f1.mass_info[next_n, j].y - f1.mass_info[next_n, target2].y), space_info[target].y  - (f1.mass_info[next_n, j].x - f1.mass_info[next_n, target2].x)] = true;
                                    blockcolor[space_info[target].x + (f1.mass_info[next_n, j].y - f1.mass_info[next_n, target2].y), space_info[target].y  - (f1.mass_info[next_n, j].x - f1.mass_info[next_n, target2].x)] = Brushes.LimeGreen;

                                }
                                for (int i = 0; i < f1.space_sum; i++)
                                {
                                    if (f1.space_info[i].n > n)
                                        break;
                                    //MessageBox.Show(f1.space_info[i].n.ToString());
                                    if (f1.space_info[i].n == n)
                                    {
                                        space_info[i].x = space_info[target].x + ((f1.space_info[i].y - 1) - f1.mass_info[n, target2].y);
                                        //MessageBox.Show((space_info[target].x).ToString() + " " + (f1.space_info[i].x - 1).ToString() + "  -" + (f1.mass_info[n, target2].x).ToString());
                                        space_info[i].y = space_info[target].y - ((f1.space_info[i].x - 1) - f1.mass_info[n, target2].x);
                                        //MessageBox.Show((space_info[target].y).ToString() + " " + (f1.space_info[i].y - 1).ToString() + "  -" + (f1.mass_info[n, target2].y).ToString());
                                        //MessageBox.Show((space_info[i].x).ToString() + " " + (space_info[i].y).ToString());
                                    }
                                }
                            }
                        }
                        version++;
                        break;
                    case 2:
                        success = true;
                        if (again_flag)
                        {
                            for (int j = 0; j < max - min; j++)
                            {
                                vir_map[space_info[temp2].x  - (f1.mass_info[n, j].x - f1.mass_info[n, temp].x), space_info[temp2].y  - (f1.mass_info[n, j].y - f1.mass_info[n, temp].y)] = false;
                                blockcolor[space_info[temp2].x  - (f1.mass_info[n, j].x - f1.mass_info[n, temp].x), space_info[temp2].y  - (f1.mass_info[n, j].y - f1.mass_info[n, temp].y)] = Brushes.White;
                            }
                            success = false;
                            again_flag = false;
                        }
                        else
                        {
                            for (int j = 0; j < nextmax - nextmin; j++)
                            {
                                if (vir_map[space_info[target].x - (f1.mass_info[next_n, j].x - f1.mass_info[next_n, target2].x), space_info[target].y - (f1.mass_info[next_n, j].y - f1.mass_info[next_n, target2].y)])
                                {
                                    success = false;
                                }
                            }
                            if (success)
                            {
                                for (int j = 0; j < nextmax - nextmin; j++)
                                {
                                    vir_map[space_info[target].x  - (f1.mass_info[next_n, j].x - f1.mass_info[next_n, target2].x), space_info[target].y  - (f1.mass_info[next_n, j].y - f1.mass_info[next_n, target2].y)] = true;
                                    blockcolor[space_info[target].x  - (f1.mass_info[next_n, j].x - f1.mass_info[next_n, target2].x), space_info[target].y  - (f1.mass_info[next_n, j].y - f1.mass_info[next_n, target2].y)] = Brushes.LimeGreen;

                                }
                            }
                            for (int i = 0; i < f1.space_sum ; i++)
                            {
                                if (f1.space_info[i].n > n)
                                    break;
                                //MessageBox.Show(f1.space_info[i].n.ToString());
                                if (f1.space_info[i].n == n)
                                {
                                    space_info[i].x = space_info[target].x - ((f1.space_info[i].x - 1) - f1.mass_info[n, target2].x);
                                    //MessageBox.Show((space_info[target].x).ToString() + " " + (f1.space_info[i].x - 1).ToString() + "  -" + (f1.mass_info[n, target2].x).ToString());
                                    space_info[i].y = space_info[target].y - ((f1.space_info[i].y - 1) - f1.mass_info[n, target2].y);
                                    //MessageBox.Show((space_info[target].y).ToString() + " " + (f1.space_info[i].y - 1).ToString() + "  -" + (f1.mass_info[n, target2].y).ToString());
                                    //MessageBox.Show((space_info[i].x).ToString() + " " + (space_info[i].y).ToString());
                                }
                            }
                        }
                        version++;
                        break;
                    case 3:
                        success = true;
                        if (again_flag)
                        {
                                for (int j = 0; j < max - min; j++)
                                {
                                    vir_map[space_info[temp2].x  - (f1.mass_info[n, j].y - f1.mass_info[n, temp].y), space_info[temp2].y  + (f1.mass_info[n, j].x - f1.mass_info[n, temp].x)] = false;
                                    blockcolor[space_info[temp2].x  - (f1.mass_info[n, j].y - f1.mass_info[n, temp].y), space_info[temp2].y  + (f1.mass_info[n, j].x - f1.mass_info[n, temp].x)] = Brushes.White;
                                }
                                success = false;
                                again_flag = false;
                        }
                        else
                        {
                            for (int j = 0; j < nextmax - nextmin; j++)
                            {
                                if (vir_map[space_info[target].x - (f1.mass_info[next_n, j].y - f1.mass_info[next_n, target2].y), space_info[target].y  + (f1.mass_info[next_n, j].x - f1.mass_info[next_n, target2].x)])
                                {
                                    success = false;
                                }
                            }
                            if (success)
                            {
                                for (int j = 0; j < nextmax - nextmin; j++)
                                {
                                    vir_map[space_info[target].x - (f1.mass_info[next_n, j].y - f1.mass_info[next_n, target2].y), space_info[target].y  + (f1.mass_info[next_n, j].x - f1.mass_info[next_n, target2].x)] = true;
                                    blockcolor[space_info[target].x - (f1.mass_info[next_n, j].y - f1.mass_info[next_n, target2].y), space_info[target].y  + (f1.mass_info[next_n, j].x - f1.mass_info[next_n, target2].x)] = Brushes.LimeGreen;

                                }
                                for (int i = 0; i < f1.space_sum; i++)
                                {
                                    if (f1.space_info[i].n > n)
                                        break;
                                    //MessageBox.Show(f1.space_info[i].n.ToString());
                                    if (f1.space_info[i].n == n)
                                    {
                                        space_info[i].x = space_info[target].x - ((f1.space_info[i].y - 1) - f1.mass_info[n, target2].y);
                                        //MessageBox.Show((space_info[target].x).ToString() + " " + (f1.space_info[i].x - 1).ToString() + "  -" + (f1.mass_info[n, target2].x).ToString());
                                        space_info[i].y = space_info[target].y + ((f1.space_info[i].x - 1) - f1.mass_info[n, target2].x);
                                        //MessageBox.Show((space_info[target].y).ToString() + " " + (f1.space_info[i].y - 1).ToString() + "  -" + (f1.mass_info[n, target2].y).ToString());
                                        //MessageBox.Show((space_info[i].x).ToString() + " " + (space_info[i].y).ToString());
                                    }
                                }
                            }
                        }
                        version++;
                        break;
                    case 4:
                        success = true;
                        if (again_flag)
                        {
                                for (int j = 0; j < max - min; j++)
                                {
                                    vir_map[space_info[temp2].x  - (f1.mass_info[n, j].x - f1.mass_info[n, temp].x), space_info[temp2].y  + (f1.mass_info[n, j].y - f1.mass_info[n, temp].y)] = false;
                                    blockcolor[space_info[temp2].x  - (f1.mass_info[n, j].x - f1.mass_info[n, temp].x), space_info[temp2].y  + (f1.mass_info[n, j].y - f1.mass_info[n, temp].y)] = Brushes.White;
                                }
                                success = false;
                                again_flag = false;
                        }
                        else
                        {
                            for (int j = 0; j < nextmax - nextmin; j++)
                            {
                                if (vir_map[space_info[target].x  - (f1.mass_info[next_n, j].x - f1.mass_info[next_n, target2].x), space_info[target].y  + (f1.mass_info[next_n, j].y - f1.mass_info[next_n, target2].y)])
                                {
                                    success = false;
                                }
                                //MessageBox.Show((vir_map[space_info[target].x - (f1.mass_info[next_n, j].x - f1.mass_info[next_n, target2].x), space_info[target].y + f1.mass_info[next_n, j].y - f1.mass_info[next_n, target2].y]).ToString());
                            }
                            if (success)
                            {
                                for (int j = 0; j < nextmax - nextmin; j++)
                                {
                                    vir_map[space_info[target].x  - (f1.mass_info[next_n, j].x - f1.mass_info[next_n, target2].x), space_info[target].y  + (f1.mass_info[next_n, j].y - f1.mass_info[next_n, target2].y)] = true;
                                    blockcolor[space_info[target].x  - (f1.mass_info[next_n, j].x - f1.mass_info[next_n, target2].x), space_info[target].y  + (f1.mass_info[next_n, j].y - f1.mass_info[next_n, target2].y)] = Brushes.LimeGreen;

                                }
                                for (int i = 0; i < f1.space_sum; i++)
                                {
                                    if (f1.space_info[i].n > n)
                                        break;
                                    //MessageBox.Show(f1.space_info[i].n.ToString());
                                    if (f1.space_info[i].n == n)
                                    {
                                        space_info[i].x = space_info[target].x - ((f1.space_info[i].x - 1) - f1.mass_info[n, target2].x);
                                        //MessageBox.Show((space_info[target].x).ToString() + " " + (f1.space_info[i].x - 1).ToString() + "  -" + (f1.mass_info[n, target2].x).ToString());
                                        space_info[i].y = space_info[target].y + ((f1.space_info[i].y - 1) - f1.mass_info[n, target2].y);
                                        //MessageBox.Show((space_info[target].y).ToString() + " " + (f1.space_info[i].y - 1).ToString() + "  -" + (f1.mass_info[n, target2].y).ToString());
                                        //MessageBox.Show((space_info[i].x).ToString() + " " + (space_info[i].y).ToString());
                                    }
                                }
                            }
                        }
                        version++;
                        break;
                    case 5:
                        success = true;
                        if (again_flag)
                        {
                                for (int j = 0; j < max - min; j++)
                                {
                                    vir_map[space_info[temp2].x + (f1.mass_info[n, j].y - f1.mass_info[n, temp].y), space_info[temp2].y  + (f1.mass_info[n, j].x - f1.mass_info[n, temp].x)] = false;
                                    blockcolor[space_info[temp2].x + (f1.mass_info[n, j].y - f1.mass_info[n, temp].y), space_info[temp2].y  + (f1.mass_info[n, j].x - f1.mass_info[n, temp].x)] = Brushes.White;
                                }
                                success = false;
                                again_flag = false;
                        }
                        else
                        {
                            for (int j = 0; j < nextmax - nextmin; j++)
                            {
                                if (vir_map[space_info[target].x  + (f1.mass_info[next_n, j].y - f1.mass_info[next_n, target2].y), space_info[target].y + (f1.mass_info[next_n, j].x - f1.mass_info[next_n, target2].x)])
                                {
                                    success = false;
                                }
                            }
                            if (success)
                            {
                                for (int j = 0; j < nextmax - nextmin; j++)
                                {
                                    vir_map[space_info[target].x  + (f1.mass_info[next_n, j].y - f1.mass_info[next_n, target2].y), space_info[target].y  + (f1.mass_info[next_n, j].x - f1.mass_info[next_n, target2].x)] = true;
                                    blockcolor[space_info[target].x  + (f1.mass_info[next_n, j].y - f1.mass_info[next_n, target2].y), space_info[target].y + (f1.mass_info[next_n, j].x - f1.mass_info[next_n, target2].x)] = Brushes.LimeGreen;
                                }
                                for (int i = 0; i < f1.space_sum; i++)
                                {
                                    if (f1.space_info[i].n > n)
                                        break;
                                    //MessageBox.Show(f1.space_info[i].n.ToString());
                                    if (f1.space_info[i].n == n)
                                    {
                                        space_info[i].x = space_info[target].x + ((f1.space_info[i].y - 1) - f1.mass_info[n, target2].y);
                                        //MessageBox.Show((space_info[target].x).ToString() + " " + (f1.space_info[i].x - 1).ToString() + "  -" + (f1.mass_info[n, target2].x).ToString());
                                        space_info[i].y = space_info[target].y + ((f1.space_info[i].x - 1) - f1.mass_info[n, target2].x);
                                        //MessageBox.Show((space_info[target].y).ToString() + " " + (f1.space_info[i].y - 1).ToString() + "  -" + (f1.mass_info[n, target2].y).ToString());
                                        //MessageBox.Show((space_info[i].x).ToString() + " " + (space_info[i].y).ToString());
                                    }
                                }
                            }
                        }
                        version++;
                        break;
                    case 6:
                        success = true;
                        if (again_flag)
                        {
                                for (int j = 0; j < max - min; j++)
                                {
                                    vir_map[space_info[temp2].x  + (f1.mass_info[n, j].x - f1.mass_info[n, temp].x), space_info[temp2].y - (f1.mass_info[n, j].y - f1.mass_info[n, temp].y)] = false;
                                    blockcolor[space_info[temp2].x  + (f1.mass_info[n, j].x - f1.mass_info[n, temp].x), space_info[temp2].y  - (f1.mass_info[n, j].y - f1.mass_info[n, temp].y)] = Brushes.White;
                                }
                                success = false;
                                again_flag = false;
                        }
                        else
                        {
                            for (int j = 0; j < nextmax - nextmin; j++)
                            {
                                if (vir_map[space_info[target].x + (f1.mass_info[next_n, j].x - f1.mass_info[next_n, target2].x), space_info[target].y  - (f1.mass_info[next_n, j].y - f1.mass_info[next_n, target2].y)])
                                {
                                    success = false;
                                }
                                //MessageBox.Show((vir_map[space_info[target].x + (f1.mass_info[next_n, j].x - f1.mass_info[next_n, target2].x), space_info[target].y  - (f1.mass_info[next_n, j].y - f1.mass_info[next_n, target2].y)]).ToString());
                            }
                            if (success)
                            {
                                for (int j = 0; j < nextmax - nextmin; j++)
                                {
                                    vir_map[space_info[target].x + (f1.mass_info[next_n, j].x - f1.mass_info[next_n, target2].x), space_info[target].y - (f1.mass_info[next_n, j].y - f1.mass_info[next_n, target2].y)] = true;
                                    blockcolor[space_info[target].x + (f1.mass_info[next_n, j].x - f1.mass_info[next_n, target2].x), space_info[target].y - (f1.mass_info[next_n, j].y - f1.mass_info[next_n, target2].y)] = Brushes.LimeGreen;
                                    //output((f1.space_info[target].x + 7 + (f1.mass_info[next_n, j].x - f1.mass_info[next_n, target2].x)).ToString()+" "+( f1.space_info[target].y + 7 - (f1.mass_info[next_n, j].y - f1.mass_info[next_n, target2].y)).ToString());
                                    //output((f1.space_info[target].x).ToString() + " " + (f1.mass_info[next_n, j].x).ToString() + " " + (f1.mass_info[next_n, target2].x).ToString() + " " + (f1.space_info[target].y).ToString() + " " + (f1.mass_info[next_n, j].y).ToString() + " " + (f1.mass_info[next_n, target2].y).ToString());
                                    //output(target.ToString() + ":target " + target2.ToString() + ":target " + next_n.ToString() + ":next_n " + j + ":j ");
                                }
                                for (int i = 0; i < f1.space_sum; i++)
                                {
                                    if (f1.space_info[i].n > n)
                                        break;
                                    //MessageBox.Show(f1.space_info[i].n.ToString());
                                    if (f1.space_info[i].n == n)
                                    {
                                        space_info[i].x = space_info[target].x + ((f1.space_info[i].x - 1) - f1.mass_info[n, target2].x);
                                        //MessageBox.Show((space_info[target].x).ToString() + " " + (f1.space_info[i].x - 1).ToString() + "  -" + (f1.mass_info[n, target2].x).ToString());
                                        space_info[i].y = space_info[target].y - ((f1.space_info[i].y - 1) - f1.mass_info[n, target2].y);
                                        //MessageBox.Show((space_info[target].y).ToString() + " " + (f1.space_info[i].y - 1).ToString() + "  -" + (f1.mass_info[n, target2].y).ToString());
                                        //MessageBox.Show((space_info[i].x).ToString() + " " + (space_info[i].y).ToString());
                                    }
                                }
                            }
                        }
                        version++;
                        break;
                    case 7:
                        success = true;
                        if (again_flag)
                        {
                                for (int j = 0; j < max - min; j++)
                                {
                                    vir_map[space_info[temp2].x  - (f1.mass_info[n, j].y - f1.mass_info[n, temp].y),space_info[temp2].y  - (f1.mass_info[n, j].x - f1.mass_info[n, temp].x)] = false;
                                    blockcolor[space_info[temp2].x  - (f1.mass_info[n, j].y - f1.mass_info[n, temp].y), space_info[temp2].y - (f1.mass_info[n, j].x - f1.mass_info[n, temp].x)] = Brushes.White;
                                }
                                success = false;
                                again_flag = false;
                                version = 0;
                        }
                        else
                        {
                            for (int j = 0; j < nextmax - nextmin; j++)
                            {
                                if (vir_map[space_info[target].x  - (f1.mass_info[next_n, j].y - f1.mass_info[next_n, target2].y), space_info[target].y  - (f1.mass_info[next_n, j].x - f1.mass_info[next_n, target2].x)])
                                {
                                    success = false;
                                }
                            }
                            if (success)
                            {
                                for (int j = 0; j < nextmax - nextmin; j++)
                                {
                                    vir_map[space_info[target].x  - (f1.mass_info[next_n, j].y - f1.mass_info[next_n, target2].y), space_info[target].y  - (f1.mass_info[next_n, j].x - f1.mass_info[next_n, target2].x)] = true;
                                    blockcolor[space_info[target].x - (f1.mass_info[next_n, j].y - f1.mass_info[next_n, target2].y), space_info[target].y  - (f1.mass_info[next_n, j].x - f1.mass_info[next_n, target2].x)] = Brushes.LimeGreen;
                                }
                                for (int i = 0; i < f1.space_sum; i++)
                                {
                                    if (f1.space_info[i].n > n)
                                        break;
                                    //MessageBox.Show(f1.space_info[i].n.ToString());
                                    if (f1.space_info[i].n == n)
                                    {
                                        space_info[i].x = space_info[target].x - ((f1.space_info[i].y - 1) - f1.mass_info[n, target2].y);
                                        //MessageBox.Show((space_info[target].x).ToString() + " " + (f1.space_info[i].x - 1).ToString() + "  -" + (f1.mass_info[n, target2].x).ToString());
                                        space_info[i].y = space_info[target].y - ((f1.space_info[i].x - 1) - f1.mass_info[n, target2].x);
                                        //MessageBox.Show((space_info[target].y).ToString() + " " + (f1.space_info[i].y - 1).ToString() + "  -" + (f1.mass_info[n, target2].y).ToString());
                                        //MessageBox.Show((space_info[i].x).ToString() + " " + (space_info[i].y).ToString());
                                    }
                                }
                            }
                            version++;
                        }
                        break;
                    default:
                        version = 0;
                        break;
                }
                if (target_change)
                {
                    Draw2();
                    //output("Ok");
                    version = 0;
                    target_change = false;
                    min = 0;
                    max = f1.total[0];
                    n = 0;
                    for (int i = 1; max < stacktarget2 + 1; i++)
                    {
                        min = max;
                        max += f1.total[i];
                        n = i;
                    }
                }
                if (success)
                    break;
                if (check_version == version)
                {
                    if (first_flag)
                    {
                        first_flag = false;
                    }
                    else
                    {
                        if (again_flag)
                        {
                            again_flag = false;
                        }
                        else
                        {
                            push_flag = false;
                            break;
                        }
                    }
                }
            }
            if(push_flag)
            put_partsnum.Push(version);
            //output(n.ToString());
            //output("put_target2.Count" + put_target2.Count.ToString());
        }

        public void output(string text)
        {
            Form frmDummy = new Form(); 
            frmDummy.Opacity = 0;  
            frmDummy.ControlBox = false; 
            frmDummy.StartPosition = FormStartPosition.CenterScreen;
            frmDummy.Show();
            frmDummy.TopMost = true;
            MessageBox.Show(text);
            frmDummy.Dispose();
            frmDummy = null;
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (list1_clear_flag)
            {
                f1.list1.ClearSelected();
                f1.list2.ClearSelected();
            }
            f1.paint2();
            list1_clear_flag = true;
            f1.form2_flag = false;
            f1.list1.ClearSelected();
            f1.list1.Items.Clear();
            f1.list2.Items.Clear();
            f1.list2.Visible = false;
            f1.label1.Visible = false;
            f1.list_n.Clear();
            f1.firstflag = true;
            for (int i = 1; i <= f1.sum; i++)
                f1.list1.Items.Add(i.ToString());
        }

    }
}
