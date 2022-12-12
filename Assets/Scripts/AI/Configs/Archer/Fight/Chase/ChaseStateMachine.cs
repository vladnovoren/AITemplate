using UnityEngine;
using AI.Base;
using AI.Configs.Archer.Fight.Stuff;
using Utils.Math;

namespace AI.Configs.Archer.Fight.Chase
{
    public class ChaseStateMachine : StateMachine
    {
        public ChaseStateMachine(GameObject agent, GameObject firePoint,
                                 GameObject arrowPrefab, GameObject enemy,
                                 Range timeout)
        {
            var arch = new Arch(1.0f, firePoint.transform, arrowPrefab);
            var fighter = new Fighter(arch, enemy);
            var attackAction = new AttackAction(fighter);
            InitStates(agent, attackAction, enemy);
            InitTransitions(agent, enemy, attackAction);
            ExitState = MakeTimeout(timeout);
        }

        public State ChaseState { get; private set; }
        public State CatchState { get; private set; }
        public State ExitState { get; private set; }

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
            AddStateToList(ChaseState);
        }

        private void InitCatchState(AttackAction attackAction)
        {
            CatchState = new State();
            CatchState.AddAction(attackAction);
            AddStateToList(CatchState);
            EntryState = CatchState;
        }

        private void InitTransitions(GameObject agent, GameObject enemy,
                                     AttackAction attackAction)
        {
            var catchToChaseDecision = InitCatchToChaseTransition(agent, enemy, attackAction);
            InitChaseToCatchTransition(catchToChaseDecision);
        }

        private CatchToChaseDecision InitCatchToChaseTransition(GameObject agent,
                                                                GameObject enemy,
                                                                AttackAction attackAction)
        {
            var catchToChaseDecision = new CatchToChaseDecision(agent, enemy, attackAction);
            var catchToChaseTransition = new Transition(catchToChaseDecision, ChaseState);
            CatchState.AddTransition(catchToChaseTransition);
            return catchToChaseDecision;
        }

        private void InitChaseToCatchTransition(CatchToChaseDecision catchToChaseDecision)
        {
            var chaseToCatchTransition = new Transition(new OppositeDecision(catchToChaseDecision),
                                                        CatchState);
            ChaseState.AddTransition(chaseToCatchTransition);
        }
    }
}