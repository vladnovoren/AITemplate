using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using AI.Base;
using AI.Roaming;

public class EnemyAI : MonoBehaviour
{
    public void Awake()
    {
        _roamingFragment = BuildRoamingFragment();
        _stateMachine = new StateMachine(_roamingFragment.Entry);
    }

    private void Start()
    {
        _stateMachine.OnEnter();
    }

    private void Update()
    {
        _stateMachine.Execute();
    }

    private StateMachineFragment BuildRoamingFragment()
    {
        var stayState = new State();
        var followState = new State();

        var timer = new CountDownTimer();
        var stayAction = new StayAction(enemy, timer, 1.0f, 2.0f);

        var stayToFollowDecision = new StayToFollowDecision(timer);
        var stayToFollowTransition = new Transition(stayToFollowDecision, followState);

        var followAction = new FollowAction(enemy, 1.0f, 2.0f);

        var followToStayDecision = new FollowToStayDecision(enemy, 0.01f);
        var followToStayTransition = new Transition(followToStayDecision, stayState);

        stayState.AddAction(stayAction);
        stayState.AddTransition(stayToFollowTransition);

        followState.AddAction(followAction);
        followState.AddTransition(followToStayTransition);

        _roamingFragment = new StateMachineFragment(stayState);
        _roamingFragment.AddState(followState);

        return _roamingFragment;
    }

    [SerializeField] private GameObject enemy;

    private StateMachine _stateMachine;
    private StateMachineFragment _roamingFragment;
}
