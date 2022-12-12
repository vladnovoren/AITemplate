﻿using System.Collections.Generic;
using Utils.Time;
using Utils.Math;

namespace AI.Base
{
    public class StateMachine
    {
        public StateMachine()
        {
            _states = new List<State>();
        }
        
        public void AddStateToList(State state)
        {
            _states.Add(state);
        }

        public State EntryState { get; set; }
        public State CurrentState
        {
            get => _currentState;
            set
            {
                _currentState?.OnExit();
                _currentState = value;
                _currentState.OnEnter();
            }
        }

        public virtual void OnEntry()
        {
            CurrentState = EntryState;
            CurrentState.OnEnter();
        }

        public virtual void Execute()
        {
            CurrentState.Execute(this);
        }

        public virtual void OnExit()
        {
            CurrentState.OnExit();
        }

        public void AddTransitionToAllStates(Transition transition)
        {
            foreach (var state in _states)
                state.AddTransition(new Transition(transition));
        }

        public void AddTransitionToAllStatesWithBack(Transition transition)
        {
            foreach (var state in _states)
            {
                var forwardTransition = new Transition(transition);
                var backTransition = new Transition(new BackDecision(forwardTransition),
                                                    state);
                state.AddTransition(forwardTransition);
                forwardTransition.TrueState.AddTransition(backTransition);
            }
        }
        public State MakeTimeout(Range timeout)
        {
            var entryState = new State();
            var timer = new CountdownTimer();
            var restartTimerAction = new RestartTimerAction(timer, timeout);
            entryState.AddAction(restartTimerAction);
            entryState.AddTransition(new Transition(new TrueDecision(), EntryState));

            var exitState = new State();
            AddTransitionToAllStates(new Transition(new TimeoutDecision(timer), exitState));

            EntryState = entryState;
            AddStateToList(EntryState);

            return exitState;
        }

        protected static void MergeCore(StateMachine result, StateMachine operand)
        {
             foreach (var state in operand._states)
                result.AddStateToList(state);           
        }

        protected readonly List<State> _states;
        protected State _currentState = null;
    }
}
