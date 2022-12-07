using AI.Base;
using AI.Movement.Chase;
using Utils.Math;
using UnityEngine;

namespace AI.Fighting.Archer
{
    public class ToAttackDecision : IDecision
    {
        public ToAttackDecision(GameObject owner, GameObject enemy, GameObject firePoint)
        {
            _ownerTransform = owner.transform;
            _enemyTransform = enemy.transform;

            _firePointTransform = firePoint.transform;

            _catch = owner.GetComponent<Catch>();

            _middleRay = new Ray();
            _leftRay = new Ray();
            _rightRay = new Ray();
        }

        public bool Decide()
        {
            return Points.InOpenBall(_ownerTransform.position, _enemyTransform.position,
                                     _catch.SqrValue)
                   && CheckRaycast();
        }

        private bool CheckRaycast()
        {
            BuildRays();
            return Points.CheckObjectRaycast(_leftRay, _enemyTransform)
                   && Points.CheckObjectRaycast(_middleRay, _enemyTransform)
                   && Points.CheckObjectRaycast(_rightRay, _enemyTransform);
        }

        private void BuildRays()
        {
            _middleRay.origin = _firePointTransform.position;

            var toRight = _firePointTransform.localScale.x * _firePointTransform.right;
            var toLeft = -toRight;

            _leftRay.origin = _middleRay.origin + toLeft;
            _rightRay.origin = _middleRay.origin + toRight;

            _leftRay.direction = _middleRay.direction = _rightRay.direction = _firePointTransform.forward;
        }

        private readonly Transform _ownerTransform;
        private readonly Transform _enemyTransform;

        private readonly Transform _firePointTransform;

        private readonly Catch _catch;

        private Ray _middleRay;
        private Ray _leftRay;
        private Ray _rightRay;
    }
}
