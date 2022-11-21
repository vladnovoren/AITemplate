using AI.Base;
using UnityEngine;
using UnityEngine.AI;
using Utils.Math;

namespace AI.Chasing
{
    public class ChaseAction : AGameObjectBasedAction
    {
        public ChaseAction(GameObject owner, GameObject chased,
                            float rebuildPathDist) : base(owner)
        {
            _navMeshAgent = owner.GetComponent<NavMeshAgent>();
            _chased = chased;

            _rebuildPathDist = rebuildPathDist;
            _sqrRebuildPathDist = rebuildPathDist * rebuildPathDist;
        }

        public override void OnEnter()
        {
            Debug.Log("ChaseAction.OnEnter");
            SetDestinationToChased();
            Debug.Log("DestinationToChase: " + _navMeshAgent.destination);
        }

        public override void Execute()
        {
            if (NeedToChangePath())
                SetDestinationToChased();
            Debug.Log("ChaseAction.Execute");
        }

        public override void OnExit()
        {
            _navMeshAgent.destination = _navMeshAgent.transform.position;
        }

        private bool NeedToChangePath()
        {
            return !Points.InOpenBall(_chased.transform.position,
                                    _navMeshAgent.destination,
                                    _sqrRebuildPathDist);
        }

        private void SetDestinationToChased()
        {
            _navMeshAgent.destination = _chased.transform.position;
        }

        private NavMeshAgent _navMeshAgent;
        private GameObject _chased;

        private float _rebuildPathDist;
        private float _sqrRebuildPathDist;
    }
}
