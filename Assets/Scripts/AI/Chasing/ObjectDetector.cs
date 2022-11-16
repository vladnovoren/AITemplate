using System;
using UnityEngine;
using static System.Math;

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
            Debug.Log("radius: " + _fov.SqrRadius);
            Debug.Log("CheckRadius(): " + res);
            return res;
        }

        private bool CheckRadius()
        {
            return (_victim.transform.position - _persecutor.transform.position).sqrMagnitude
                   <= _fov.SqrRadius;
        }

        private uint _raysAmount;

        private GameObject _persecutor;
        private FieldOfView _fov;

        private GameObject _victim;
    }
}
