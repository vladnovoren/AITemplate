using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using AI.Base;

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

