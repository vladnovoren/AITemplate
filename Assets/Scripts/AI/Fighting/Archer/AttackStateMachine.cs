using UnityEngine;
using AI.Base;

namespace AI.Fighting.Archer
{
    public class AttackStateMachine : StateMachine
    {
        public AttackStateMachine(GameObject agent, GameObject firePoint,
                                  GameObject arrowPrefab, GameObject enemy)
        {
            _agent = agent;
            _enemy = enemy;

            BuildIdleState();
            BuildAttackState(firePoint, arrowPrefab);
            BuildIdleToAttackTransition();
        }

        public State IdleState { get; private set; }
        public State AttackState { get; private set; }

        private void BuildIdleState()
        {
            IdleState = new State();
            Entry = IdleState;
        }

        private void BuildAttackState(GameObject firePoint, GameObject arrowPrefab)
        {
            var arch = new Arch(1.0f, firePoint.transform, arrowPrefab, _enemy);
            var fighter = new Fighter(arch);

            AttackState = new State();
            AttackState.AddAction(new AttackAction(fighter));
        }

        private void BuildIdleToAttackTransition(GameObject firePoint)
        {
            var toAttackDecision = new ToAttackDecision(_agent, _enemy);
            var toAttackTransition = new Transition(toAttackDecision, AttackState);
            IdleState.AddTransition(toAttackTransition);
        }

        private readonly GameObject _agent;
        private readonly GameObject _enemy;
    }
}
