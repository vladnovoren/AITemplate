using UnityEngine;
using Utils.Math;
using Utils.Time;

namespace AI.Fighting.Archer
{
    public class Arch
    {
        public Arch(float reloadTime, Transform firePoint,
                    GameObject arrowPrefab, GameObject enemy)
        {
            _reloadTime = reloadTime;
            _timer = new CountdownTimer();
            _timer.Restart(0.0f);

            _firePoint = firePoint;

            _arrowPrefab = arrowPrefab;

            _enemy = enemy;
            _enemyTransform = enemy.transform;
        }

        public void TryShoot()
        {
            if (CanShoot())
            {
                SpawnArrow();
                _timer.Restart(_reloadTime + Random.Range(0, _reloadEps));
            }
        }

        private void SpawnArrow()
        {
            Object.Instantiate(_arrowPrefab, _firePoint.position,
                               Quaternion.LookRotation(_firePoint.forward));
        }

        private bool CanShoot()
        {
            return _timer.IsDown() && CheckRaycast();
        }

        private bool CheckRaycast()
        {
            return Points.CheckObjectsRaycast(_firePoint, _enemyTransform);
        }

        private readonly float _reloadTime;
        private readonly float _reloadEps = 1.0f;
        private readonly CountdownTimer _timer;

        private readonly Transform _firePoint;

        private readonly GameObject _arrowPrefab;

        private readonly GameObject _enemy;
        private readonly Transform _enemyTransform;
    }
}
