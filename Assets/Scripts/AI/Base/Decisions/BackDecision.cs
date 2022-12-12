using System;

namespace AI.Base
{
    public class BackDecision : ADecision
    {
        public BackDecision(NotifyingDecision forwardDecision)
        {
            forwardDecision.DecisionAccepted += OnForwardDecisionAccepted;
        }

        public override bool Decide()
        {
            if (_forwardDecision)
            {
                _forwardDecision = false;
                return true;
            }
            return false;
        }

        private void OnForwardDecisionAccepted(object sender, EventArgs args)
        {
            _forwardDecision = true;
        }

        private bool _forwardDecision = false;
    }
}