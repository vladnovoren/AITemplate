using AI.Base;
using System;
using UnityEngine;

namespace AI.Configs.Archer.Fight
{
    public class AttackAction : AAction
    {
        public AttackAction(Fighter fighter)
        {
            _fighter = fighter;
        }

        public override void Execute()
        {
            Debug.Log("AttackAction.Execute()");
            if (!_fighter.HitsEnemy())
            {
                OnNeedToComeCloser();
            }
            else
            {
                _fighter.TryShoot();
            }
        }

        public event EventHandler NeedToComeCloser;

        private void OnNeedToComeCloser()
        {
            NeedToComeCloser?.Invoke(this, EventArgs.Empty);
        }

        private readonly Fighter _fighter;
    }
}
