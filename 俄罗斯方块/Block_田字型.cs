using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 俄罗斯方块
{
    class Block_田字型:Block
    {
        public int[,] Shape1 { get; set; }

     public Block_田字型()
        {
            Shape1 = new int[4, 4];
            Shape1[2, 2] = Shape1[2, 1] = Shape1[1, 2] = Shape1[1, 1] = 1;
        }

     public override void Change()
     {
         this.currentMode++;
         if (this.currentMode == 1)
             this.currentMode = 0;
     }
     public override int[,] GetCurrentMode()
     {
         if (this.currentMode == 0) return Shape1;
         else return Shape1;
     }
    }
}
