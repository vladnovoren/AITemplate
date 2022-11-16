namespace AI.Base
{
    public class StateMachine
    {
        public StateMachine(State initState)
        {
            CurrentState = initState;
        }

        public void ChangeState(State newState)
        {
            CurrentState.OnExit();
            CurrentState = newState;
            CurrentState.OnEnter();
        }

        public void OnEnter()
        {
            CurrentState.OnEnter();
        }

        public void Execute()
        {
            CurrentState.Execute(this);
        }

        public void OnExit()
        {
            CurrentState.OnExit();
        }

        public State CurrentState { get; private set; }
    }
}
