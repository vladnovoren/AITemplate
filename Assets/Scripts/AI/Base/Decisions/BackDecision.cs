using System;

namespace AI.Base
{
    public class BackDecision : ADecision
    {
        public BackDecision(Transition forwardTransition)
        {
            forwardTransition.Accepted += OnForwardAccepted;
        }

        public override bool Decide()
        {
            if (_forwardAccepted)
            {
                _forwardAccepted = false;
                return true;
            }
            return false;
        }

        private void OnForwardAccepted(object sender, EventArgs args)
        {
            _forwardAccepted = true;
        }

        private bool _forwardAccepted = false;
    }
}