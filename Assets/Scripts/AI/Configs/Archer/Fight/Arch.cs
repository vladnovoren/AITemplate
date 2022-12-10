using UnityEngine;
using Utils.Math;
using Utils.Time;

namespace AI.Configs.Archer.Fight
{
    public class Arch
    {
        public Arch(float reloadTime, Transform firePoint,
                    GameObject arrowPrefab)
        {
            _reloadTime = reloadTime;
            _timer = new CountdownTimer();
            _timer.Restart(0.0f);

            _firePointTransform = firePoint;
            _arrowPrefab = arrowPrefab;
        }

        public void TryShoot()
        {
            if (CanShoot())
            {
                SpawnArrow();
                _timer.Restart(_reloadTime + UnityEngine.Random.Range(0, _reloadEps));
            }
        }

        public bool HitsEnemy(Transform enemyTransform)
        {
            BuildRays();
            return Points.CheckObjectRaycast(_leftRay, enemyTransform) && 
                   Points.CheckObjectRaycast(_middleRay, enemyTransform) &&
                   Points.CheckObjectRaycast(_rightRay, enemyTransform);
        }

        public bool CanShoot()
        {
            return _timer.IsDown();
        }

        private void SpawnArrow()
        {
            UnityEngine.Object.Instantiate(_arrowPrefab,
                                           _firePointTransform.position,
                                           UnityEngine.Quaternion.LookRotation(
                                               _firePointTransform.forward));
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

        private readonly float _reloadTime;
        private readonly float _reloadEps = 1.0f;
        private readonly CountdownTimer _timer;

        private readonly Transform _firePointTransform;

        private Ray _leftRay;
        private Ray _middleRay;
        private Ray _rightRay;

        private readonly GameObject _arrowPrefab;
    }
}
