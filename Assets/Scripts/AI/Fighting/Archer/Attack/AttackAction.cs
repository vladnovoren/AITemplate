using AI.Base;
using System;

namespace AI.Fighting.Archer
{
    public class AttackAction : AAction
    {
        public AttackAction(Fighter fighter)
        {
            _fighter = fighter;
        }

        public override void Execute()
        {
            if (!_fighter.HitsEnemy())
            {
                OnNeedToComeCloser(this, EventArgs.Empty);
            }
            else
            {
                _fighter.TryShoot();
            }
        }

        public event EventHandler NeedToComeCloser;

        private void OnNeedToComeCloser(object sender, EventArgs args)
        {
            NeedToComeCloser?.Invoke(this, args);
        }

        private readonly Fighter _fighter;
    }
}
