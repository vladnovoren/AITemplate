using UnityEngine;
using Utils.Math;
using AI.Base;
using AI.Common.Roam;
using Unity.VisualScripting;

namespace AI.Configs.Archer.Fight
{
    public class FightStateMachine : StateMachine
    {
        public FightStateMachine(GameObject agent, GameObject firePoint,
                                 GameObject arrowPrefab, GameObject enemy)
        {
            _roamStateMachine = new RoamStateMachine(agent,
                                                     new Range(0, 0),
                                                     new Range(0, 1));
            var arch = new Arch(1.0f, firePoint.transform, arrowPrefab);
            var fighter = new Fighter(arch, enemy);
            var attackAction = new AttackAction(fighter);
            InitStates(agent, attackAction, enemy);
            InitTransitions(agent, enemy, attackAction);
        }

        public State ChaseState { get; private set; }

        public State CatchState { get; private set; }

        private void InitStates(GameObject agent, AttackAction attackAction,
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
            AddState(ChaseState);
            Entry = ChaseState;
        }

        private void InitCatchState(AttackAction attackAction)
        {
            CatchState = new State();
            CatchState.AddAction(attackAction);
            AddState(CatchState);
        }

        private void InitTransitions(GameObject agent, GameObject enemy,
                                     AttackAction attackAction)
        {
            var catchToChaseDecision = new CatchToChaseDecision(agent, enemy, attackAction);
            var catchToChaseTransition = new Transition(catchToChaseDecision, ChaseState);
            CatchState.AddTransition(catchToChaseTransition);

            var chaseToCatchTransition = new Transition(new OppositeDecision(catchToChaseDecision),
                                                        CatchState);
            ChaseState.AddTransition(chaseToCatchTransition);
        }

        private void ConnectWithRoam()
        {
            var 
        }

        private RoamStateMachine _roamStateMachine;
    }
}