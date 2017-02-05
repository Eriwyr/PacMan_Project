using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PacMan_CHABRIER_REGNARD
{
    class BlueGhost : Ghost
    {
        //This is the last ghost of the game

        public BlueGhost() : base() //We call base constructor
        {
            turnToGoOut = 100; //He will wait 100 turn before going out
        }

        //Overriding computeTargetTile so that we can have a different target
        protected override void computeTargetTile(PacMan pac, Ghost ghost)
        {
            switch (this.mode)
            {
                case Mode.Scatter: //If we are in Scatter mode, we target a fixed point in the corner of the map
                    target = new Position(32, 28);
                    break;
                case Mode.StayIn: //We stay in the center 
                    target = new Position(14, 14);
                    break;
                case Mode.GoOut: //We target outside the center
                    target = new Position(0, 14);
                    break;
                case Mode.Normal: //We chase the player

                    //This ghost target two tiles ahead of the pacman, and create a vector from this
                    //new position and the position of the red ghost, this vector timed by 2 gives 
                    //The target of the blue ghost.
                    Position tmp = twoAhead(pac.getState(), pac.getPosition());
                    int xg = ghost.getPosition().getPosX();
                    int yg = ghost.getPosition().getPosY();
                    int xp = pac.getPosition().getPosX();
                    int yp = pac.getPosition().getPosY();
                    target = new Position(2 * xp - xg, 2 * yp - yg);
                    break;
            }
        }


        //Method to give the position two tiles ahead depending on the direction
        private Position twoAhead(State d, Position p)
        {
            switch (d)
            {
                case State.Up:
                    return new Position(p.getPosX() - 2, p.getPosY());
                case State.Down:
                    return new Position(p.getPosX() + 2, p.getPosY());
                case State.Left:
                    return new Position(p.getPosX(), p.getPosY() - 2);
                case State.Right:
                    return new Position(p.getPosX(), p.getPosY() + 2);
                default:
                    return p;

            }
        }
    }
}
