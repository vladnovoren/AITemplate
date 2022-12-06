using AI.Base;
using UnityEngine;

namespace AI.Watch
{
    public class WatchStateMachine : StateMachine
    {
        public WatchStateMachine(GameObject agent, GameObject enemy)
        {
            _agent = agent;
            _enemy = enemy;
            BuildIdleState();
            BuildWatchState();
            BuildIdleToWatchTransition();
            BuildWatchToIdleTransition();
        }

        public State IdleState { get; private set; }
        public State WatchState { get; private set; }

        private void BuildIdleState()
        {
            IdleState = new State();
            Entry = IdleState;
        }

        private void BuildWatchState()
        {
            WatchState = new State();
            var watchAction = new WatchAction(_agent, _enemy);
            WatchState.AddAction(watchAction);
        }

        private void BuildIdleToWatchTransition()
        {
            var toWatchDecision = new ToWatchDecision(_agent, _enemy);
            var toWatchTransition = new Transition(toWatchDecision, WatchState);
            IdleState.AddTransition(toWatchTransition);
        }

        private void BuildWatchToIdleTransition()
        {
            var toIdleTransition = new Transition(new OppositeDecision(_toWatchDecision),
                                                  IdleState);
            WatchState.AddTransition(toIdleTransition);
        }

        private readonly GameObject _agent;
        private readonly GameObject _enemy;

        private readonly ToWatchDecision _toWatchDecision;
    } 
}
