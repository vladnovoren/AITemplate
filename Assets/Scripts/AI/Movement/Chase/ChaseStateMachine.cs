using AI.Base;
using UnityEngine;

namespace AI.Movement.Chase
{
    public class ChaseStateMachine : StateMachine
    {
        public ChaseStateMachine(GameObject agent, GameObject enemy)
        {
            _agent = agent;
            _enemy = enemy;
            BuildChaseState();
            BuildCatchState();
            BuildChaseToCatchTransition();
            BuildCatchToChaseTransition();
        }

        public State ChaseState { get; private set; }
        public State CatchState { get; private set; }

        private void BuildChaseState()
        {
            ChaseState = new State();
            var chaseAction = new ChaseAction(_agent, _enemy, 0.01f);
            ChaseState.AddAction(chaseAction);
            Entry = ChaseState;
        }

        private void BuildCatchState()
        {
            CatchState = new State();
        }

        private void BuildChaseToCatchTransition()
        {
            var toCatchDecision = new ToCatchDecision(_agent, _enemy);
            var toCatchTransition = new Transition(toCatchDecision, CatchState);
            ChaseState.AddTransition(toCatchTransition);
        }

        private void BuildCatchToChaseTransition()
        {
            var catchToChaseDecision = new OppositeDecision(_toCatchDecision);
            var catchToChaseTransition = new Transition(catchToChaseDecision, chaseState);
            CatchState.AddTransition(catchToChaseTransition);
        }

        private readonly ToCatchDecision _toCatchDecision;

        private readonly GameObject _agent;
        private readonly GameObject _enemy;
    }
}
