using System;
using UnityEngine;

namespace JackODrain.Player
{
    public sealed class PlayerHealth : MonoBehaviour
    {
        [SerializeField, Min(1)] private int _maxHearts = 5;

        [Tooltip("Seconds without taking damage after each hit.")]
        [SerializeField, Min(0f)] private float _invulnerabilitySeconds = 1f;

        private HealthPool _pool;

        public event Action<int> HeartsChanged;
        public event Action Hurt;
        public event Action Depleted;

        public int Hearts => _pool.Hearts;
        public int MaxHearts => _pool.MaxHearts;
        public bool IsInvulnerable => _pool.IsInvulnerable;

        private void Awake()
        {
            _pool = new HealthPool(_maxHearts, _invulnerabilitySeconds);
        }

        private void Update()
        {
            _pool.Tick(Time.deltaTime);
        }

        public void TakeHit()
        {
            HitResult result = _pool.TakeHit();

            if (result == HitResult.Ignored)
                return;

            HeartsChanged?.Invoke(_pool.Hearts);

            if (result == HitResult.Depleted)
                Depleted?.Invoke();
            else
                Hurt?.Invoke();
        }

        public void Refill()
        {
            _pool.Refill();
            HeartsChanged?.Invoke(_pool.Hearts);
        }
    }
}
