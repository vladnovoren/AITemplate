using AI.Base;
using System;

namespace AI.Common.Animations
{
    public class ToMutingAnimationDecision : ADecision
    {
        public override bool Decide()
        {
            if (_animationStarted)
            {
                _animationStarted = false;
                return true;
            }
            return false;
        }

        public void OnAnimationStarted(object sender, EventArgs args)
        {
            _animationStarted = true;
        }

        private bool _animationStarted = false;
    }
}