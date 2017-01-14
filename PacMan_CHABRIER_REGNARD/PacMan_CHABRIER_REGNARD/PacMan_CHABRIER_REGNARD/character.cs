using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PacMan_CHABRIER_REGNARD
{
    public enum State {Up,Left,Down,Right,Nothing, Wait};

    class Character
    {
        protected Position position;
        protected State state;

        public Character()
        {
            position = new Position(20, 15);
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
                        if( this is PacMan)
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
                        if (this is PacMan)
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
                        if (this is PacMan)
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
                        if (this is PacMan)
                            map.setElement(posTemp, Element.Nothing);
                    }
                    else
                    {
                        this.state = State.Wait;
                    }


                    break;
            }

            if (position.equals(map.getTP1()) && this.state == State.Left)
            {
                position.setPosX(map.getTP2().getPosX());
                position.setPosY(map.getTP2().getPosY());
                this.state = State.Left;
            } else if (position.equals(map.getTP2()) && this.state == State.Right)
            {
                position.setPosX(map.getTP1().getPosX());
                position.setPosY(map.getTP1().getPosY());
                state = State.Right;
            }
        }
        
        public State getState()
        {
            return this.state;
        }

        public Position getPosition()
        {
            return position;
        }

    }
}
