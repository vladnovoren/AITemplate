using System.Collections;
using System.Collections.Generic;
using AI.Base;
using AI.Roaming;
using UnityEngine;

public class Manager : MonoBehaviour
{
    private void Start()
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

        _stateMachine = new StateMachine(stayState);
        _stateMachine.OnEnter();
    }

    private void Update()
    {
        _stateMachine.Execute();
    }

    [SerializeField] private GameObject enemy;
    private StateMachine _stateMachine;
}
