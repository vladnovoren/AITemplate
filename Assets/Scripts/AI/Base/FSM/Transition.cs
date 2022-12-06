namespace AI.Base
{
    public class Transition
    {
        public Transition(IDecision decision, State trueState)
        {
            _decision = decision;
            TrueState = trueState;
        }

        public void Transit(StateMachine stateMachine)
        {
            if (_decision.Decide())
                stateMachine.ChangeState(TrueState);
        }

        public State TrueState { get; private set; }

        private IDecision _decision;
    }
}
