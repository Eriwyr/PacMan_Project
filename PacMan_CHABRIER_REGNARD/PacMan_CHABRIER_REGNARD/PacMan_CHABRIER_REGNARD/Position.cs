﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PacMan_CHABRIER_REGNARD
{
    class Position
    {
        private int x;
        private int y;

        public int getPosX()
        {
            return x;
        }

        public int getPosY()
        {
            return y;
        }
        
        
        public void setPosX(int x)
        {
            this.x = x;
        }

        public void setPosY(int y)
        {
            this.y = y;
        }

        public void setPosXY(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }
}