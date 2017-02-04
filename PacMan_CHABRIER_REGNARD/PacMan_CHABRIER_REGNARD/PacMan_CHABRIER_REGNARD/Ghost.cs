using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PacMan_CHABRIER_REGNARD
{
    public enum Aggressivity {aggresive, defensive };
    public enum Mode {Normal, Scatter, StayIn, GoOut};

    class Ghost : Character
    {

        protected Position target;
        protected Aggressivity aggressivity;
        protected Mode mode;
        private State nextMove;
        private bool[] intersect;
        protected int turnToGoOut;

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
            resetIntersect();
            computeTargetTile(pac, ghost);
            checkIntersection(map);
            computeState();

        }

        private void resetIntersect()
        {
            for (int i = 0; i < 4; i++)
                intersect[i] = true;
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

            if(this.state != State.Nothing)
            {
                int inv = (int)this.state;
                inv += 2;
                inv %= 4;
                intersect[inv] = false;
            }
            
            Position tmp = new Position(0, 0);
            //State inverse = (State)(inv);
            
            int distance = 100000;
            int manhattan = 0;
            int j = 0;
            bool changed = false;
            for(int i = 0; i < 4; i++)
            {
                if (intersect[i])
                {
                    tmp = nextTile((State)i);
                    manhattan = manhattanDistance(target, tmp);
                    if ( manhattan < distance)
                    {
                        distance = manhattan;
                        j = i;
                        changed = true;
                    }
                        
                }
            }
            if(changed)
                return (State) j;
            return this.state;
        }


        protected int manhattanDistance(Position taget, Position pos)
        {
            return (Math.Abs(pos.getPosX() - target.getPosX()) + Math.Abs(pos.getPosY() - target.getPosY()));
        }

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
