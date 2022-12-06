using UnityEngine;
using AI.Base;
using Utils.Time;
using AI.Watch;
using AI.Movement.Chase;
using AI.Movement.Roam;
using AI.Fighting.Archer;

namespace AI.Configs
{
    public class Archer : MonoBehaviour
    {
        public void Awake()
        {
            Init();
            _mainStateMachine = BuildRoamStateMachine();
            _watchStateMachine = BuildWatchStateMachine();
            _attackStateMachine = BuildAttackStateMachine();
        }

        private void Start()
        {
            _mainStateMachine.OnEnter();
            _watchStateMachine.OnEnter();
            _attackStateMachine.OnEnter();
        }

        private void Update()
        {
            _mainStateMachine.Execute();
            _watchStateMachine.Execute();
            _attackStateMachine.Execute();
        }

        private void Init() {
            var fov = gameObject.GetComponent<FieldOfView>();
            fov.Value = 12.0f;

            var catchComp = gameObject.GetComponent<Catch>();
            catchComp.Value = 6.0f;
        }

        private StateMachine BuildRoamStateMachine()
        {
            var roamGroup = BuildRoamGroup();
            var chaseGroup = BuildChaseGroup();

            var toChaseDecision = new ToChaseDecision(gameObject, enemy);
            var toChaseTransition = new Transition(toChaseDecision,
                                                   chaseGroup.Entry);
            roamGroup.AddTransitionToAllStates(toChaseTransition);

            return new StateMachine(roamGroup.Entry);
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

            var catchState = new State();
            var toCatchDecision = new ToCatchDecision(gameObject, enemy);
            var toCatchTransition = new Transition(toCatchDecision, catchState);
            chaseState.AddTransition(toCatchTransition);

            var catchToChaseDecision = new OppositeDecision(toCatchDecision);
            var catchToChaseTransition = new Transition(catchToChaseDecision, chaseState);
            catchState.AddTransition(catchToChaseTransition);

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
            var arch = new Arch(1.0f, firePoint.transform, arrowPrefab, enemy);
            var fighter = new Fighter(arch);

            var attackState = new State();
            attackState.AddAction(new AttackAction(fighter));

            var idleState = new State();
            var toAttackDecision = new ToAttackDecision(gameObject, enemy);
            var toAttackTransition = new Transition(toAttackDecision, attackState);
            idleState.AddTransition(toAttackTransition);
    
            return new StateMachine(idleState);
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