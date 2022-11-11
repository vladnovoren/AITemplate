using AI.Base;
using UnityEngine;

namespace AI.Chasing
{
    public class ToChaseDecision : AGameObjectBasedDecision
    {
        public ToChaseDecision(GameObject owner, GameObject chased) : base(owner)
        {
            _chased = chased;
        }

        public override bool Decide()
        {
        }

        private GameObject _chased;
    }
}