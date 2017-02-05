using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PacMan_CHABRIER_REGNARD
{
    //Enumeration to know the state of the character

    public enum State {
        Up,Left,Down,Right, //The four directions possible
        Nothing, //Keeping the same direction
        Wait, //Stop every movements
        Quit //Exit the game
    };

    class Character
    {
        protected Position position;
        protected State state;
        protected int score;
        public Character()
        {
            position = new Position(23, 13);
            state = State.Wait;
            score = 0;
        }


        //This will make the character move
        public Boolean movement(State state, Map map)
        {
            Position posTemp = new Position(position.getPosX(),position.getPosY());
            Boolean chase = false;
            switch (state)
            {
                case State.Nothing: //We call again the function with the same state as before
                    return movement(this.state, map);


                case State.Up:
                    posTemp.setPosX(posTemp.getPosX() - 1);


                    //If there is no wall on the next position
                    if (map.checkElement(posTemp) > Element.Wall)
                    {
                        position = posTemp;
                        this.state = state;

                        //If the pacman is calling the function
                        if ( this is PacMan)
                        {

                            if (map.checkElement(posTemp) == Element.BigBeans)
                            {
                                score += 200;
                                chase = true;
                            }
                            if (map.checkElement(posTemp) == Element.Beans)
                                score += 10;

                            if(map.checkElement(posTemp) < Element.Nothing)
                            {
                                //We remove beans from the map
                                map.removeBean();
                            }
                            map.setElement(posTemp, Element.Nothing);
                        }


                    }
                    else
                    {
                        //this.state = State.Wait;
                    }

                    break;

                //Same steps for every directions
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
                                score += 200;
                                chase = true;
                            }
                            if (map.checkElement(posTemp) == Element.Beans)
                                score += 10;

                            if (map.checkElement(posTemp) < Element.Nothing)
                            {
                                map.removeBean();
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
                            {
                                score += 200;
                                chase = true;
                            }
                            if (map.checkElement(posTemp) == Element.Beans)
                                score += 10;

                            if (map.checkElement(posTemp) < Element.Nothing)
                            {
                                map.removeBean();
                            }
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
                            {
                                score += 200;
                                chase = true;
                            }
                                
                            if (map.checkElement(posTemp) == Element.Beans)
                                score += 10;

                            if (map.checkElement(posTemp) < Element.Nothing)
                            {
                                map.removeBean();
                            }
                            map.setElement(posTemp, Element.Nothing);
                        }
                    }
                    else
                    {
                        //this.state = State.Wait;
                    }


                    break;
            }

            //If we are on the teleporters then we change to the other teleporter
            if (position.equals(map.getTP1()) && this.state == State.Left)
            {
                position.setPosX(map.getTP2().getPosX());
                position.setPosY(map.getTP2().getPosY());
                this.state = State.Left;
                if (this is PacMan)
                {
                    if (map.checkElement(posTemp) == Element.BigBeans)
                        chase = true;

                    if (map.checkElement(posTemp) < Element.Nothing)
                    {
                        map.removeBean();
                    }


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

                    if (map.checkElement(posTemp) < Element.Nothing)
                    {
                        map.removeBean();
                    }
                    if (map.checkElement(position) != Element.Nothing)
                        map.setElement(position, Element.Nothing);
                }
            }

            if (chase)
            {
                return true;
            }
            return false;   
        }

        //Check if the movement is possible
        public bool checkMovement(State state, Map map)
        {
            Position posTemp = new Position(position.getPosX(), position.getPosY());
            //We check the tile corresponding to the new position
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
        public int getScore()
        {
            return score;
        }

    }
}
