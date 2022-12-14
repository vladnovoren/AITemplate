using System;
using UnityEngine;

namespace AI.Base.FSM
{
    public class TransitionAcceptedArgs : EventArgs
    {
        public TransitionAcceptedArgs(State trueState)
        {
            TrueState = trueState;
        }

        public State TrueState;
    }
}