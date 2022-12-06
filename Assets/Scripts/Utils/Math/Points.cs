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

        public static bool CheckRaycast(Transform watcher, Transform target)
        {
            var persecutorToVictim = new Ray(watcher.position,
                                             target.position
                                             - watcher.position);
            return Physics.Raycast(persecutorToVictim, out RaycastHit hit)
                   && hit.transform == target;
        }
    }
}
