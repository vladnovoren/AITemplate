using AI.Base.FSM;
using System;

namespace AI.Base
{
    public class Transition
    {
        public Transition(ADecision decision, State trueState)
        {
            _decision = decision;
            TrueState = trueState;
        }

        public Transition(Transition other)
        {
            _decision = other._decision;
            TrueState = other.TrueState;
        }

        public bool Transit(StateMachine stateMachine)
        {
            if (_decision.Decide())
            {
                stateMachine.CurrentState = TrueState;
                OnAccepted();
                return true;
            }
            return false;
        }

        public event EventHandler<TransitionAcceptedArgs> Accepted;

        public void OnAccepted()
        {
            Accepted?.Invoke(this, new TransitionAcceptedArgs(TrueState));
        }

        public void SwitchToSourceState(object sender, TransitionAcceptedArgs newTrueState)
        {
            TrueState = newTrueState.TrueState;
        }

        public State TrueState { get; private set; }

        private ADecision _decision;
    }
}
