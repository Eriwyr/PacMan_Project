using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PacMan_CHABRIER_REGNARD
{   
    //The agressivity will tell wheter or not the ghost chase the pacman
    public enum Aggressivity {aggresive, defensive };

    //This will tell which actions to do
    public enum Mode {Normal, Scatter, StayIn, GoOut};
    //Scatter mode is a special mode during which the ghost goes on a fixed point on the map (corner)
    //This mode is here so that the ghosts don't focus the player all the time

    class Ghost : Character
    {

        protected Position target; //Where the ghost will try to go
        protected Aggressivity aggressivity;
        protected Mode mode;
        private State nextMove; //The direction it'll take in the next turn
        private bool[] intersect;
        protected int turnToGoOut; //The maximum time before going out of the square
        private bool hasChanged = false;

        public Ghost()
        {
            position = new Position(13, 14);
            target = new Position(13, 14);
            aggressivity = Aggressivity.aggresive;
            state = State.Nothing;
            nextMove = State.Nothing;
            mode = Mode.StayIn;
            intersect = new bool[4];
            turnToGoOut = 150;
            for (int i = 0; i < 4; i++)
                intersect[i] = true;
        }

        public void computeNextMove(PacMan pac, Ghost ghost, Map map)
        {
            //Each loop turn this method is called
            resetIntersect(); //We say that every intersections is possible
            if (this.aggressivity == Aggressivity.aggresive)
            {
                //If the ghost chase the pacman
                computeTargetTile(pac, ghost); //We calculate the target 
                //Each ghost will have a different target to make the game easier to play


                checkIntersection(map); //We check if there was an intersection and we forbid the ones with walls
                computeState(); //We deduce a direction
            } else
            {

                //If the pacman chase the ghost
                computeAwayTile(pac, map); //We calculate the furthest point from the pacman
                checkIntersection(map); //We as well check intersections
                reverse(); //We reverse the direction if we were going toward the pacman

            }
            
            

        }

        private void reverse()
        {
            if (this.state != State.Nothing)
            { //If we were moving
                if (hasChanged) //This boolean allows us to call this part only once
                {
                    hasChanged = false;
                    int inv = (int)this.state;
                    inv += 2;
                    inv %= 4;
                    intersect[inv] = true;
                    //We reverse the direction of the ghost
                    for(int i = 0; i < 4; i++)
                    {
                        if( i != inv)
                        {
                            //We make every other directions false
                            intersect[i] = false;
                        }
                    }

                    this.state = (State)inv; //We make sure we go back

                } else
                {
                    //Otherwise we compute the new state as before
                    computeState();
                }
                

            }
        }

        public void setHasChanged(bool b)
        {
            hasChanged = b;
        }

        private void computeAwayTile(PacMan pac, Map map)
        {

            double distance = 0;
            double dist;
            Position pacPos = pac.getPosition();
            Position ret = new Position(0, 0);
            //For each tiles of the map, we store the furthest
            for(int i = 0; i < map.getVX();i++)
            {
                for(int j = 0; j < map.getVY(); j++)
                {
                    Position tmp = new Position(i, j);
                    dist = euclidianDistance(pacPos, tmp);
                    if( dist >= distance)
                    {
                        distance = dist;
                        ret.setPosXY(tmp.getPosX(), tmp.getPosY());
                    }
                }
            }

            target = ret; //We target the furthests point
        } 

        private void resetIntersect()
        {
            for (int i = 0; i < 4; i++)
                intersect[i] = true; //By default every movement is possible
        }

        private void computeState()
        {
            this.state = selectIntersect();
        }

        public State getNextMove()
        {
            return nextMove;
        }


        private State selectIntersect()
        {

            //If we are not in the sqaure
            if(this.state != State.Nothing && this.mode != Mode.GoOut)
            {
                    int inv = (int)this.state;
                    inv += 2;
                    inv %= 4;
                    intersect[inv] = false; //We forbid going back
                
            }
            
            Position tmp = new Position(0, 0);
            //State inverse = (State)(inv);
            
            //We take the minimum distance between the target and the possible directions
            double distance = 100000;
            double dist = 0;
            int j = 0;
            bool changed = false;
            for(int i = 0; i < 4; i++)
            {
                if (intersect[i])
                {
                    tmp = nextTile((State)i);
                    dist = euclidianDistance(target, tmp);
                    if ( dist < distance)
                    {
                        distance = dist;
                        j = i;
                        changed = true;
                    }
                        
                }
            }
            //If we had a better choice we return it
            if(changed)
                return (State) j;

                //We keep the same state
            return this.state;
        }


        //Calculate the manhattan distance between two points
        protected int manhattanDistance(Position taget, Position pos)
        {
            return (Math.Abs(pos.getPosX() - target.getPosX()) + Math.Abs(pos.getPosY() - target.getPosY()));
        }


        //Calculate euclidian distance between two points
        protected double euclidianDistance(Position target, Position pos)
        {
            double a = Math.Pow(target.getPosX() - pos.getPosX(), 2);
            double b = Math.Pow(target.getPosY() - pos.getPosY(), 2);
            return Math.Sqrt(a + b);
        }

        //This method will be overwritten by every ghosts because they don't treat target the same way
        protected virtual void  computeTargetTile(PacMan pac, Ghost ghost)
        {
            //To override
        }

        public int getTurnToGoOut()
        {
            return turnToGoOut;
        }

        public void setTurnToGoOut(int a)
        {
            turnToGoOut = a;
        }
        private void checkIntersection(Map map)
        {
            Position up, down, left, right;
            Position pos = this.position;
            up = new Position(pos.getPosX() - 1, pos.getPosY());
            down = new Position(pos.getPosX() + 1, pos.getPosY());
            left = new Position(pos.getPosX(), pos.getPosY()-1);
            right = new Position(pos.getPosX(), pos.getPosY()+1);


            //For every directions possible, if there is a wall on it we forbid this movement
            Element elmt = map.checkElement(up);
            if (elmt == Element.Wall)
            {
                intersect[(int)State.Up] = false;
            }
                
            elmt = map.checkElement(down);
            if (elmt == Element.Wall)
            { 
                intersect[(int)State.Down] = false;
            }
                
            elmt = map.checkElement(left);
            if (elmt == Element.Wall)
            {
                intersect[(int)State.Left] = false;
            }
            elmt = map.checkElement(right);
            if (elmt == Element.Wall)
            {
                intersect[(int)State.Right] = false;
            }
            

        }

        //Return the tile at the next position according to the state
        private Position nextTile(State state)
        {
            switch (state)
            {
                case State.Down:
                    return new Position(position.getPosX() + 1, position.getPosY());
                case State.Up:
                    return new Position(position.getPosX() - 1, position.getPosY());
                case State.Left:
                    return new Position(position.getPosX(), position.getPosY()-1);
                case State.Right:
                    return new Position(position.getPosX(), position.getPosY()+1);
                default:
                    return position;
            }
        }

       public Aggressivity getAggressivity()
        {
            return aggressivity;
        }

        public void setDefensive()
        {
            aggressivity = Aggressivity.defensive;
        }

        public void setAgresive()
        {
            aggressivity = Aggressivity.aggresive;
        }

        public void setScatter()
        {
            mode = Mode.Scatter;
        }
        public void setMode(Mode mode)
        {
            this.mode = mode;
        }

        public void setNormal()
        {
            mode = Mode.Normal;
        }

        public Mode getMode()
        {
            return mode;
        }
    }
}
