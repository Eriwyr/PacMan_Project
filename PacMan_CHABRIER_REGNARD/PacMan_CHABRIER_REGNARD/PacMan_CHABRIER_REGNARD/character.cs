using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PacMan_CHABRIER_REGNARD
{
    public enum State {Up,Left,Down,Right,Nothing, Wait, Quit};

    class Character
    {
        protected Position position;
        protected State state;

        public Character()
        {
            position = new Position(23, 13);
            state = State.Wait;
        }

        public Boolean movement(State state, Map map)
        {
            Position posTemp = new Position(position.getPosX(),position.getPosY());
            Boolean chase = false;
            switch (state)
            {
                case State.Nothing:
                    return movement(this.state, map);

                case State.Up:
                    posTemp.setPosX(posTemp.getPosX() - 1);

                    if (map.checkElement(posTemp) > Element.Wall)
                    {
                        position = posTemp;
                        this.state = state;
                        if( this is PacMan)
                        {
                            if (map.checkElement(posTemp) == Element.BigBeans)
                                chase = true;
                            map.setElement(posTemp, Element.Nothing);
                        }
                           
                        
                    }
                    else
                    {
                        //this.state = State.Wait;
                    }

                    break;

                case State.Down:
                    posTemp.setPosX(posTemp.getPosX() + 1);
                    if (map.checkElement(posTemp) > Element.Wall)
                    {
                        position = posTemp;
                        this.state = state;
                        if (this is PacMan)
                        {
                            if (map.checkElement(posTemp) == Element.BigBeans)
                            {
                                chase = true;
                            }
                                
                            map.setElement(posTemp, Element.Nothing);
                        }
                    }
                    else
                    {
                       // this.state = State.Wait;
                    }


                    break;

                case State.Left:
                    posTemp.setPosY(posTemp.getPosY() - 1);
                    if (map.checkElement(posTemp) > Element.Wall)
                    {
                        position = posTemp;
                        this.state = state;
                        if (this is PacMan)
                        {
                            if (map.checkElement(posTemp) == Element.BigBeans)
                                chase = true;
                            map.setElement(posTemp, Element.Nothing);
                        }

                    }
                    else
                    {
                        //this.state = State.Wait;
                    }


                    break;

                case State.Right:
                    posTemp.setPosY(posTemp.getPosY() + 1);
                    if (map.checkElement(posTemp) > Element.Wall)
                    {
                        position = posTemp;
                        this.state = state;
                        if (this is PacMan)
                        {
                            if (map.checkElement(posTemp) == Element.BigBeans)
                                chase = true;
                            map.setElement(posTemp, Element.Nothing);
                        }
                    }
                    else
                    {
                        //this.state = State.Wait;
                    }


                    break;
            }

            if (position.equals(map.getTP1()) && this.state == State.Left)
            {
                position.setPosX(map.getTP2().getPosX());
                position.setPosY(map.getTP2().getPosY());
                this.state = State.Left;
                if (this is PacMan)
                {
                    if (map.checkElement(posTemp) == Element.BigBeans)
                        chase = true;
                    if (map.checkElement(position) != Element.Nothing)
                        map.setElement(position, Element.Nothing);
                }
               

            } else if (position.equals(map.getTP2()) && this.state == State.Right)
            {
                position.setPosX(map.getTP1().getPosX());
                position.setPosY(map.getTP1().getPosY());
                state = State.Right;
                if (this is PacMan)
                {
                    if (map.checkElement(posTemp) == Element.BigBeans)
                        chase = true;
                    if (map.checkElement(position) != Element.Nothing)
                        map.setElement(position, Element.Nothing);
                }
            }

            if (chase)
            {
                Console.WriteLine("Returning TRUE");
                return true;
            }
            return false;
        }


        public bool checkMovement(State state, Map map)
        {
            Position posTemp = new Position(position.getPosX(), position.getPosY()); ;
            switch (state)
            {
                case State.Up:
                    posTemp.setPosX(posTemp.getPosX() - 1);
                    if (map.checkElement(posTemp) > Element.Wall)
                    {
                        return true;
                    }
                    break;
                case State.Down:
                    posTemp.setPosX(posTemp.getPosX() + 1);
                    if (map.checkElement(posTemp) > Element.Wall)
                    {
                        return true;
                    }
                    break;
                case State.Left:
                    posTemp.setPosY(posTemp.getPosY() - 1);
                    if (map.checkElement(posTemp) > Element.Wall)
                    {
                        return true;
                    }
                    break;
                case State.Right:
                    posTemp.setPosY(posTemp.getPosY() + 1);
                    if (map.checkElement(posTemp) > Element.Wall)
                    {
                        return true;
                    }
                    break;
                default:
                    return true;
            }
            return false;
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
