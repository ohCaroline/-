using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 俄罗斯方块
{
    class Block_反七字型 : Block
    {
        public int[,] Shape1 { get; set; }
        public int[,] Shape2 { get; set; }
        public int[,] Shape3 { get; set; }
        public int[,] Shape4 { get; set; }

        public Block_反七字型()
        {
            Shape1 = new int[4, 4];
            Shape2 = new int[4, 4];
            Shape3 = new int[4, 4];
            Shape4 = new int[4, 4];
            Shape1[3, 2] = Shape1[2, 2] = Shape1[1, 2] = Shape1[3, 1] = 1;
            Shape2[3, 2] = Shape2[3, 1] = Shape2[3, 0] = Shape2[2, 0] = 1;
            Shape3[1, 1] = Shape3[3, 0] = Shape3[2, 0] = Shape3[1, 0] = 1;
            Shape4[2, 2] = Shape4[1, 2] = Shape4[1, 1] = Shape4[1, 0] = 1;
        }

        public override void Change()
        {
            this.currentMode++;
            if (this.currentMode == 4)
                this.currentMode = 0;
        }
        public override int[,] GetCurrentMode()
        {
            if (this.currentMode == 0) return Shape1;
            else if (this.currentMode == 1) return Shape2;
            else if (this.currentMode == 2) return Shape3;
            else return Shape4;
        }
    }
}
