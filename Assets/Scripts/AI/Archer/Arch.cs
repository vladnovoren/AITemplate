using UnityEngine;
using Utils.Time;

namespace AI.Archer
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
                _timer.Restart(_reloadTime);
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
            if (Physics.Raycast(_firePoint.position, _firePoint.forward,
                        out RaycastHit hit))
                if (hit.transform.gameObject == _enemy)
                    return true;
            return false;
        }

        private readonly float _reloadTime;
        private readonly CountdownTimer _timer;

        private readonly Transform _firePoint;

        private readonly GameObject _arrowPrefab;

        private readonly GameObject _enemy;
        private readonly Transform _enemyTransform;
    }
}
