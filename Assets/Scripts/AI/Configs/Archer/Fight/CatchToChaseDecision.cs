using UnityEngine;
using UnityEngine.AI;
using AI.Base;
using Utils.Math;
using System;

namespace AI.Configs.Archer.Fight
{
    public class CatchToChaseDecision : IDecision
    {
        public CatchToChaseDecision(GameObject persecutor, GameObject target,
                                    AttackAction attackAction)
        {
            _persecutorCatch = persecutor.GetComponent<Catch>();
            _persecutorAgentTransform = persecutor.GetComponent<NavMeshAgent>().transform;
            _targetTransform = target.transform;
            attackAction.NeedToComeCloser += OnNeedToComeCloser;
        }

        public bool Decide()
        {
            var result = !Points.InOpenBall(_targetTransform.position,
                                            _persecutorAgentTransform.position,
                                            _persecutorCatch.SqrValue) ||
                         _needToComeCloser;
            _needToComeCloser = false;
            return result;
        }

        public void OnNeedToComeCloser(object sender, EventArgs args)
        {
            _needToComeCloser = true;
        }

        private readonly Catch _persecutorCatch;
        private readonly Transform _persecutorAgentTransform;
        private readonly Transform _targetTransform;

        private bool _needToComeCloser = false;
    }
}