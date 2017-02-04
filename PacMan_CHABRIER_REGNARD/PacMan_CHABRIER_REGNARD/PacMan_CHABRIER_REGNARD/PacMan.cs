using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PacMan_CHABRIER_REGNARD
{
    class PacMan : Character
    {
        private int life;

        public PacMan() : base()
        {
            life = 3;
        }

        public int getLife()
        {
            return life;
        }

        public void loseLife()
        {
            life--;
        }
    }

    
}
