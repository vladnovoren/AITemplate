using AI.Base;
using AI.Common.Watch;
using AI.Interaction;
using System;

namespace AI.Configs.Archer
{
    public class RoamToFightDecision : ADecision
    {
        public RoamToFightDecision(SpottingManager spottingManager)
        {
            spottingManager.EnemySpotted += OnEnemySpotted;
        }

        public override bool Decide()
        {
            if (_enemySpotted)
            {
                _enemySpotted = false;
                return true;
            }
            return false;
        }

        private void OnEnemySpotted(object sender, EventArgs args)
        {
            _enemySpotted = true;
        }

        private bool _enemySpotted = false;
    }
}