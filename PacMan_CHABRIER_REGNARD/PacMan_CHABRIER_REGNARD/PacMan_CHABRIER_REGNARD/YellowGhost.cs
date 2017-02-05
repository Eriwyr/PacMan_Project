using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PacMan_CHABRIER_REGNARD
{
    class YellowGhost : Ghost
    {

        //This is the third ghost of the game
        public YellowGhost() : base()
        {
            turnToGoOut = 500;
        }
        protected override void computeTargetTile(PacMan pac, Ghost ghost)
        {
            switch (this.mode)
            {
                //We target different points according to the mode
                case Mode.Scatter:
                    target = new Position(32, 0); //We target a fixed point
                    break;
                case Mode.StayIn:
                    target = new Position(14, 14); //We target the center of the square to stay in iy
                    break;
                case Mode.GoOut:
                    target = new Position(0, 14); //We target outside of the square
                    break;
                case Mode.Normal:

                    //We target either the scatter point or the pacman
                    int distance = manhattanDistance(pac.getPosition(), this.getPosition());
                    if (distance < 8) //If we are at less than 8 tiles away from the pacman
                    {
                        //We target the scatter position
                        target = new Position(32, 0);
                    }
                    else
                    {   
                        //We have the same target has the red ghost
                        target = new Position(pac.getPosition().getPosX(), pac.getPosition().getPosY());
                    }
                    break;
            }

            
        }
    }
}
