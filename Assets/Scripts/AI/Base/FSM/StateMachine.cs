using System.Collections.Generic;

namespace AI.Base
{
    public class StateMachine
    {
        public void AddState(State state)
        {
            _states.Add(state);
        }

        public State Entry { get; set; }

        public void AddTransitionToAllStates(Transition transition)
        {
            foreach (var state in _states)
                state.AddTransition(transition);
            AddState(transition.TrueState);
        }

        public static StateMachine Merge(StateMachine stateMachine1, StateMachine stateMachine2,
                                         State entryState)
        {
            var result = new StateMachine();
            MergeCore(result, stateMachine1);
            MergeCore(result, stateMachine2);
            result.Entry = entryState;
            return result;
        }

        private static void MergeCore(StateMachine result, StateMachine operand)
        {
             foreach (var state in operand._states)
                result.AddState(state);           
        }

        private readonly List<State> _states;
    }
}
