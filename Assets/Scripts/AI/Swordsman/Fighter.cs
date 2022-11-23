using UnityEngine;
using Utils.Time;
using Lifetime;

namespace AI.Swordsman
{
    public class Fighter
    {
        public Fighter(GameObject owner, GameObject enemy, float reloadTime)
        {
            _timer = new CountDownTimer();
            _timer.Restart(0.0f);
            _sword = gameObject.GetComponent<Sword>();
            _enemyHealth = enemy.GetComponent<Health>();
        }

        public void TryHit()
        {
            if (CanHit())
            {
                _enemyHealth.TakeSwordDamage(_sword.Damage);
                _timer.Restart();
            }
        }

        private bool CanHit()
        {
            return _timer.IsDown() && CheckRaycast();
        }

        private bool CheckRaycast()
        {
            RaycastHit hit;
            if (Physics.Raycast(_transform.position, _transform.forward,
                                out hit))
                if (hit.transform.gameObject == _enemy)
                    return true;
            return false;
        }

        private Transform _transform;

        private GameObject _enemy;
        private Health _enemyHealth;

        private Sword _sword;
        private float _reloadTime;
        private CountDownTimer _timer;
    }
}
