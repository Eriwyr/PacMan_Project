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
        private int[,] countersFixed;
        private int[,] countersChanging;
        private int[] turnToGoOut;
        int score;
        private Boolean[] scatter;


        public Game()
        {
            map = new Map();
            pacMan = new PacMan();
            ghosts = new Ghost[4];
            ghosts[0] = new RedGhost();
            ghosts[1] = new PinkGhost();
            ghosts[2] = new YellowGhost();
            ghosts[3] = new BlueGhost();
            score = 0;
            countersFixed = new int[4, 2];
            countersChanging = new int[4, 2];
            scatter = new Boolean[4];
            turnToGoOut = new int[4];
            for(int i = 0; i < 4; i++)
            {
                countersFixed[i, 0] = 0;
                countersFixed[i, 1] = 2000000;
                countersChanging[i, 0] = 0;
                countersChanging[i, 1] = 0;
                scatter[i] = false;
                turnToGoOut[i] = 0;
            }
            
        }

        public void reset()
        {
            for(int i = 0; i < ghosts.Length; i++)
            {
                ghosts[i].getPosition().setPosXY(13, 14);
                ghosts[i].setMode(Mode.StayIn);
                ghosts[i].setTurnToGoOut(ghosts[i].getTurnToGoOut() / 2);
                turnToGoOut[i] = 0;
            }

            pacMan.getPosition().setPosXY(23, 13);
        }

        public bool isTouched()
        {
            for(int i = 0; i < ghosts.Length; i++)
            {

                if (pacMan.getPosition().equals(ghosts[i].getPosition()))
                {
                    if (ghosts[i].getAggressivity() == Aggressivity.aggresive)
                    {
                        pacMan.loseLife();
                        return true;    //pacMan.getPosition().setPosXY(17, 14);
                    }
                    else // if pacman can eat ghost, we teleport the ghost in the spawn
                    {
                        ghosts[i].getPosition().setPosXY(13, 14);
                        ghosts[i].setAgresive();
                        ghosts[i].setTurnToGoOut(150);
                        ghosts[i].setMode(Mode.StayIn);
                        turnToGoOut[i] = 0;
                    }
                }
            }
            return false;
        }
        public void pacmanMovement(State state)
        {

            
            if(pacMan.movement(state, map) == true)
            {
                for(int i = 0; i < 4; i++)
                {
                    ghosts[i].setDefensive();
                }
            } else
            {
                
            }
        }

        public void computeGhosts()
        {

            for(int i = 0; i < ghosts.Length; i++)
            {
                incrementCounters();
                compareCounters();

                if(ghosts[i] is BlueGhost)
                {
                    Console.WriteLine(ghosts[i].getMode());
                }

                if (ghosts[i].getMode() != Mode.StayIn && ghosts[i].getMode() != Mode.GoOut)
                {
                    if (scatter[i])
                        ghosts[i].setScatter();
                    else
                        ghosts[i].setNormal();
                }

                   
                if(ghosts[i].getMode() == Mode.GoOut)
                {
                    
                    if(!isInSquare(ghosts[i].getPosition()))
                    {
                        if (scatter[i])
                            ghosts[i].setScatter();
                        else
                            ghosts[i].setNormal();
                    }
                } else if(ghosts[i].getMode() == Mode.Normal)
                {
                    if (isInSquare(ghosts[i].getPosition()))
                    {
                        ghosts[i].setMode(Mode.GoOut);
                    }
                }

                ghosts[i].computeNextMove(pacMan, ghosts[0], map);
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

        public Boolean isInSquare(Position pos)
        {
            if(pos.getPosX() >= 12 && pos.getPosX() <= 16)
            {
                if(pos.getPosY() >= 10 && pos.getPosY() <= 17)
                {
                    return true;
                }
            }
            return false;
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
            return ghosts[i];
        }
        public void increaseScore()
        {
            score++;
        }

        private void incrementCounters()
        {
            for (int i = 0; i < 4; i++)
            {
                if (ghosts[i].getMode() != Mode.StayIn && ghosts[i].getMode() != Mode.GoOut)
                {
                    if (scatter[i])
                        countersChanging[i, 0]++;
                    else
                        countersChanging[i, 1]++;
                }
                if(ghosts[i].getMode() == Mode.StayIn)
                {
                    turnToGoOut[i]++;
                }
            }
           
                
            
        }

        private void compareCounters()
        { //TODO refactor so that every ghost is compare to it's scatter independently
            for(int i = 0; i < 4; i++)
            {
                if(countersChanging[i, 0] >= countersFixed[i, 0])
                {
                    countersChanging[i, 0] = 0;
                    scatter[i] = false;
                } else if(countersChanging[i, 1] >= countersFixed[i, 1])
                {
                    countersChanging[i, 1] = 0;
                    scatter[i] = true;
                }

                if(turnToGoOut[i] >= ghosts[i].getTurnToGoOut())
                {
                    turnToGoOut[i] = 0;
                    ghosts[i].setMode(Mode.GoOut);
                }
            }
        }
    }
}
