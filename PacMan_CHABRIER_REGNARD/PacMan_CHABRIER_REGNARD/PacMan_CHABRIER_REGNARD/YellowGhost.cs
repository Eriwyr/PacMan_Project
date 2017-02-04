using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PacMan_CHABRIER_REGNARD
{
    class YellowGhost : Ghost
    {

        public YellowGhost() : base()
        {
            turnToGoOut = 500;
        }
        protected override void computeTargetTile(PacMan pac, Ghost ghost)
        {
            switch (this.mode)
            {
                case Mode.Scatter:
                    target = new Position(32, 0);
                    break;
                case Mode.StayIn:
                    target = new Position(14, 14);
                    break;
                case Mode.GoOut:
                    target = new Position(0, 14);
                    break;
                case Mode.Normal:
                    int distance = manhattanDistance(pac.getPosition(), this.getPosition());
                    if (distance < 8)
                    {
                        target = new Position(32, 0);
                    }
                    else
                    {
                        target = new Position(pac.getPosition().getPosX(), pac.getPosition().getPosY());
                    }
                    break;
            }

            
        }
    }
}
