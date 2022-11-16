using AI.Base;
using UnityEngine;

namespace AI.Roaming
{
    class StayToFollowDecision : IDecision
    {
        public StayToFollowDecision(CountDownTimer timer)
        {
            _timer = timer;
        }

        public bool Decide()
        {
            return _timer.IsDown();
        }

        private CountDownTimer _timer;
    }
}