using UnityEngine;
using Utils.Time;

namespace AI.Swordsman
{
    public class Fighter : MonoBehaviour
    {
        private void Start()
        {
            _timer = new CountDownTimer();
            _timer.Restart(0.0f);
            _sword = gameObject.GetComponent<Sword>();
        }

        public float ReloadTime { get; set; } = 1.0f;
        public GameObject Enemy;

        public float TryHit()
        {
            if (!_timer.IsDown())
                return 0.0f;

            _timer.Restart(ReloadTime);
            return _sword.Damage;
        }

        private Sword _sword;
        private CountDownTimer _timer;
    }
}
