using UnityEngine;

namespace AI.Base
{
    public abstract class AGameObjectBasedAAction : AAction
    {
        protected AGameObjectBasedAAction(GameObject gameObject)
        {
            _gameObject = gameObject;
        }

        protected GameObject _gameObject;
    }
}