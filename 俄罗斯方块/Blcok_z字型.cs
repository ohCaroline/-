﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 俄罗斯方块
{
    class Blcok_z字型 : Block
    {
        public int[,] Shape1 { get; set; }
        public int[,] Shape2 { get; set; }

        public Blcok_z字型()
        {
            Shape1 = new int[4, 4];
            Shape2 = new int[4, 4];
            Shape1[2, 1] = Shape1[2, 0] = Shape1[1, 1] = Shape1[1, 0] = 1;
            Shape2[3, 2] = Shape2[2, 1] = Shape2[1, 1] = Shape2[2, 2] = 1;
        }

        public override void Change()
        {
            this.currentMode++;
            if (this.currentMode == 2)
                this.currentMode = 0;
        }
        public override int[,] GetCurrentMode()
        {
            if (this.currentMode == 0) return Shape1;
            else return Shape2;
        }
    }
    }

