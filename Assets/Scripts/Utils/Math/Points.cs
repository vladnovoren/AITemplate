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
    }
}
