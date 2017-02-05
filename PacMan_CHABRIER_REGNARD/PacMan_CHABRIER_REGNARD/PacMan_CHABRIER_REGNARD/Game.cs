using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PacMan_CHABRIER_REGNARD
{   enum GamePlay {Normal, Hungry}
    
    //Main class of the game
    class Game
    {
        //We store every element mandatory for the game
        private Map map;
        private PacMan pacMan;
        private Ghost[] ghosts;
        private GamePlay gamePlay;
        private bool hasEaten;

        //Definition of counters to keep track of every modes possible for the ghosts
        private int[,] countersFixed;// This contain the number of time to Scatter mode and Normal mode
        private int[,] countersChanging; //This contain the actual timer of scatter mode and normal mode
        private int[] turnToGoOut; //Actual timer of the time before getting out of the starting square
        private int[] vulnerabitlitiesFixed; //How much time can ghosts be vulnerable
        private int[] vulnerabitlitiesChanging; //How much time the ghosts have been vulnerable so far

        int score;
        private Boolean[] scatter; //Wether or not the ghosts are in scatter mode


        public Game()
        {
            restart();
            
        }

        //Initialize every items
        public void restart()
        {
            map = new Map();
            map.countBeans(); //We count the number of beans on the map only once so we can check more quickly afterward.
            pacMan = new PacMan();
            ghosts = new Ghost[4];
            ghosts[0] = new RedGhost();
            ghosts[1] = new PinkGhost();
            ghosts[2] = new YellowGhost();
            ghosts[3] = new BlueGhost();
            gamePlay = GamePlay.Normal;
            hasEaten = false;
            score = 0;
            countersFixed = new int[4, 2];
            countersChanging = new int[4, 2];
            vulnerabitlitiesFixed = new int[4];
            vulnerabitlitiesChanging = new int[4];
            scatter = new Boolean[4];
            turnToGoOut = new int[4];
            for (int i = 0; i < 4; i++)
            {
                countersFixed[i, 0] = 20;
                countersFixed[i, 1] = 50;
                countersChanging[i, 0] = 0;
                countersChanging[i, 1] = 0;
                vulnerabitlitiesFixed[i] = 175;
                vulnerabitlitiesChanging[i] = 0;
                scatter[i] = false;
                turnToGoOut[i] = 0;
            }
        }

        //When we died this function is called to reset everything
        public void reset()
        {
            for(int i = 0; i < ghosts.Length; i++)
            {
                ghosts[i].getPosition().setPosXY(13, 14);
                ghosts[i].setMode(Mode.StayIn);
                ghosts[i].setTurnToGoOut(ghosts[i].getTurnToGoOut() / 2);
                turnToGoOut[i] = 0;
                vulnerabitlitiesChanging[i] = 0;
                countersChanging[i, 0] = 0;
                countersChanging[i, 1] = 0;
            }

            pacMan.getPosition().setPosXY(23, 13);
        }

        public bool isTouched()
        {
            hasEaten = false;
            for(int i = 0; i < ghosts.Length; i++)
            {
                if(ghosts[i].getAggressivity() == Aggressivity.aggresive)
                {
                    gamePlay = GamePlay.Normal;
                }
                else
                {
                    gamePlay = GamePlay.Hungry;
                }
                
                if (pacMan.getPosition().equals(ghosts[i].getPosition()))
                {
                    if (ghosts[i].getAggressivity() == Aggressivity.aggresive)
                    {
                        //If the pacman is eaten by one of the ghosts
                        pacMan.loseLife();
                        return true;
                    }
                    else 
                    {
                        //If the pacman eats a ghost, we send it to the starting square
                        //And we reset all of its counters;
                        hasEaten = true;
                        ghosts[i].getPosition().setPosXY(13, 14);
                        ghosts[i].setAgresive();
                        ghosts[i].setTurnToGoOut(200);
                        ghosts[i].setMode(Mode.StayIn);
                        turnToGoOut[i] = 0;
                        vulnerabitlitiesChanging[i] = 0;
                        countersChanging[i, 0] = 0;
                        countersChanging[i, 1] = 0;
                    }
                }
            }
            return false;
        }
        public void pacmanMovement(State state)
        {
            //Execute the movement of the pacman
            
            if(pacMan.movement(state, map) == true)
            {
                //If we ate a big bean then the ghosts change their states
                for(int i = 0; i < ghosts.Length; i++)
                {
                    ghosts[i].setDefensive();
                    ghosts[i].setHasChanged(true);
                    vulnerabitlitiesChanging[i] = 0;
                    
                }
            } 
        }

        public void computeGhosts()
        {
            //Prepare ghosts movements

            for(int i = 0; i < ghosts.Length; i++) //Foreach ghpsts
            {
                incrementCounters(); //We increment the counters
                compareCounters(); //We verify each counters

                if (ghosts[i].getMode() != Mode.StayIn && ghosts[i].getMode() != Mode.GoOut)
                {
                    //If we are not in the square

                    //We set Scatter mode accordingly to the booleans
                    if (scatter[i])
                        ghosts[i].setScatter();
                    else
                        ghosts[i].setNormal();
                }

                //If the ghost is trying to go out of the square
                if(ghosts[i].getMode() == Mode.GoOut)
                {
                    //If it's not in the square anymore
                    if(!isInSquare(ghosts[i].getPosition())) 
                    {
                        //We reset to the mode it was before entering the square
                        if (scatter[i])
                            ghosts[i].setScatter();
                        else
                            ghosts[i].setNormal();
                    }
                } else if(ghosts[i].getMode() == Mode.Normal)
                {
                    if (isInSquare(ghosts[i].getPosition()))
                    {
                        //If the ghost accidently enters the square then
                        //we force it to go out
                        ghosts[i].setMode(Mode.GoOut);
                    }
                }
                //We calculate the next move of the ghosts
                //One ghost is send in param because some ghosts use it to
                //computes their target tiles.
                ghosts[i].computeNextMove(pacMan, ghosts[0], map);
            }
            
           
        }

        //Will check if the game has ended
        public bool isFinished()
        {
            if(pacMan.getLife() <= 0) //If we don't have lives anymore
            {
                return true; //We stop the game
            } else if(map.getNbBeans() == 0) { //If every beans on the map was eaten
                pacMan.win(); //The pacman win the game
                return true;//We stop the game
            }
            return false; 
        }

        public void ghostMovement() //Execute the movement of each ghosts
        {
            foreach(Ghost ghost in ghosts)
                ghost.movement(ghost.getNextMove(), map);
        }

        //We check if the movement of the pacman will be possible
        //(will help to manage inputs)
        public bool checkPacman(State state)
        {
            return pacMan.checkMovement(state, map);
        }

        //Check if the position pos is in the starting square of the ghosts
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
            //For each ghosts
            for (int i = 0; i < ghosts.Length; i++)
            {
                if (ghosts[i].getMode() != Mode.StayIn && ghosts[i].getMode() != Mode.GoOut)
                {
                    //If we are not in the square
                    
                    if (scatter[i]) //If we were in scatter mode
                        countersChanging[i, 0]++; //We keep track of how much time the ghost has been in scatter
                    else
                        countersChanging[i, 1]++; //We keep track of how much time the ghost has been in normal
                }
                if(ghosts[i].getMode() == Mode.StayIn) //If the ghost stayed in the square
                {
                    turnToGoOut[i]++;//We increment the time before going out
                }

                if(ghosts[i].getAggressivity() == Aggressivity.defensive)
                {
                    vulnerabitlitiesChanging[i]++; //If the ghost was chased, we increment this counter
                }
            }
           
                
            
        }

        private void compareCounters()
        { 
            //Will make the changement between the differents mode according to timers
            for(int i = 0; i < ghosts.Length; i++)
            {
                if(countersChanging[i, 0] >= countersFixed[i, 0])
                { //If we were in scatter for enough time
                    countersChanging[i, 0] = 0; //We reset the timer
                    scatter[i] = false; //We are not in scatter anymore
                } else if(countersChanging[i, 1] >= countersFixed[i, 1])
                {
                    //If we were in normal mode for enough time
                    countersChanging[i, 1] = 0; //We reset the timer
                    scatter[i] = true; //We go in scatter mode
                }

                if(turnToGoOut[i] >= ghosts[i].getTurnToGoOut())
                {
                    //If the ghost stayed in the square enough
                    turnToGoOut[i] = 0;
                    ghosts[i].setMode(Mode.GoOut); //We make it go out
                }

                if(vulnerabitlitiesChanging[i] >= vulnerabitlitiesFixed[i])
                {
                    //If the ghost was vulnerable for enough time
                    vulnerabitlitiesChanging[i] = 0;
                    ghosts[i].setAgresive(); //We switch to chasing mode
                }
            }
        }

        public GamePlay getGamePlay()
        {
            return gamePlay;
        }
        public bool getHasEaten()
        {
            return hasEaten;
        }
    }
}
