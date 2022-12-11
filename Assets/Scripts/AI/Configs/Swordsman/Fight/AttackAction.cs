using AI.Base;
using UnityEngine;

namespace AI.Configs.Swordsman.Fight
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
