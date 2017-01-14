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
            ghost = new RedGhost();
        }

        public void update(State state)
        {
            ghost.computeNextMove(pacMan, map);
            pacMan.movement(state, map);
            
            
            ghost.movement(ghost.getNextMove(), map);


        }

        public Map getMap()
        {
            return map;
        }

        public PacMan getPacman()
        {
            return pacMan;
        }

        public Ghost getGhost()
        {
            return ghost;
        }
    }
}
