using UnityEngine;

namespace JackODrain.Core
{
    public sealed class Countdown
    {
        private float _remaining;

        public bool IsRunning => _remaining > 0f;

        public void Start(float duration)
        {
            _remaining = Mathf.Max(0f, duration);
        }

        public void Stop()
        {
            _remaining = 0f;
        }

        public void Tick(float deltaTime)
        {
            _remaining = Mathf.Max(0f, _remaining - deltaTime);
        }
    }
}
