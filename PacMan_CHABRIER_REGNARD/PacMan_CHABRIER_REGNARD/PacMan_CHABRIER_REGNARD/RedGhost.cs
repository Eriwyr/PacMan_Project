using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PacMan_CHABRIER_REGNARD
{
    class RedGhost : Ghost
    {
        protected override void computeTargetTile(PacMan pac)
        {
            switch (this.mode)
            {
                case Mode.Scatter:
                    this.target = new Position(-1, 26);
                    break;
                case Mode.Normal:
                    this.target = new Position(pac.getPosition().getPosX(), pac.getPosition().getPosY());
                    break;
            }
            
        }
    }
}
