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
        Init();
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

        var roamToChaseDecision = new RoamToChaseDecision(gameObject, enemy);
        var roamToChaseTransition = new Transition(roamToChaseDecision,
                                            chaseFragment.Entry);
        roamFragment.AddTransitionToAllStates(roamToChaseTransition);

        var catchFragment = BuildCatchFragment();

        var chaseToCatchDecision = new ChaseToCatchDecision(gameObject, enemy);
        var chaseToCatchTransition = new Transition(chaseToCatchDecision,
                                                    catchFragment.Entry);

        var catchToChaseDecision = new ToChaseDecision(gameObject, enemy);
        var catchToChaseTransition = new Transition(catchToChaseDecision,
                                                    chaseFragment.Entry);

        chaseFragment.AddTransitionToAllStates(chaseToCatchTransition);
        catchFragment.AddTransitionToAllStates(catchToChaseTransition);

        return new StateMachine(roamFragment.Entry);
    }

    private void Init() {
        var fov = gameObject.GetComponent<FieldOfView>();
        fov.Init(3.0f);

        var catchComp = gameObject.GetComponent<Catch>();
        catchComp.Radius = 2.0f;
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

    private StateMachineFragment BuildCatchFragment()
    {
        var catchState = new State();
        return new StateMachineFragment(catchState);
    }

    [SerializeField] private GameObject enemy;

    private StateMachine _stateMachine;
    private StateMachineFragment _roamingFragment;
}
