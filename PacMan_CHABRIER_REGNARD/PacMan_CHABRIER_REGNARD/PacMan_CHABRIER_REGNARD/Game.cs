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
        private Ghost[] ghosts;
        public static int[,] countersFixed;
        private int[,] countersChanging;
        int score;
        Boolean scatter;


        public Game()
        {
            map = new Map();
            pacMan = new PacMan();
            ghosts = new Ghost[3];
            ghosts[0] = new RedGhost();
            ghosts[1] = new PinkGhost();
            ghosts[2] = new YellowGhost();
            score = 0;
            countersFixed = new int[3, 2];
            countersChanging = new int[3, 2];
            scatter = false;
            for(int i = 0; i < 3; i++)
            {
                countersFixed[i, 0] = 7;
                countersFixed[i, 1] = 20;
            }
            for (int i = 0; i < 3; i++)
            {
                countersChanging[i, 0] = 0;
                countersChanging[i, 1] = 0;
            }
        }

        public bool isTouched()
        {   
            foreach(Ghost ghost in ghosts)
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
           
            }
            return false;
        }
        public void pacmanMovement(State state)
        {
            pacMan.movement(state, map);
        }

        public void computeGhosts()
        {
            foreach(Ghost ghost in ghosts)
            {
                incrementCounters();
                compareCounters();
                if (scatter)
                    ghost.setScatter();
                else
                    ghost.setNormal();
                ghost.computeNextMove(pacMan, map);

            }
                
        }

        public void ghostMovement()
        {
            foreach(Ghost ghost in ghosts)
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

        public Ghost getGhost(int i)
        {
            if (i > 2)
                i = 0;
            return ghosts[i];
        }
        public void increaseScore()
        {
            score++;
        }

        private void incrementCounters()
        {
            if (scatter)
            {
                for(int i = 0; i < 3; i++)
                {
                    countersChanging[i, 0]++;
                }
                    
            } else
            {
                for(int i = 0; i < 3; i++)
                {
                    countersChanging[i, 1]++;
                }
            }
        }

        private void compareCounters()
        { //TODO refactor so that every ghost is compare to it's scatter independently
            for(int i = 0; i < 3; i++)
            {
                if(countersChanging[i, 0] >= countersFixed[i, 0])
                {
                    countersChanging[i, 0] = 0;
                    scatter = false;
                } else if(countersChanging[i, 1] >= countersFixed[i, 1])
                {
                    countersChanging[i, 1] = 0;
                    scatter = true;
                }
            }
        }
    }
}
