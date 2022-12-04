using AI.Base;
using AI.Chasing;
using UnityEngine;

namespace AI.Swordsman
{
    public class ToAttackDecision : 
    {
        public ToAttackDecision(GameObject persecutor, GameObject victim)
        {
            _persecutor = persecutor;
            _victim = victim;

            _objectDetector = new ObjectDetector(persecutor, victim);
        }

        private GameObject _persecutor;
        private GameObject _victim;

        private ObjectDetector _objectDetector;
    }
}