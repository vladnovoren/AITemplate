using AI.Base;
using UnityEngine.AI;
using UnityEngine;

namespace AI.Movement.Roam
{
    class FollowAction : AGameObjectBasedAction
    {
        public FollowAction(GameObject gameObject, float minDeltaCoord,
                        float maxDeltaCoord) :
            base(gameObject)
        {
            _minDeltaCoord = minDeltaCoord;
            _maxDeltaCoord = maxDeltaCoord;
            _navMeshAgent = gameObject.GetComponent<NavMeshAgent>();
        }

        public override void OnEnter()
        {
            var currPos = _navMeshAgent.transform.position;
            var randomVector = GetRandomVector();
            _navMeshAgent.SetDestination(currPos + randomVector);
            if (_navMeshAgent.pathStatus != NavMeshPathStatus.PathComplete)
                _navMeshAgent.SetDestination(currPos - randomVector);
        }

        public override void OnExit()
        {
            _navMeshAgent.destination = _navMeshAgent.transform.position;
        }

        private Vector3 GetRandomVector()
        {
            var angle = Random.Range(0.0f, 360.0f);
            var dir = Quaternion.AngleAxis(angle, Vector3.up)
                      * Vector3.forward;
            return dir * Random.Range(_minDeltaCoord, _maxDeltaCoord);
        }

        private float _minDeltaCoord;
        private float _maxDeltaCoord;

        private NavMeshAgent _navMeshAgent;
    }
}
