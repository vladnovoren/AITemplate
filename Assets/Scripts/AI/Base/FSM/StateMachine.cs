using System.Collections.Generic;

namespace AI.Base
{
    public class StateMachine
    {
        public StateMachine()
        {
            _states = new List<State>();
        }
        
        public void AddStateToList(State state)
        {
            _states.Add(state);
        }

        public State Entry { get; set; }
        public State CurrentState
        {
            get => _currentState;
            set
            {
                _currentState?.OnExit();
                _currentState = value;
                _currentState.OnEnter();
            }
        }

        public virtual void OnEntry()
        {
            CurrentState = Entry;
            CurrentState.OnEnter();
        }

        public virtual void Execute()
        {
            CurrentState.Execute(this);
        }

        public virtual void OnExit()
        {
            CurrentState.OnExit();
        }

        public void AddTransitionToAllStates(Transition transition)
        {
            foreach (var state in _states)
                state.AddTransition(transition);
            AddStateToList(transition.TrueState);
        }

        protected static void MergeCore(StateMachine result, StateMachine operand)
        {
             foreach (var state in operand._states)
                result.AddStateToList(state);           
        }

        protected readonly List<State> _states;
        protected State _currentState = null;
    }
}
