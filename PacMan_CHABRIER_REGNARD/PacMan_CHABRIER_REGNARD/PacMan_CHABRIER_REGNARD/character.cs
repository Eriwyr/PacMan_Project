using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PacMan_CHABRIER_REGNARD
{
    public enum State {Wait,Up,Down,Left,Right};

    class Character
    {
        protected Position position;
        protected State state;

        public void movement(Position position, State state, Map map)
        {
            Position posTemp = new Position(position.getPosX(),position.getPosY());

            switch (state)
            {   
                case State.Wait:

                    break;

                case State.Up:
                    posTemp.setPosX(posTemp.getPosX() - 1);

                    if (map.checkElement(posTemp) != 0)
                    {
                        position = posTemp;
                    }
                    break;

                case State.Down:
                    posTemp.setPosX(posTemp.getPosX() + 1);
                    if (map.checkElement(posTemp) != 0)
                    {
                        position = posTemp;
                    }

                    break;

                case State.Left:
                    posTemp.setPosY(posTemp.getPosY() - 1);
                    if (map.checkElement(posTemp) != 0)
                    {
                        position = posTemp;
                    }

                    break;

                case State.Right:
                    posTemp.setPosY(posTemp.getPosY() + 1);
                    if (map.checkElement(posTemp) != 0)
                    {
                        position = posTemp;
                    }

                    break;
            }
        }
        
    }
}
