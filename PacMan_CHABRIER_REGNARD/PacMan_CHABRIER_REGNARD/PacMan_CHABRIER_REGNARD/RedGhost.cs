using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PacMan_CHABRIER_REGNARD
{
    class RedGhost : Ghost
    {

        public RedGhost() : base()
        {
            this.mode = Mode.GoOut;
            turnToGoOut = 0;
        }

        protected override void computeTargetTile(PacMan pac, Ghost ghost)
        {
            switch (this.mode)
            {
                case Mode.Scatter:
                    this.target = new Position(-1, 26);
                    break;
                case Mode.StayIn:
                    target = new Position(14, 14);
                    break;
                case Mode.GoOut:
                    target = new Position(12, 0);
                    break;
                case Mode.Normal:
                    this.target = new Position(pac.getPosition().getPosX(), pac.getPosition().getPosY());
                    break;
            }
            
        }
    }
}
