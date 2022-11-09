using AI.Base;
using UnityEngine;
using UnityEngine.AI;

namespace AI.Roaming
{
    public class StayAction : AGameObjectBasedAction 
    {
        public StayAction(GameObject gameObject, CountDownTimer timer,
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

        private CountDownTimer _timer;
    }
}