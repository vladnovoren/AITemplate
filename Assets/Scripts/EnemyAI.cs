using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using AI.Base;
using AI.Chasing;
using AI.Roaming;

public class EnemyAI : MonoBehaviour
{
    public void Awake()
    {
        InitFov();
        _stateMachine = BuildStateMachine();
    }

    private void Start()
    {
        _stateMachine.OnEnter();
    }

    private void Update()
    {
        _stateMachine.Execute();
    }

    private StateMachine BuildStateMachine()
    {
        var roamFragment = BuildRoamFragment();
        var chaseFragment = BuildChaseFragment();

        var roamToChaseDecision = new ToChaseDecision(gameObject, enemy);
        var roamToChaseTransition = new Transition(roamToChaseDecision,
                                            chaseFragment.Entry);
        roamFragment.AddTransitionToAllStates(roamToChaseTransition);

        return new StateMachine(roamFragment.Entry);
    }

    private void InitFov() {
        var fov = gameObject.GetComponent<FieldOfView>();
        fov.Init(10.0f);
    }

    private StateMachineFragment BuildRoamFragment()
    {
        var stayState = new State();
        var followState = new State();

        var timer = new CountDownTimer();
        var stayAction = new StayAction(gameObject, timer, 1.0f, 2.0f);

        var stayToFollowDecision = new StayToFollowDecision(timer);
        var stayToFollowTransition = new Transition(stayToFollowDecision,
                                            followState);

        var followAction = new FollowAction(gameObject, 1.0f, 2.0f);

        var followToStayDecision = new FollowToStayDecision(gameObject, 0.01f);
        var followToStayTransition = new Transition(followToStayDecision,
                                            stayState);

        stayState.AddAction(stayAction);
        stayState.AddTransition(stayToFollowTransition);

        followState.AddAction(followAction);
        followState.AddTransition(followToStayTransition);

        _roamingFragment = new StateMachineFragment(stayState);
        _roamingFragment.AddState(followState);

        return _roamingFragment;
    }

    private StateMachineFragment BuildChaseFragment()
    {
        var chaseAction = new ChaseAction(gameObject, enemy, 0.5f);
        var chaseState = new State();
        chaseState.AddAction(chaseAction);

        return new StateMachineFragment(chaseState);
    }

    [SerializeField] private GameObject enemy;

    private StateMachine _stateMachine;
    private StateMachineFragment _roamingFragment;
}
