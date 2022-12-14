using AI.Base;
using AI.Common.Chase;
using AI.Common.Events;
using UnityEngine;

namespace AI.Configs.Swordsman.Fight
{
    public class FightStateMachine : StateMachine
    {
        public FightStateMachine(GameObject agent,
                                 MovementNotifier movementNotifier,
                                 GameObject enemy)
        {
            _chaseStateMachine = new ChaseStateMachine(agent, enemy,
                                                       movementNotifier);
            MergeCore(this, _chaseStateMachine);

            var fighter = new Fighter(agent, enemy, 2.0f);
            var attackAction = new AttackAction(fighter);
            AddActionToAllStates(attackAction);

            EntryState = _chaseStateMachine.EntryState;
        }

        private ChaseStateMachine _chaseStateMachine;
    }
}