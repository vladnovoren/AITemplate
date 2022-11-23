using UnityEngine;

namespace Lifetime
{
    public class Health : MonoBehaviour
    {
        private void Update()
        {
            UpdateHealth();
            if (!IsAlive())
                Destroy(gameObject);
            UpdateArmor();
            ResetDamage();
        }

        public void TakeSwordDamage(float damage)
        {
            _swordDamage = damage;
        }

        public void TakeArchDamage(float damage)
        {
            _archDamage = damage;
        }

        private bool IsAlive()
        {
            Debug.Log("_health: " + _health);
            return _health > 0;
        }

        private void UpdateHealth()
        {
            var deltaHealth = -_swordDamage - _archDamage;
            deltaHealth *= ArmorCoeff();
            _health += deltaHealth;
        }

        private void UpdateArmor()
        {
            _armor -= _swordDamage;
            _armor -= _archDamage;
        }

        private float ArmorCoeff()
        {
            return (_armor > 0) ? 0.5f : 1.0f;
        }

        private void ResetDamage()
        {
            _swordDamage = 0;
            _archDamage = 0;
        }

        private float _health = 100f;
        private float _armor = 100f;

        private float _swordDamage = 0;
        private float _archDamage = 0;
    }
}
