using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using AI.Base;

namespace AI.Chasing
{
    public class ChaseToCatchDecision : IDecision
    {
        public ChaseToCatchDecision(GameObject persecutor, GameObject victim)
        {
            _catchSqrRadius = persecutor.GetComponent<Catch>().SqrValue;
            _persecutorAgentTransform = persecutor.GetComponent<NavMeshAgent>()
                                                                    .transform;
            _victimTransform = victim.transform;
        }

        public bool Decide()
        {
            return (_victimTransform.position -
                    _persecutorAgentTransform.position).sqrMagnitude >
                    _catchSqrRadius;
        }

        private float _catchSqrRadius;
        private Transform _persecutorAgentTransform;
        private Transform _victimTransform;
    }
}
