using System;

namespace AI.Base
{
    public abstract class NotifyingDecision : ADecision
    {
        public event EventHandler DecisionAccepted;

        private void OnDecisionAccepted()
        {
            DecisionAccepted?.Invoke(this, EventArgs.Empty);
        }
    }
}