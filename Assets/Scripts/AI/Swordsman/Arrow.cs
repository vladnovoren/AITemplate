using UnityEngine;
using Lifetime;

namespace AI.Swordsman
{
    public class Arrow : MonoBehaviour
    {
        private void Awake()
        {
            _enemyHealth = enemy.GetComponent<Health>();
        }

        private void OnTriggerEnter(Collider otherCollider)
        {
            if (IsHitEnemy(otherCollider))
                DamageEnemy();

            Destroy(gameObject);
        }

        private bool IsHitEnemy(Collider hitCollider)
        {
            return hitCollider.gameObject == enemy;
        }

        private void DamageEnemy()
        {
            _enemyHealth.TakeArchDamage(_damage);
        }

        [SerializeField] private GameObject enemy;
        private Health _enemyHealth;
        private float _damage = 50.0f;
    }
}
