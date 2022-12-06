using UnityEngine;
using AI.Base;

namespace AI.Fighting.Swordsman
{
    public class AttackStateMachine : StateMachine
    {
        public AttackStateMachine(GameObject agent, GameObject enemy)
        {
            _agent = agent;
            _enemy = enemy;

            BuildIdleState();
            BuildAttackState();
            BuildIdleToAttackTransition();
        }

        public State IdleState { get; private set; }
        public State AttackState { get; private set; }

        private void BuildIdleState()
        {
            IdleState = new State();
            Entry = IdleState;
        }

        private void BuildAttackState()
        {
            var fighter = new Fighter(_agent, _enemy, 0.5f);

            var attackState = new State();
            attackState.AddAction(new AttackAction(fighter));
        }

        private void BuildIdleToAttackTransition()
        {
            var toAttackDecision = new ToAttackDecision(_agent, _enemy);
            var toAttackTransition = new Transition(toAttackDecision, AttackState);
            IdleState.AddTransition(toAttackTransition);
        }

        private readonly GameObject _agent;
        private readonly GameObject _enemy;
    }
}
