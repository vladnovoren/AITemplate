using UnityEngine;

namespace AI.Chasing
{
    public class ObjectDetecting
    {
        public ObjectDetecting(GameObject persecutor, GameObject victim)
        {
            _persecutor = persecutor;
            _fov = persecutor.GetComponent<FieldOfView>();
            
            _victim = victim;
            _victimWidth = victim.GetComponent<ObjectWidth>();
        }

        public bool TryDetect()
        {
        }

        private GameObject _persecutor;
        private FieldOfView _fov;
        
        private GameObject _victim;
        private ObjectWidth _victimWidth;
    }
}