using UnityEngine;
using Utils.Math;

namespace AI.Chasing
{
    public class ObjectDetector
    {
        public ObjectDetector(GameObject persecutor, GameObject victim)
        {
            _persecutor = persecutor;
            _fov = persecutor.GetComponent<FieldOfView>();
            _persecutorTransform = persecutor.transform;

            _victim = victim;
            _victimTransform = victim.transform;
        }

        public bool TryDetect()
        {
            return CheckRadius() && CheckRays();
        }

        private bool CheckRadius()
        {
            return Points.InOpenBall(_victimTransform.position,
                                     _persecutorTransform.position,
                                     _fov.SqrValue);
        }

        private bool CheckRays()
        {
            return Points.CheckRaycast(_persecutorTransform, _victimTransform);
        }

        private readonly GameObject _persecutor;
        private readonly FieldOfView _fov;

        private readonly Transform _persecutorTransform;
        private readonly Transform _victimTransform;

        private readonly GameObject _victim;
    }
}
