using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PacMan_CHABRIER_REGNARD
{
    class Ghost : Character
    {

        protected Position target;
        private State nextMove;
        private bool[] intersect;

        public Ghost()
        {
            target = new Position(0, 0);
            nextMove = State.Nothing;
            intersect = new bool[4];
            for (int i = 0; i < 4; i++)
                intersect[i] = false;
        }

        public void computeNextMove(PacMan pac, Map map)
        {
            computeTargetTile(pac);
            Position next = nextTile();
            if(checkIntersection(next, map))
            {
               this.state =  selectIntersect();
            } 


        }

        public State getNextMove()
        {
            return nextMove;
        }


        private State selectIntersect()
        {
            State inverse = (State)((int)this.state + 2 % 4);
            intersect[(int)inverse] = false;
            int distance = 100000;
            int j = 0;
            for(int i = 0; i < 4; i++)
            {
                if (intersect[i])
                {

                    if (manhattanDistance(target) < distance)
                        j = i;
                }
            }

            return (State) j; 
        }

        private int manhattanDistance(Position taget)
        {
            return (Math.Abs(this.position.getPosX() - target.getPosX()) + Math.Abs(this.position.getPosY() - target.getPosY()));
        }

        protected virtual void  computeTargetTile(PacMan pac)
        {
            //To override
        }

        private bool checkIntersection(Position pos, Map map)
        {
            Position up, down, left, right;
            int i = 0;
            up = new Position(pos.getPosX() - 1, pos.getPosY());
            down = new Position(pos.getPosX() + 1, pos.getPosY());
            left = new Position(pos.getPosX(), pos.getPosY()-1);
            right = new Position(pos.getPosX(), pos.getPosY()+1);

            Element elmt = map.checkElement(up);
            if (elmt != Element.Wall)
            {
                i++;
                intersect[0] = true;
            }
                
            elmt = map.checkElement(down);
            if (elmt != Element.Wall)
                i++;
            elmt = map.checkElement(left);
            if (elmt != Element.Wall)
                i++;
            elmt = map.checkElement(right);
            if (elmt != Element.Wall)
                i++;

            if (i > 2)
                return true;
            return false;

        }

        private Position nextTile()
        {
            switch (this.state)
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
    }
}
