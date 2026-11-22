using JackODrain.Core;
using UnityEngine;

namespace JackODrain.Player
{
    public sealed class HealthPool
    {
        private readonly Countdown _invulnerability = new();
        private readonly float _invulnerabilitySeconds;

        public HealthPool(int maxHearts, float invulnerabilitySeconds)
        {
            MaxHearts = Mathf.Max(1, maxHearts);
            Hearts = MaxHearts;
            _invulnerabilitySeconds = invulnerabilitySeconds;
        }

        public int MaxHearts { get; }
        public int Hearts { get; private set; }
        public bool IsInvulnerable => _invulnerability.IsRunning;

        public HitResult TakeHit()
        {
            if (IsInvulnerable || Hearts == 0)
                return HitResult.Ignored;

            Hearts--;
            _invulnerability.Start(_invulnerabilitySeconds);

            return Hearts == 0 ? HitResult.Depleted : HitResult.Hurt;
        }

        public void Refill()
        {
            Hearts = MaxHearts;
        }

        public void Tick(float deltaTime)
        {
            _invulnerability.Tick(deltaTime);
        }
    }
}
