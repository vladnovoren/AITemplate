using AI.Base;
using UnityEngine;

namespace AI.Chasing
{
    public class ToChaseDecision : IDecision
    {
        public ToChaseDecision(GameObject owner, GameObject chased)
        {
            _detector = new ObjectDetector(owner, chased);
        }

        public bool Decide()
        {
            Debug.Log("decides");
            return _detector.TryDetect();
        }

        private ObjectDetector _detector;
    }
}

