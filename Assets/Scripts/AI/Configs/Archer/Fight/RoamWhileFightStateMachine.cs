using AI.Base;
using AI.Common.Roam;
using UnityEngine;
using Utils.Math;
using Utils.Time;

namespace AI.Configs.Archer.Fight
{
    public class RoamWhileFightStateMachine : RoamStateMachine
    {
        public RoamWhileFightStateMachine(GameObject agent, Range stayTime,
                                          Range roamDistance, Range roamTime) :
            base(agent, stayTime, roamDistance)
        {
            var timer = AddEntryState(roamTime);
            AddFinalState();

            AddTransitionFromEntry();
            AddTransitionToExit(timer);

            AddStateToList(Entry);
            AddStateToList(ExitState);
        }

        public State ExitState { get; private set; }

        private CountdownTimer AddEntryState(Range roamTime)
        {
            Entry = new State();
            
            var timer = new CountdownTimer();
            var restartTimerAction = new RestartTimerAction(timer, roamTime);
            Entry.AddAction(restartTimerAction);

            return timer;
        }

        private void AddFinalState()
        {
            ExitState = new State();
        }

        private void AddTransitionFromEntry()
        {
            var trueDecision = new TrueDecision();
            var transition = new Transition(trueDecision, StayState);
            Entry.AddTransition(transition);
        }

        private void AddTransitionToExit(CountdownTimer timer)
        {
            var timeoutDecision = new TimeoutDecision(timer);
            var transition = new Transition(timeoutDecision, ExitState);
            AddTransitionToAllStates(transition);
        }
    }
}