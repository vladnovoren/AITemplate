using UnityEngine;
using AI.Base;
using AI.Movement.Chase;
using AI.Watch;
using AI.Fighting.Swordsman;

namespace AI.Configs.Swordsman
{
    public class Swordsman : MonoBehaviour
    {
        private void Awake()
        {
            Init();
            BuildStateMachines();
        }

        private void Start()
        {
            _watchStateMachine.OnEntry();
            _movementStateMachine.OnEntry();
            _attackStateMachine.OnEntry();
        }

        private void Update()
        {
            _watchStateMachine.Execute();
            _movementStateMachine.Execute();
            _attackStateMachine.Execute();
        }

        private void Init() {
            var fov = gameObject.GetComponent<FieldOfView>();
            fov.Value = 6.0f;

            var catchComp = gameObject.GetComponent<Catch>();
            catchComp.Value = 2.0f;

            var sword = gameObject.GetComponent<Sword>();
            sword.Damage = 50f;
        }

        private void BuildStateMachines()
        {
            _watchStateMachine = new WatchStateMachine(gameObject, enemy);
            _movementStateMachine = new MovementStateMachine(gameObject, enemy);
            _attackStateMachine = new AttackStateMachine(gameObject, enemy);
        }

        [SerializeField] private GameObject enemy;

        private WatchStateMachine _watchStateMachine;
        private StateMachine _movementStateMachine;
        private AttackStateMachine _attackStateMachine;
    }
}
