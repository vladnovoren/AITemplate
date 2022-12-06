using UnityEngine;
using AI.Base;
using AI.Watch;
using Utils.Math;

namespace AI.Movement.Chase
{
    public class ToChaseDecision : IDecision
    {
        public ToChaseDecision(GameObject owner, GameObject chased)
        {
            _ownerTransform = owner.transform;
            _chasedTransform = chased.transform;

            _fov = owner.GetComponent<FieldOfView>();
        }

        public bool Decide()
        {
            return Points.InOpenBall(_ownerTransform.position, _chasedTransform.position,
                                     _fov.SqrValue)
                   && Points.CheckRaycast(_ownerTransform, _chasedTransform);
        }

        private Transform _ownerTransform;
        private Transform _chasedTransform;

        private FieldOfView _fov;
    }
}
