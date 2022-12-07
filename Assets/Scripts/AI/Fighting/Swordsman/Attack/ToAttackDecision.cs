﻿using UnityEngine;
using AI.Base;
using AI.Movement.Chase;
using Utils.Math;

namespace AI.Fighting.Swordsman
{
    public class ToAttackDecision : IDecision
    {
        public ToAttackDecision(GameObject owner, GameObject enemy)
        {
            _ownerTransform = owner.transform;
            _enemyTransform = enemy.transform;

            _catch = owner.GetComponent<Catch>();
        }

        public bool Decide()
        {
            return Points.InOpenBall(_ownerTransform.position, _enemyTransform.position,
                                     _catch.SqrValue)
                   && Points.CheckObjectRaycast(new Ray(_ownerTransform.position, _ownerTransform.forward), _enemyTransform);
        }

        private readonly Transform _ownerTransform;
        private readonly Transform _enemyTransform;

        private readonly Catch _catch;
    }
}