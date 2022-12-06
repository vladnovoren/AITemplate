using UnityEngine;
using Lifetime;
using AI.Base;

namespace AI.Fighting.Swordsman
{
    public class AttackAction : AAction
    {
        public AttackAction(Fighter fighter)
        {
            _fighter = fighter;
        }

        public override void Execute()
        {
            _fighter.TryHit();
        }

        private Fighter _fighter;
    }
}
