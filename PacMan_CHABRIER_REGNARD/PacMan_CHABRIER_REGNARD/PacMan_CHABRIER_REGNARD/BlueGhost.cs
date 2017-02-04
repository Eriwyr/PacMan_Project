using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PacMan_CHABRIER_REGNARD
{
    class BlueGhost : Ghost
    {

        public BlueGhost() : base()
        {
            turnToGoOut = 50;
        }
        protected override void computeTargetTile(PacMan pac, Ghost ghost)
        {
            switch (this.mode)
            {
                case Mode.Scatter:
                    target = new Position(32, 28);
                    break;
                case Mode.StayIn:
                    target = new Position(14, 14);
                    break;
                case Mode.GoOut:
                    target = new Position(0, 14);
                    break;
                case Mode.Normal:
                    Position tmp = twoAhead(pac.getState(), pac.getPosition());
                    int xg = ghost.getPosition().getPosX();
                    int yg = ghost.getPosition().getPosY();
                    int xp = pac.getPosition().getPosX();
                    int yp = pac.getPosition().getPosY();
                    target = new Position(2 * xp - xg, 2 * yp - yg);
                    break;
            }
        }

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
