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

        private StateGroup BuildChaseGroup()
        {
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