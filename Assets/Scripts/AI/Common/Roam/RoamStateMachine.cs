using AI.Base;
using UnityEngine;
using Utils.Time;

namespace AI.Common.Roam
{
    public class RoamStateMachine : StateMachine
    {
        public RoamStateMachine(GameObject agent)
        {
            _agent = agent;
            BuildStayState();
            BuildFollowState();
            BuildFollowToStayTransition();
            BuildStayToFollowTransition();
        }

        public State StayState { get; private set; }
        public State FollowState { get; private set; }

        private void BuildStayState()
        {
            StayState = new State();
            _timer = new CountdownTimer();
            var stayAction = new StayAction(_agent, _timer, 1.0f, 2.0f);
            StayState.AddAction(stayAction);
            AddState(StayState);
            Entry = StayState;
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

        private GameObject _agent;
    }
}