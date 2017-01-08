using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PacMan_CHABRIER_REGNARD
{
    class Game
    {
        private Map map;
        private PacMan pacMan;
        private Ghost ghost;


        public Game()
        {
            map = new Map();
            pacMan = new PacMan();
            ghost = new Ghost();
        }

        public void update(State state)
        {
            pacMan.movement(state,map);


        }

        public Map getMap()
        {
            return map;
        }

        public PacMan getPacman()
        {
            return pacMan;
        }
    }
}
