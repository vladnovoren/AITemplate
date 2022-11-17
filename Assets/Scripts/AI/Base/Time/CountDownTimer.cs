using UnityEngine;

namespace AI.Base
{
    public class CountDownTimer
    {
        public void Restart(float allottedTime)
        {
            _allotedTime = allottedTime;
            _startTime = Time.time;
            IsStarted = true;
        }

        public void Reset()
        {
            _allotedTime = 0.0f;
            _startTime = 0.0f;
            IsStarted = false;
        }

        public bool IsDown()
        {
            if (!IsStarted) return false;
            return Time.time - _startTime >= _allotedTime;
        }

        private float _allotedTime;
        private float _startTime;
        public bool IsStarted { get; private set; } = false;
    }
}