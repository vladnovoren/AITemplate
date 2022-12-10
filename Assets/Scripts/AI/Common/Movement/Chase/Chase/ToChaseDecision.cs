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
            if (Points.InOpenBall(_ownerTransform.position, _chasedTransform.position,
                                  _fov.SqrValue))
            {
                if (_alreadyChased)
                    return true;

                if (Points.CheckObjectRaycast(_ownerTransform, _chasedTransform))
                {
                    _alreadyChased = true;
                    return true;
                }
            }
            return false;
        }

        private Transform _ownerTransform;
        private Transform _chasedTransform;

        private FieldOfView _fov;

        private bool _alreadyChased = false;
    }
}
