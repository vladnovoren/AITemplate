using UnityEngine;

namespace AI.Base
{
    public abstract class AGameObjectBasedDecision : IDecision
    {
        protected AGameObjectBasedDecision(GameObject gameObject)
        {
            _gameObject = gameObject;
        }

        public abstract bool Decide();

        protected GameObject _gameObject;
    }
}