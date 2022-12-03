using UnityEngine;
using Lifetime;

namespace AI.Archer
{
    public class Arrow : MonoBehaviour
    {
        private void Awake()
        {
            _enemyHealth = enemy.GetComponent<Health>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.transform.gameObject == enemy)
            {
                _enemyHealth.TakeArchDamage(_damage);
            }
            Destroy(gameObject);
        }

        [SerializeField] private GameObject enemy;
        private Health _enemyHealth;

        private float _damage = 30.0f;
    }
}
