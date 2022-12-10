using UnityEngine;

namespace AI.Fighting.Archer
{
    public class Fighter
    {
        public Fighter(Arch arch, GameObject enemy)
        {
            _arch = arch;
            _enemyTransform = enemy.transform;
        }

        public bool HitsEnemy()
        {
            return _arch.HitsEnemy(_enemyTransform);
        }

        public void TryShoot()
        {
            _arch.TryShoot();
        }

        private Arch _arch;
        private Transform _enemyTransform;
    }
}
