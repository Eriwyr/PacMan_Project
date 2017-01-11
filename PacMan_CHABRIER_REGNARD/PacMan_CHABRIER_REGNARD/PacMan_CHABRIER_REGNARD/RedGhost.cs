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
            this.target = new Position(pac.getPosition().getPosX(), pac.getPosition().getPosY());
        }
    }
}
