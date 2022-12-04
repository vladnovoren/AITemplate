using AI.Base;
using UnityEngine;
using Utils.Time;

namespace AI.Roaming
{
    public class StayAction : AGameObjectBasedAction
    {
        public StayAction(GameObject gameObject, CountdownTimer timer,
            float minTime, float maxTime) :
            base(gameObject)
        {
            _minTime = minTime;
            _maxTime = maxTime;
            _timer = timer;
        }

        public override void OnEnter()
        {
            var dt = Random.Range(_minTime, _maxTime);
            _timer.Restart(dt);
        }

        public override void OnExit()
        {
            _timer.Reset();
        }

        private float _minTime;
        private float _maxTime;

        private CountdownTimer _timer;
    }
}
