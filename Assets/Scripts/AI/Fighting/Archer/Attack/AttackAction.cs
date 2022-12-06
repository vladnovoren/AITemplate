using AI.Base;

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
            _fighter.TryShoot();
        }

        private Fighter _fighter;
    }
}
