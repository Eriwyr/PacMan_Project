using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PacMan_CHABRIER_REGNARD
{
    class BufferInput
    {


        //Class to control user inputs

        //Implementation of a linked list
        private Input first = null;
        private Input last = null;
        private int count = 0;

        private class Input
        {
            public Input prev; //Link to self
            private State state; //The state we store

            public Input(State state) { 
                this.state = state;
                prev = null;
            }
            
            public State getState()
            {
                return state;
            }

        }

        public void push(State state) //Push a new state at the end of the list
        {
            Input input = new Input(state);
            if(first == null)
            {
                first = input;
                last = input;
                count++;
            } else
            {
               if(count <= 2) //We verify that we don't have more than 2 inputs
                {
                    last.prev = input;
                    last = input;
                    count++;
                } else 
                {
                    //We override the last input
                    this.pop();
                    this.push(state);
                }
            }
            
        }

        public State getHead() //Return the first element of the list without deleting it
        {
            return first.getState();
        }

        public void clear() //Remove every inpput of the list
        {
            while(count > 0)
            {
                this.pop();
            }
        }

        public State pop() //Return and delete the last element of the list
        {
            Input tmp = first;
            first = first.prev;
            count--;
            return tmp.getState();
        }

        public int getCount() //REturn the number of inputs in the lists
        {
            return this.count;
        }
    }
}
