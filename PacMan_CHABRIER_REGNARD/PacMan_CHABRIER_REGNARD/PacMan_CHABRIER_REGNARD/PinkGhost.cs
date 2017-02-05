using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PacMan_CHABRIER_REGNARD
{
    class PinkGhost : Ghost
    {

        //This is the second ghost of the game
        public PinkGhost() : base()
        {
            turnToGoOut = 50;
        }
        protected override void computeTargetTile(PacMan pac, Ghost ghost)
        {

            //We target a different point according to the mode
            switch (this.mode)
            {
                case Mode.Scatter:
                    target = new Position(-1, 2); //We target a fixed point in a corner
                    break;
                case Mode.StayIn:
                    target = new Position(14, 14); //We target the center of the square to stay in it
                    break;
                case Mode.GoOut: //We target outside of the square
                    target = new Position(0, 14);
                    break;
                case Mode.Normal:
                    //This ghost targets four tiles ahead of the pacman
                    Position pos = fourAhead(pac.getState(), pac.getPosition());
                    if (pac.getState() == State.Up)
                    {

                        //According to an overflow bug in the original version, 
                        //When the pacman is heading up, the target was also four tiles to the left.
                        //For a purpose of keeping the same artificial intelligence, we decided to keep this
                        //even though it does not causes any bug today.
                        pos.setPosY(pos.getPosY() - 4);
                    }
                    target = pos;
                    break;
            }
           
        }

        //Return the tiles that is four ahead in the given direction
        private Position fourAhead(State d, Position p)
        {
            switch (d)
            {
                case State.Up:
                    return new Position(p.getPosX() - 4, p.getPosY());
                case State.Down:
                    return new Position(p.getPosX() + 4, p.getPosY());
                case State.Left:
                    return new Position(p.getPosX(), p.getPosY() - 4);
                case State.Right:
                    return new Position(p.getPosX(), p.getPosY() + 4);
                default:
                    return p;

            }
        }
    }
}

