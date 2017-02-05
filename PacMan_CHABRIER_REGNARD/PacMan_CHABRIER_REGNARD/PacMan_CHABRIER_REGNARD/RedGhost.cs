using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PacMan_CHABRIER_REGNARD
{
    class RedGhost : Ghost
    {
        //This is the first ghost of the game
        public RedGhost() : base()
        {
            this.mode = Mode.Normal;
            this.position = new Position(11, 14); //It starts outside of the square
            turnToGoOut = 0;
        }

        protected override void computeTargetTile(PacMan pac, Ghost ghost)
        {
            //We target a different point according to the mode
            switch (this.mode)
            {
                //We target a fixed point in Scatter mode
                case Mode.Scatter:
                    this.target = new Position(-1, 26);
                    break;

                //We target the center of the square to stay in it
                case Mode.StayIn:
                    target = new Position(14, 14);
                    break;
                case Mode.GoOut: //We target outside the square
                    target = new Position(0, 13);
                    break;
                case Mode.Normal:
                    //We target the player
                    this.target = new Position(pac.getPosition().getPosX(), pac.getPosition().getPosY());
                    break;
            }
        }
    }
}
