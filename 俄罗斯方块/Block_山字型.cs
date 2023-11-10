using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 俄罗斯方块
{//子类重写
    class Block_山字型:Block
    {
        public int[,] Shape1 { get; set; }
        public int[,] Shape2 { get; set; }
        public int[,] Shape3 { get; set; }
        public int[,] Shape4 { get; set; }
      
        public Block_山字型()
        {
            //四种形态
            Shape1 = new int[4, 4];
            Shape2 = new int[4, 4];
            Shape3 = new int[4, 4];
            Shape4 = new int[4, 4];
            Shape1[2, 1] = Shape1[1, 1] = Shape1[1, 0] = Shape1[0, 1] = 1;
            Shape2[1, 2] = Shape2[1, 1] = Shape2[1, 0] = Shape2[0, 1] = 1;
            Shape3[2, 1] = Shape3[1, 2] = Shape3[1, 1] = Shape3[0, 1] = 1;
            Shape4[2, 1] = Shape4[1, 2] = Shape4[1, 1] = Shape4[1, 0] = 1;

        }

        //按键改变形态
        public override void Change()
        {
            this.currentMode++;
            if (this.currentMode == 4)//mode到4循环重来
                this.currentMode = 0;
        }
        //形态
        public override int[,] GetCurrentMode()
        {
            if (this.currentMode == 0) return Shape1;
            else if (this.currentMode == 1) return Shape2;
            else if (this.currentMode == 2) return Shape3;
            else return Shape4; 
        }
    }
    }

