using UnityEngine;
using AI.Watch;
using AI.Movement.Chase;
using AI.Fighting.Archer;

namespace AI.Configs.Archer
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
            _movementStateMachine = new MovementStateMachine(gameObject, enemy);
            _attackStateMachine = new AttackStateMachine(gameObject, firePoint, arrowPrefab, enemy);
        }

        [SerializeField] private GameObject enemy;

        [SerializeField] private GameObject arrowPrefab;
        [SerializeField] private GameObject firePoint;

        private WatchStateMachine _watchStateMachine;
        private MovementStateMachine _movementStateMachine;
        private AttackStateMachine _attackStateMachine;
    }
}