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
            Position postemp = new Position();

            switch (state)
            {   
                case State.Wait:
                    postemp = this.position;

                    break;

                case State.Up:

                    break;

                case State.Down:

                    break;

                case State.Left:

                    break;

                case State.Right:

                    break;
            }
        }
        
    }
}
