using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PacMan_CHABRIER_REGNARD
{
    public enum State {Wait,Up,Down,Left,Right,Nothing};

    class Character
    {
        protected Position position;
        protected State state;

        public Character()
        {
            position = new Position(1, 1);
        }

        public void movement(State state, Map map)
        {
            Position posTemp = new Position(position.getPosX(),position.getPosY());

            switch (state)
            {
                case State.Nothing:
                    movement(this.state, map);
                    break;

                case State.Up:
                    posTemp.setPosX(posTemp.getPosX() - 1);

                    if (map.checkElement(posTemp) > Element.Wall)
                    {
                        position = posTemp;
                        this.state = state;
                        map.setElement(posTemp, Element.Nothing);
                        
                    }
                    else
                    {
                        this.state = State.Wait;
                    }

                    break;

                case State.Down:
                    posTemp.setPosX(posTemp.getPosX() + 1);
                    if (map.checkElement(posTemp) > Element.Wall)
                    {
                        position = posTemp;
                        this.state = state;
                        map.setElement(posTemp, Element.Nothing);
                    }
                    else
                    {
                        this.state = State.Wait;
                    }


                    break;

                case State.Left:
                    posTemp.setPosY(posTemp.getPosY() - 1);
                    if (map.checkElement(posTemp) > Element.Wall)
                    {
                        position = posTemp;
                        this.state = state;
                        map.setElement(posTemp, Element.Nothing);

                    }
                    else
                    {
                        this.state = State.Wait;
                    }


                    break;

                case State.Right:
                    posTemp.setPosY(posTemp.getPosY() + 1);
                    if (map.checkElement(posTemp) > Element.Wall)
                    {
                        position = posTemp;
                        this.state = state;
                        map.setElement(posTemp, Element.Nothing);
                    }
                    else
                    {
                        this.state = State.Wait;
                    }


                    break;
            }
        }
        
        public Position getPosition()
        {
            return position;
        }
    }
}
