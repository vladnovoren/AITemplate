using System.Collections.Generic;

namespace AI.Base
{
    public class StateMachineFragment
    {
        public StateMachineFragment()
        {
            _states = new List<State>();
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

        public static StateMachineFragment operator+(StateMachineFragment fragment1,
            StateMachineFragment fragment2)
        {
            StateMachineFragment result = new StateMachineFragment();
            OperatorPlusCore(result, fragment1);
            OperatorPlusCore(result, fragment2);
            return result;
        }

        private static void OperatorPlusCore(StateMachineFragment result, StateMachineFragment operand)
        {
             foreach (var state in operand._states)
                result.AddState(state);           
        }

        private List<State> _states;
    }
}