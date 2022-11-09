namespace AI.Base
{
    public class Transition
    {
        public Transition(IDecision decision, State trueState)
        {
            _decision = decision;
            _trueState = trueState;
        }

        public void Transit(StateMachine stateMachine)
        {
            if (_decision.Decide())
                stateMachine.ChangeState(_trueState);
        }

        private IDecision _decision;
        private State _trueState;
    }
}
