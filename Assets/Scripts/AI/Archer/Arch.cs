using UnityEngine;
using Utils.Time;
using Lifetime;

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
            RaycastHit hit;
            if (Physics.Raycast(_firePoint.position, _firePoint.forward,
                        out hit))
                if (hit.transform.gameObject == _enemy)
                    return true;
            return false;
        }

        private float _reloadTime;
        private CountdownTimer _timer;

        private Transform _firePoint;

        private GameObject _arrowPrefab;

        private GameObject _enemy;
        private Transform _enemyTransform;
    }
}
