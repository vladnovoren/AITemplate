using UnityEngine;

namespace Utils.Math
{
    public class Points
    {
        public static bool InOpenBall(Vector3 center, Vector3 toCheck,
                                      float sqrRadius)
        {
            return (center - toCheck).sqrMagnitude < sqrRadius;
        }

        public static bool CheckObjectRaycast(Ray ray, Transform target)
        {
            return Physics.Raycast(ray, out RaycastHit hit)
                   && hit.transform == target;
        }
    }
}
