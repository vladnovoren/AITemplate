using UnityEngine;
using Utils.Math;
using Utils.Time;
using AI.Base;
using AI.Configs.Archer.Fight.Dodge;
using AI.Configs.Archer.Fight.Chase;

namespace AI.Configs.Archer.Fight
{
    public class FightStateMachine : StateMachine
    {
        public FightStateMachine(GameObject agent, GameObject firePoint,
                                 GameObject arrowPrefab, GameObject enemy)
        {
            _chaseStateMachine = new ChaseStateMachine(agent, firePoint,
                                                       arrowPrefab, enemy,
                                                       new Range(3, 4));
            _dodgeStateMachine = new DodgeStateMachine(agent,
                                                      new Range(0, 0),
                                                      new Range(1, 2),
                                                      new Range(3, 4));
            ConnectChaseAndDodge();
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

        private readonly ChaseStateMachine _chaseStateMachine;
        private readonly DodgeStateMachine _dodgeStateMachine;
    }
}