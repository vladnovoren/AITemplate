using UnityEngine;

namespace AI.Chasing
{
    public class FieldOfView : Component
    {
        public FieldOfView(float radius, float angle)
        {
            Radius = radius;
            Angle = angle;
            SqrRadius = radius * radius;
        }
        
        public float Radius;

        public float SqrRadius
        {
            get;
            private set;
        }

        public float Angle;
    }
}