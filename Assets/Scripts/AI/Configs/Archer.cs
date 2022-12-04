using UnityEngine;
using AI.Base;
using AI.Chasing;
using AI.Roaming;
using Utils.Time;
using AI.Watch;

namespace AI.Configs
{
    public class Archer : MonoBehaviour
    {
        public void Awake()
        {
            Init();
            _mainStateMachine = BuildMainStateMachine();
            _watchStateMachine = BuildWatchStateMachine();
        }

        private void Start()
        {
            _mainStateMachine.OnEnter();
            _watchStateMachine.OnEnter();
        }

        private void Update()
        {
            _mainStateMachine.Execute();
            _watchStateMachine.Execute();
        }

        private StateMachine BuildMainStateMachine()
        {
            var roamFragment = BuildRoamGroup();
            var chaseFragment = BuildChaseGroup();

            var roamToChaseDecision = new ToChaseDecision(gameObject, enemy);
            var roamToChaseTransition = new Transition(roamToChaseDecision,
                                                chaseFragment.Entry);
            roamFragment.AddTransitionToAllStates(roamToChaseTransition);

            var attackFragment = BuildAttackGroup();

            var chaseToAttackDecision = new ToCatchDecision(gameObject, enemy);
            var chaseToAttackTransition = new Transition(chaseToAttackDecision,
                                                        attackFragment.Entry);

            var attackToChaseDecision = new OppositeDecision(
                                            new ToCatchDecision(gameObject,
                                                                        enemy));
            var attackToChaseTransition = new Transition(attackToChaseDecision,
                                                        chaseFragment.Entry);

            chaseFragment.AddTransitionToAllStates(chaseToAttackTransition);
            attackFragment.AddTransitionToAllStates(attackToChaseTransition);

            return new StateMachine(roamFragment.Entry);
        }

        private void Init() {
            var fov = gameObject.GetComponent<FieldOfView>();
            fov.Value = 6.0f;

            var catchComp = gameObject.GetComponent<Catch>();
            catchComp.Radius = 6.0f;

            var watchDistance = gameObject.GetComponent<WatchDistance>();
            watchDistance.Value = 6.0f;
        }

        private StateGroup BuildRoamGroup()
        {
            var stayState = new State();
            var followState = new State();

            var timer = new CountdownTimer();
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

            _roamingGroup = new StateGroup(stayState);
            _roamingGroup.AddState(followState);

            return _roamingGroup;
        }

        private StateGroup BuildChaseGroup()
        {
            var chaseAction = new ChaseAction(gameObject, enemy, 0.01f);
            var chaseState = new State();
            chaseState.AddAction(chaseAction);
    
            return new StateGroup(chaseState);
        }

        private StateMachine BuildWatchStateMachine()
        {
            var watchState = new State();
            var watchAction = new WatchAction(gameObject, enemy);
            watchState.AddAction(watchAction);

            var idleState = new State();

            var toWatchDecision = new ToWatchDecision(gameObject, enemy);
            var toWatchTransition = new Transition(toWatchDecision, watchState);
            idleState.AddTransition(toWatchTransition);

            var toIdleTransition = new Transition(new OppositeDecision(toWatchDecision),
                                                    idleState);
            watchState.AddTransition(toIdleTransition);

            return new StateMachine(idleState);
        }

        private StateMachine BuildAttackStateMachine()
        {
            var arch = new AI.Archer.Arch(1.0f, firePoint.transform, arrowPrefab, enemy);
            var fighter = new AI.Archer.Fighter(arch);

            var idleState = new State();

            var attackState = new State();
            attackState.AddAction(new AI.Archer.AttackAction(fighter));

            var toAttackDecision = new 
    
            return new StateMachine(attackState);

        }

        [SerializeField] private GameObject enemy;

        [SerializeField] private GameObject arrowPrefab;
        [SerializeField] private GameObject firePoint;
    
        private StateMachine _mainStateMachine;
        private StateMachine _watchStateMachine;
        private StateMachine _attackStateMachine;
        private StateGroup _roamingGroup;
    }
}