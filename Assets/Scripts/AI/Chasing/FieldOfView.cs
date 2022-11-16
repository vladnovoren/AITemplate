using UnityEngine;

namespace AI.Chasing
{
    public class FieldOfView : MonoBehaviour
    {
        public void Init(float radius)
        {
            this.radius = radius;
            SqrRadius = radius * radius;
        }

        public float radius;

        public float SqrRadius
        {
            get;
            private set;
        }
    }
}
