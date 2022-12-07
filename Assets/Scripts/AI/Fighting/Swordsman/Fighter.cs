using UnityEngine;
using Utils.Time;
using Lifetime;
using Utils.Math;

namespace AI.Fighting.Swordsman
{
    public class Fighter
    {
        public Fighter(GameObject owner, GameObject enemy, float reloadTime)
        {
            _transform = owner.transform;

            _enemy = enemy;
            _enemyHealth = enemy.GetComponent<Health>();

            _sword = owner.GetComponent<Sword>();
            _reloadTime = reloadTime;
            _timer = new CountdownTimer();
            _timer.Restart(0.0f);
        }

        public void TryHit()
        {
            if (CanHit())
            {
                _enemyHealth.TakeSwordDamage(_sword.Damage);
                _timer.Restart(_reloadTime);
            }
        }

        private bool CanHit()
        {
            return _timer.IsDown() && CheckRaycast();
        }

        private bool CheckRaycast()
        {
            return Points.CheckObjectRaycast(new Ray(_transform.position, _transform.forward), _enemyTransform);
        }

        private Transform _transform;

        private GameObject _enemy;
        private Transform _enemyTransform;
        private Health _enemyHealth;

        private Sword _sword;
        private float _reloadTime;
        private CountdownTimer _timer;
    }
}
