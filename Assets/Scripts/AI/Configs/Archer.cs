using UnityEngine;
using AI.Base;
using AI.Watch;
using AI.Movement.Chase;
using AI.Movement.Roam;
using AI.Fighting.Archer;

namespace AI.Configs
{
    public class Archer : MonoBehaviour
    {
        private void Awake()
        {
            Init();
            BuildStateMachines();
        }

        private void Start()
        {
            _movementStateMachine.OnEntry();
            _attackStateMachine.OnEntry();
            _watchStateMachine.OnEntry();
        }

        private void Update()
        {
            _movementStateMachine.Execute();
            _attackStateMachine.Execute();
            _watchStateMachine.Execute();
        }

        private void Init() {
            var fov = gameObject.GetComponent<FieldOfView>();
            fov.Value = 12.0f;

            var catchComp = gameObject.GetComponent<Catch>();
            catchComp.Value = 6.0f;
        }

        private void BuildStateMachines()
        {
            _watchStateMachine = new WatchStateMachine(gameObject, enemy);

            _roamStateMachine = new RoamStateMachine(gameObject);
            _chaseStateMachine = new ChaseStateMachine(gameObject, enemy);
            _movementStateMachine = StateMachine.Merge(_roamStateMachine, _chaseStateMachine, _roamStateMachine.Entry);

            _attackStateMachine = new AttackStateMachine(gameObject, firePoint, arrowPrefab, enemy);
        }

        [SerializeField] private GameObject enemy;

        [SerializeField] private GameObject arrowPrefab;
        [SerializeField] private GameObject firePoint;

        private WatchStateMachine _watchStateMachine;
        private RoamStateMachine _roamStateMachine;
        private ChaseStateMachine _chaseStateMachine;
        private StateMachine _movementStateMachine;
        private AttackStateMachine _attackStateMachine;
    }
}