using UnityEngine;
using Utils.Math;
using AI.Base;
using AI.Configs.Archer.Fight.Dodge;
using AI.Configs.Archer.Fight.Chase;
using AI.Configs.Archer.Fight.Stuff;

namespace AI.Configs.Archer.Fight
{
    public class FightStateMachine : StateMachine
    {
        public FightStateMachine(GameObject agent, GameObject firePoint,
                                 GameObject arrowPrefab, GameObject enemy)
        {
            var attackAction = BuildAttackAction(firePoint, arrowPrefab, enemy);
            _chaseStateMachine = new ChaseStateMachine(agent, enemy,
                                                       attackAction,
                                                       new Range(3, 4));
            _dodgeStateMachine = new DodgeStateMachine(agent,
                                                      new Range(0, 0),
                                                      new Range(1, 2),
                                                      new Range(3, 4));
            ConnectChaseAndDodge();
            MergeCore(this, _chaseStateMachine);
            MergeCore(this, _dodgeStateMachine);
            AddActionToAllStates(attackAction);
            EntryState = _chaseStateMachine.EntryState;
        }

        public void ConnectChaseAndDodge()
        {
            _chaseStateMachine.ExitState.AddTransition(
                new Transition(new TrueDecision(),
                               _dodgeStateMachine.EntryState)
            );

            _dodgeStateMachine.ExitState.AddTransition(
                new Transition(new TrueDecision(),
                               _chaseStateMachine.EntryState)
            );
        }

        private AttackAction BuildAttackAction(GameObject firePoint,
                                               GameObject arrowPrefab,
                                               GameObject enemy)
        {
            var arch = new Arch(1.0f, firePoint.transform, arrowPrefab);
            var fighter = new Fighter(arch, enemy);
            return new AttackAction(fighter);
        }

        private readonly ChaseStateMachine _chaseStateMachine;
        private readonly DodgeStateMachine _dodgeStateMachine;
    }
}