using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PacMan_CHABRIER_REGNARD
{
    class BufferInput
    {

        private Input first = null;
        private Input last = null;
        private int count = 0;

        private class Input
        {
            public Input prev;
            private State state;

            public Input(State state) {
                this.state = state;
                prev = null;
            }
            
            public State getState()
            {
                return state;
            }

        }

        public void push(State state)
        {
            Input input = new Input(state);
            if(first == null)
            {
                first = input;
                last = input;
                count++;
            } else
            {
               if(count <= 2)
                {
                    last.prev = input;
                    last = input;
                    count++;
                } else
                {
                    this.pop();
                    this.push(state);
                }
            }
            
        }

        public State getHead()
        {
            return first.getState();
        }

        public void clear()
        {
            while(count > 0)
            {
                this.pop();
            }
        }

        public State pop()
        {
            Input tmp = first;
            first = first.prev;
            count--;
            return tmp.getState();
        }

        public int getCount()
        {
            return this.count;
        }
    }
}
