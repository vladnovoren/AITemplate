using AI.Base;
using UnityEngine;

namespace AI.Chasing
{
    public class RoamToChaseDecision : IDecision
    {
        public RoamToChaseDecision(GameObject owner, GameObject chased)
        {
            _detector = new ObjectDetector(owner, chased);
        }

        public bool Decide()
        {
            return _detector.TryDetect();
        }

        private ObjectDetector _detector;
    }
}

