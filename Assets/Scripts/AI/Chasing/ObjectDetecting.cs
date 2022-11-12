using System;
using UnityEngine;
using static System.Math;

namespace AI.Chasing
{
    public class ObjectDetecting
    {
        public ObjectDetecting(GameObject persecutor, GameObject victim, uint raysAmount)
        {
            _persecutor = persecutor;
            _fov = persecutor.GetComponent<FieldOfView>();

            _victim = victim;
            _victimWidth = victim.GetComponent<ObjectWidth>();

            _raysAmount = raysAmount;
        }

        public bool TryDetect()
        {
            return CheckRadius() && CheckAngle() && CheckRays();
        }

        private bool CheckRadius()
        {
            return (_victim.transform.position - _persecutor.transform.position).sqrMagnitude
                   < _fov.SqrRadius;
        }

        private bool CheckAngle()
        {
            return Math.Abs(Vector3.Angle(_persecutor.transform.forward,
                _victim.transform.position - _persecutor.transform.position)) >= _fov.Angle;
        }

        private bool CheckRays()
        {
            var persecutorToVictim = _victim.transform.position - _persecutor.transform.position;
            var victimAngle = (float)Math.Atan2(0.5 * _victimWidth.Value, persecutorToVictim.magnitude);
            var deltaAngle = victimAngle / _raysAmount;

            return RaycastCircle(deltaAngle) || RaycastCircle(-deltaAngle);
        }

        private bool RaycastCircle(float deltaAngle)
        {
            var ray = new Ray(_persecutor.transform.position, _persecutor.transform.forward);
            RaycastHit hitInfo;

            for (uint i = 0; i < _raysAmount / 2; ++i)
            {
                if (Physics.Raycast(ray, out hitInfo, _fov.Radius))
                    if (hitInfo.collider.gameObject == _victim)
                        return true;
                ray.direction = Quaternion.AngleAxis(deltaAngle, Vector3.up) * ray.direction;
            }

            return false;
        }

        private uint _raysAmount;

        private GameObject _persecutor;
        private FieldOfView _fov;
        
        private GameObject _victim;
        private ObjectWidth _victimWidth;
    }
}