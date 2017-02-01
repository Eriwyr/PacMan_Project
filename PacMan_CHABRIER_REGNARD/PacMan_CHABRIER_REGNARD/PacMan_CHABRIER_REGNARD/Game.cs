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
        int score;


        public Game()
        {
            map = new Map();
            pacMan = new PacMan();
            ghost = new RedGhost();
            score = 0;
        }

        public bool isTouched()
        {   
            if (pacMan.getPosition().equals(ghost.getPosition()))
            {
                if (ghost.getAggressivity() == Aggressivity.aggresive)
                {
                    pacMan.loseLife();
                    return true;    //pacMan.getPosition().setPosXY(17, 14);
                }
                else // if pacman can eat ghost, we teleport the ghost in the spawn
                {
                    ghost.getPosition().setPosXY(15, 16);
                }
            }
            return false;
        }
        public void pacmanMovement(State state)
        {
            pacMan.movement(state, map);
        }

        public void computeGhosts()
        {
            ghost.computeNextMove(pacMan, map);
        }

        public void ghostMovement()
        {
            ghost.movement(ghost.getNextMove(), map);
        }

        public bool checkPacman(State state)
        {
            return pacMan.checkMovement(state, map);
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
        public void increaseScore()
        {
            score++;
        }
    }
}
