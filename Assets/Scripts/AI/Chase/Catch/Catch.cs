using AI.Base;
using UnityEngine;

namespace AI.Chasing
{
    public class Catch : MonoBehaviour
    {
        public float Radius
        {
            get => _radius;
            set
            {
                _radius = value;
                SqrRadius = value * value;
            }
        }

        public float SqrRadius
        {
            get;
            private set;
        }

        private float _radius;
    }
}

