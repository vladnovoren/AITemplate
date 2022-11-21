using UnityEngine;

namespace AI.Chasing
{
    public class FieldOfView : MonoBehaviour
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
