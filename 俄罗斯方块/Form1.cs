using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 俄罗斯方块
{
    public partial class Form1 : Form
    {
        Graphics g;//地图
        Graphics g2;//预览框
        int[,] map;
        Block block_Now;//当前方块
        Block block_Next;//下一个方块


        public Form1()
        {
            InitializeComponent();
            g = this.panel1.CreateGraphics();
            g2 = this.panel2.CreateGraphics();
            map = new int[20, 10];//初始化游戏界面
        }

      

        //画地图，对g(游戏界面)的操作
        private void DrawMap()
        {
            //g.Clear(Color.Black);
            for (int i=0;i<20;i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (map[i, j] == 1)
                    {
                        g.FillRectangle(Brushes.PowderBlue, j * 25, i * 25, 24, 24);//当有方块冻住时填充为

                    }
                    else
                    {
                        g.FillRectangle(Brushes.LightPink, j * 25, i * 25, 24, 24);//无方块时填充为
                    }
                }
            }
        }
        //画地图上的当前方块
        private void DrawBlockNow()
        {
            if (block_Now == null)
            {
                return;
            }//先检查对象是否为空，一旦方块为空则就结束当前的方法，不执行后面的代码

            int[,] shape = block_Now.GetCurrentMode();//调用GetCurrentMode方法，初始化方块形状
            
            //填充方块
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (shape[i, j] == 1)
                        g.FillRectangle(Brushes.NavajoWhite, (j +block_Now.Col) * 25, (i + block_Now.Row) * 25, 24, 24);
                }
            }
        }//

        //画预览框上的下一个方块
        public void DrawBlockNext()
        {
            if (block_Next == null) return;//一旦方块为空则就结束当前的方法，不执行后面的代码

            int[,] shape = block_Next.GetCurrentMode();//调用GetCurrentMode方法，初始化方块形状

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (shape[i, j] == 1)
                        g2.FillRectangle(Brushes.NavajoWhite, j * 25, i * 25, 24, 24);
                    else
                        g2.FillRectangle(Brushes.LightPink, j * 25, i * 25, 24, 24);
                }
            }
        }

        //随机产生下一个方块，共7种形状
        public void GrowBlockNext()
        {
            Random r = new Random();
            int num = r.Next(0, 7);
            if (num == 0) block_Next = new Block_山字型();
            else if (num == 1) block_Next = new Block_七字型();
            else if (num == 2) block_Next = new Blcok_z字型();
            else if (num == 5) block_Next = new Block_田字型();
            else if (num == 4) block_Next = new Block_反七字型();
            else if (num == 3) block_Next = new Block_反z字型();
            else block_Next = new Block_一字型();
        }
      
        //产生新的方块
        public void GrowBlockNew()
        {
            if (block_Next == null)
                GrowBlockNext();//对block_Next赋新值
            
            //把下一个方块做为当前方块
            block_Now = block_Next;
            
            //再随机产生下一个方块
            GrowBlockNext();
        }
        //画游戏
        private void DrawGame()
        {
            DrawMap();
            DrawBlockNow();
            DrawBlockNext();
        }

        //开始按纽
        private void btnStart_Click(object sender, EventArgs e)
        {
            //for (int i = 0; i < 20; i++)
            //{
            //    for (int j = 0; j < 10; j++)
            //    {
            //        map[i, j] = 0;
            //    }
            //}
                
            GrowBlockNew();
            DrawGame();
            this.timer1.Start();

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //DrawGame();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            DrawGame();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            DrawGame();
            
        }

        private void panel1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {

        }
        //判断出界、重叠
        private bool CheckCrush()
        {
            int[,] shape = block_Now.GetCurrentMode();
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (shape[i, j] == 1)
                    {//判断是否已出界、触底或重叠
                        //左边界
                        if (j + block_Now.Col < 0) return false;
                        //右边界
                        if (j + block_Now.Col > 9) return false;
                        //下边界
                        if (i + block_Now.Row > 19) return false;
                        //触底或接触到已触底的方块
                        if (map[i + block_Now.Row, j + block_Now.Col] == 1) return false;
                    }
                }
            }
            return true;
        }
        //下移
        private bool BlockMoveDown()
        {
            block_Now.MoveDown();
            if (!CheckCrush())//若检测到触底
            {
                block_Now.Row--;//初始化方块从索引为1开始，需要自减
                FrozenBlock();//固定方块 
                GrowBlockNew();//产生新方块
                Clear();//若有消行则执行消行
                Gameover();//若最顶端有方块则游戏结束
                return false; //表示下移失败，已被冻住，不再下移
            }
            return true; //表示下移成功
            
        }

        //按键控制移动、旋转变形
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (block_Now == null) { return; }
            if (e.KeyCode == Keys.A)//左移
            {
                block_Now.MoveLeft();
                if (!CheckCrush()) { block_Now.MoveRight(); }
            }
            else if (e.KeyCode == Keys.D)//右移
            {
                block_Now.MoveRight();
                if (!CheckCrush()) { block_Now.MoveLeft(); }
            }
            else if (e.KeyCode == Keys.S)//下移
            {
                BlockMoveDown();
                
            }
            else if (e.KeyCode == Keys.W)//旋转变形
            {
                block_Now.Change();
                
                if (!CheckCrush()) { block_Now.currentMode--; }//？
            }
            DrawGame();
        }
        //冻结 触底

        private void FrozenBlock()
        {
            int[,] shape = block_Now.GetCurrentMode();
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (shape[i, j] == 1)
                    {
                        //冻结格子
                        map[i + block_Now.Row, j + block_Now.Col] = 1;
                        
                    }
                }
            }
        }

        //消行
        private void clearRow(int rowIndex)
        {
            //1 消行
            for(int j=0;j<10;j++)
            {
                map[rowIndex, j] = 0;
                //DrawGame();
                DrawMap();
            }
            //2 上面的行往下掉，整行进行消除
            for (int i = rowIndex; i > 0; i--)
            {
                for (int j = 0; j < 10; j++)
                {
                    map[i,j]=map[i-1,j];
                }
            }
        }

        //完成消行
        int countrow;//记录所消行数
        private void Clear()
        {
            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (map[i, j] == 0) { break; }//跳出当前行的检查，进行下一行的检查
                    if(j==9)
                    {
                        clearRow(i);
                        countrow++;
                        textBox1.Text = Convert.ToString(countrow*10);//每消一行加10分
                    }
                }
            }
        }

        private void timer1_Tick_1(object sender, EventArgs e)
        {
            BlockMoveDown();
            DrawGame();
        }

        private void Gameover()
        {
            for (int j = 0; j < 10; j++)
            {
                if (map[0, j] == 1)
                {
                    timer1.Stop();
                    block_Now = null;
                    block_Next = null;
                    
                    MessageBox.Show("GAME OVER!");
                    break;
                }
            }
        }
        //暂停键
        private void btnPause_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            timer1.Enabled = false;
        }
        //继续键
        private void btnContinue_Click(object sender, EventArgs e)
        {

            timer1.Start();
            timer1.Enabled = true;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    
}}
