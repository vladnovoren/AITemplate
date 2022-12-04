using System.Collections.Generic;

namespace AI.Base
{
    public class StateGroup
    {
        public StateGroup(State entry)
        {
            Entry = entry;
            _states = new List<State>();
            AddState(entry);
        }

        public void AddState(State state)
        {
            _states.Add(state);
        }

        public void AddTransitionToAllStates(Transition transition)
        {
            foreach (var state in _states)
                state.AddTransition(transition);
        }

        public static StateGroup Merge(StateGroup fragment1,
            StateGroup fragment2, State entry)
        {
            var result = new StateGroup(entry);
            MergeCore(result, fragment1);
            MergeCore(result, fragment2);
            return result;
        }

        private static void MergeCore(StateGroup result, StateGroup operand)
        {
             foreach (var state in operand._states)
                result.AddState(state);           
        }

        public State Entry { get; private set; }
        private List<State> _states;
    }
}
