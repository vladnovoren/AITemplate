namespace AI.Base
{
    public class Transition
    {
        public Transition(ADecision decision, State trueState)
        {
            _decision = decision;
            TrueState = trueState;
        }

        public void Transit(StateMachine stateMachine)
        {
            if (_decision.Decide())
                stateMachine.CurrentState = TrueState;
        }

        public State TrueState { get; private set; }

        private ADecision _decision;
    }
}
