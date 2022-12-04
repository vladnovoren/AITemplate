using AI.Base;
using UnityEngine;
using Utils.Math;

namespace AI.Watch
{
    public class ToWatchDecision : IDecision
    {
        public ToWatchDecision(GameObject owner, GameObject enemy)
        {
            _ownerTransform = owner.transform;
            _enemyTransform = enemy.transform;

            _ownerWatchDistance = owner.GetComponent<AI.Chasing.FieldOfView>();
        }

        public bool Decide()
        {
            return Points.InOpenBall(_ownerTransform.position,
                                     _enemyTransform.position,
                                     _ownerWatchDistance.SqrValue);
        }

        private readonly Transform _ownerTransform;
        private readonly Transform _enemyTransform;

        private readonly AI.Chasing.FieldOfView  _ownerWatchDistance;
    }
}
