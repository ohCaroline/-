using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace 俄罗斯方块
{
    abstract class Block
    {
        public int currentMode { get; set; }
        public int Row { get; set; }
        public int Col { get; set; }

        public Block()
        {
            this.Col=3;
            this.Row = -1;//从最上面显示
        }
        public void MoveLeft()
        {
            this.Col--;
        }
        public void MoveRight()
        {
            this.Col++;
        }
        public void MoveDown()
        {
            this.Row++;
        }
        public abstract void Change();//旋转变换
        public abstract int[,] GetCurrentMode();//获取形状

      
    }
}
