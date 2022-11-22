using UnityEngine;
using Lifetime;
using AI.Base;

namespace AI.Swordsman
{
    public class AttackAction : AGameObjectBasedAction
    {
        public AttackAction(GameObject gameObject, GameObject enemy) :
            base(gameObject)
        {
            _enemyHealth = enemy.GetComponent<Health>();
            _sword = gameObject.GetComponent<Sword>();
        }

        public override void Execute()
        {
            _enemyHealth.TakeSwordDamage(_sword.Damage);
        }

        private Health _enemyHealth;
        private Sword _sword;
    }
}
