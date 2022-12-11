using AI.Common.Watch;
using System;

namespace AI.Interaction
{
    public class SpottingManager
    {
        public void SubscribeToWatcher(ToWatchDecision toWatchDecision)
        {
            toWatchDecision.EnemySpotted += OnEnemySpotted;
        }

        public event EventHandler EnemySpotted;

        private void OnEnemySpotted(object sender, EventArgs args)
        {
            EnemySpotted?.Invoke(this, args);
        }
    }
}