using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PacMan_CHABRIER_REGNARD
{
    class PinkGhost : Ghost
    {
        protected override void computeTargetTile(PacMan pac)
        {

            switch (this.mode)
            {
                case Mode.Scatter:
                    target = new Position(-1, 2);
                    break;
                case Mode.Normal:
                    Position pos = fourAhead(pac.getState(), pac.getPosition());
                    if (pac.getState() == State.Up)
                    {
                        pos.setPosY(pos.getPosY() - 4);
                    }
                    target = pos;
                    break;
            }
           
        }

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

