using AI.Base;
using UnityEngine;
using Utils.Time;

namespace AI.Movement.Roam
{
    public class RoamStateMachine : StateMachine
    {
        public RoamStateMachine(GameObject agent) => _agent = agent;

        public void Build()
        {
            BuildStayState();
            BuildFollowState();
            Entry = StayState;
            BuildFollowToStayTransition();
            BuildStayToFollowTransition();
        }

        private void BuildStayState()
        {
            StayState = new State();
            _timer = new CountdownTimer();
            var stayAction = new StayAction(_agent, _timer, 1.0f, 2.0f);
            StayState.AddAction(stayAction);
            AddState(StayState);
        }

        private void BuildFollowState()
        {
            FollowState = new State();
            var followAction = new FollowAction(_agent, 1.0f, 2.0f);
            FollowState.AddAction(followAction);
            AddState(FollowState);
        }

        private void BuildStayToFollowTransition()
        {
            var stayToFollowDecision = new StayToFollowDecision(_timer);
            var stayToFollowTransition = new Transition(stayToFollowDecision,
                                                        FollowState);
            StayState.AddTransition(stayToFollowTransition);
        }

        private void BuildFollowToStayTransition()
        {
            var followToStayDecision = new FollowToStayDecision(_agent, 0.01f);
            var followToStayTransition = new Transition(followToStayDecision,
                                                        StayState);
            FollowState.AddTransition(followToStayTransition);
        }

        private CountdownTimer _timer;
        public State StayState { get; private set; }
        public State FollowState { get; private set; }

        private GameObject _agent;
    }
}