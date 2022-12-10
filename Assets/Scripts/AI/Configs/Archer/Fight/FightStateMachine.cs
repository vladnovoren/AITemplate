using UnityEngine;
using AI.Base;
using System;

namespace AI.Configs.Archer.Fight
{
    public class FightStateMachine : StateMachine
    {
        public FightStateMachine(GameObject agent, GameObject firePoint,
                                 GameObject arrowPrefab, GameObject enemy)
        {
            var arch = new Arch(1.0f, firePoint.transform, arrowPrefab);
            var fighter = new Fighter(arch, enemy);
            var attackAction = new AttackAction(fighter);
            InitStates(agent, firePoint, arrowPrefab, attackAction, enemy);
            InitTransitions(agent, enemy, attackAction);
        }

        public State ChaseState { get; private set; }

        public State CatchState { get; private set; }

        private void InitStates(GameObject agent, GameObject firePoint,
                                GameObject arrowPrefab, AttackAction attackAction,
                                GameObject enemy)
        {
            InitChaseState(agent, enemy, attackAction);
            InitCatchState(attackAction);
        }

        private void InitChaseState(GameObject agent, GameObject enemy,
                                    AttackAction attackAction)
        {
            ChaseState = new State();
            var chaseAction = new ChaseAction(agent, enemy, 0.01f);
            ChaseState.AddAction(chaseAction);
            ChaseState.AddAction(attackAction);
        }

        private void InitCatchState(AttackAction attackAction)
        {
            CatchState = new State();
            CatchState.AddAction(attackAction);
        }

        private void InitTransitions(GameObject agent, GameObject enemy,
                                     AttackAction attackAction)
        {
            var chaseToCatchDecision = new CatchToChaseDecision(agent, enemy, attackAction);
            var chaseToCatchTransition = new Transition(chaseToCatchDecision, CatchState);
            ChaseState.AddTransition(chaseToCatchTransition);

            var catchToChaseTransition = new Transition(new OppositeDecision(chaseToCatchDecision),
                                                        CatchState);
            CatchState.AddTransition(catchToChaseTransition);
        }
    }
}