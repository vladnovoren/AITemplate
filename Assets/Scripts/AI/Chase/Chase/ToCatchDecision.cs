using UnityEngine;
using UnityEngine.AI;
using AI.Base;
using Utils.Math;

namespace AI.Chasing
{
    public class ToCatchDecision : IDecision
    {
        public ToCatchDecision(GameObject persecutor, GameObject victim)
        {
            _persecutorCatch = persecutor.GetComponent<Catch>();
            _persecutorAgentTransform = persecutor.GetComponent<NavMeshAgent>().transform;
            _victimTransform = victim.transform;
        }

        public bool Decide()
        {
            return Points.InOpenBall(_victimTransform.position,
                                     _persecutorAgentTransform.position,
                                     _persecutorCatch.SqrValue);
        }

        private Catch _persecutorCatch;
        private Transform _persecutorAgentTransform;
        private Transform _victimTransform;
    }
}
