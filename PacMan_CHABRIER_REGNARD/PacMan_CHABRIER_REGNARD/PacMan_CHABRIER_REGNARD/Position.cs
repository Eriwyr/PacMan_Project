using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PacMan_CHABRIER_REGNARD
{
    class Position
    {
        private int x; //The coordonates of the point
        private int y;

        public Position(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
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


        //We create an equals method to compare two positions
        public bool equals(Position pos)
        {
            return (this.x == pos.x && this.y == pos.y);
        }
    }
}
