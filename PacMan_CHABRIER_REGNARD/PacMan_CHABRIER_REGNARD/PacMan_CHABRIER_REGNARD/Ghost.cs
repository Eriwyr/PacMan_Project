using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PacMan_CHABRIER_REGNARD
{
    class Ghost : Character
    {

        private Position target;
        private State nextMove;

        public Ghost()
        {
            target = new Position(0, 0);
            nextMove = State.Nothing;
        }

        public State computeNextMove(PacMan pac, Map map)
        {
            computeTargetTile(pac);
            Position next = nextTile();



            return State.Nothing;
        }


        public void computeTargetTile(PacMan pac)
        {
            //To override
        }

        public bool checkIntersection(Position pos, Map map)
        {
            Position up, down, left, right;
            int i = 0;
            up = new Position(pos.getPosX() - 1, pos.getPosY());
            down = new Position(pos.getPosX() + 1, pos.getPosY());
            left = new Position(pos.getPosX(), pos.getPosY()-1);
            right = new Position(pos.getPosX(), pos.getPosY()+1);

            Element elmt = map.checkElement(up);
            if (elmt != Element.Wall)
                i++;
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

        public Position nextTile()
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
