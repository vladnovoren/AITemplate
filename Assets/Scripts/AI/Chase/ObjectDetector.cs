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

            _victim = victim;
        }

        public bool TryDetect()
        {
            var res = CheckRadius();
            return res;
        }

        private bool CheckRadius()
        {
            return Points.InOpenBall(_victim.transform.position,
                                        _persecutor.transform.position,
                                        _fov.SqrValue);
        }

        private uint _raysAmount;

        private GameObject _persecutor;
        private FieldOfView _fov;

        private GameObject _victim;
    }
}
