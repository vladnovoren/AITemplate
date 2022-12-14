using UnityEngine;
using Utils.Math;
using AI.Base;
using AI.Common.Chase;
using AI.Common.Events;
using AI.Configs.Archer.Fight.Dodge;
using AI.Configs.Archer.Fight.Stuff;
using AI.Common.Animations;

namespace AI.Configs.Archer.Fight
{
    public class FightStateMachine : StateMachine
    {
        public FightStateMachine(GameObject agent, GameObject firePoint,
                                 GameObject arrowPrefab, GameObject enemy,
                                 MovementNotifier movementNotifier,
                                 AnimationNotifier animationNotifier)
        {
            var attackAction = BuildAttackAction(firePoint, arrowPrefab, enemy);

            attackAction.NeedToComeCloser += movementNotifier.DispatchNeedToComeCloser;

            _chaseStateMachine = new TimeoutChaseStateMachine(agent, enemy,
                                                              movementNotifier,
                                                              new Range(3, 4));
            _dodgeStateMachine = new DodgeStateMachine(agent,
                                                      new Range(0, 0),
                                                      new Range(1, 2),
                                                      new Range(3, 4));
            ConnectChaseAndDodge();
            MergeCore(this, _chaseStateMachine);
            MergeCore(this, _dodgeStateMachine);
            AddActionToAllStates(attackAction);
            BuildAttackAnimationState(animationNotifier);
            EntryState = _chaseStateMachine.EntryState;
        }

        public State AttackAnimationState { get; private set; }

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
            var arch = new Arch(1.5f, firePoint.transform, arrowPrefab);
            var fighter = new Fighter(arch, enemy);
            return new AttackAction(fighter);
        }

        private void BuildAttackAnimationState(AnimationNotifier animationNotifier)
        {
            AttackAnimationState = new State();
            BuildAttackAnimationTransitions(animationNotifier);
        }

        private void BuildAttackAnimationTransitions(AnimationNotifier animationNotifier)
        {
            var toDecision = new ToMutingAnimationDecision();
            animationNotifier.AttackAnimationStarted += toDecision.OnAnimationStarted;

            var fromDecision = new FromMutingAnimationDecision();
            animationNotifier.AttackAnimationFinished += fromDecision.OnAnimationFinished;

            var toTransition = new Transition(toDecision, AttackAnimationState);
            var fromTransition = new Transition(fromDecision, toTransition);
            AddTransitionToAllStates(toTransition);
            AttackAnimationState.AddTransition(fromTransition);
        }

        private readonly TimeoutChaseStateMachine _chaseStateMachine;
        private readonly DodgeStateMachine _dodgeStateMachine;
    }
}